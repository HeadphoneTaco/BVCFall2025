using System;
using UnityEngine;
using System.Collections.Generic;
using CoreUtils.AssetBuckets;
using TMPro;
using UnityEditor;
using Random = UnityEngine.Random;

public class PatternSpawner : MonoBehaviour
{
    [Header("Buckets")]
    public PatternBucket patternBucket;   // typed wrapper from PatternBucket.cs
    public PrefabBucket prefabBucket;     // CoreUtils provided bucket for GameObjects

    [Header("Settings")]
    public int count = 31;
    public float spacing = 7f;
    
    [Header("Prefab Options")]
    public bool randomizePrefabs = true;
    [Tooltip("Optional: control randomness for repeatable results.")]
    public bool useSeed;
    public int randomSeed;

    public int currentPatternIndex;
    public int currentPrefabIndex;
    private readonly List<GameObject> _spawned = new();
    
    public GridMapGenerator gridMapGenerator;

    
    
    
    [Header("UI stuff")]
    public TextMeshProUGUI infoText;
    
    public void Start()
    {
        Spawn();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPattern();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousPattern();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PreviousPrefab();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            NextPrefab();
        }
    }

    public void Spawn()
    {
        Clear();

        if (patternBucket == null || patternBucket.Items == null || patternBucket.Items.Length == 0)
        {
            Debug.LogWarning("PatternSpawner: no patterns in patternBucket.");
            return;
        }

        if (prefabBucket == null || prefabBucket.Items == null || prefabBucket.Items.Length == 0)
        {
            Debug.LogWarning("PatternSpawner: no prefabs in prefabBucket.");
            return;
        }

        // Clamp indices so edits to bucket length don't crash
        currentPatternIndex = Mathf.Clamp(currentPatternIndex, 0, patternBucket.Items.Length - 1);
        currentPrefabIndex  = Mathf.Clamp(currentPrefabIndex, 0, prefabBucket.Items.Length - 1);

        var pattern = patternBucket.Items[currentPatternIndex];
        GameObject prefab = null;

        // --- Random or manual prefab selection ---
        if (prefabBucket != null && prefabBucket.Items.Length > 0)
        {
            if (useSeed)
                Random.InitState(randomSeed);

            if (randomizePrefabs)
            {
                int index = Random.Range(0, prefabBucket.Items.Length);
                prefab = prefabBucket.Items[index];
            }
            else
            {
                prefab = prefabBucket.Items[Mathf.Clamp(currentPrefabIndex, 0, prefabBucket.Items.Length - 1)];
            }
        }


        if (pattern == null || prefab == null)
        {
            Debug.LogWarning("PatternSpawner: selected pattern or prefab is null.");
            return;
        }

        List<Vector3> positions;
        try
        {
            positions = pattern.GetPositions(count, spacing) ?? new List<Vector3>();
        }
        catch (Exception ex)
        {
            Debug.LogError($"PatternSpawner: exception when evaluating pattern '{pattern.name}': {ex}");
            return;
        }

        Debug.Log($"Spawning pattern '{pattern.name}' ({positions.Count} positions) with prefab '{prefab.name}'");

        foreach (var localPos in positions)
        {
            Vector3 worldPos = transform.TransformPoint(localPos);

            GameObject chosenPrefab = prefab;

            if (randomizePrefabs && prefabBucket != null && prefabBucket.Items.Length > 0)
            {
                if (useSeed)
                    Random.InitState(randomSeed + localPos.GetHashCode());
                int index = Random.Range(0, prefabBucket.Items.Length);
                chosenPrefab = prefabBucket.Items[index];
            }

            if (chosenPrefab == null) continue;

            var go = Instantiate(chosenPrefab, worldPos, Quaternion.identity, transform);
            _spawned.Add(go);
        }
        UpdateUI();
    }
    public void Clear()
    {
        // First clear our tracked list
        for (int i = _spawned.Count - 1; i >= 0; i--)
        {
            GameObject obj = _spawned[i];
            if (obj != null)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    DestroyImmediate(obj); // edit mode: destroy instantly
                else
                    Destroy(obj);
#else
            Destroy(obj);
#endif
            }
        }
        _spawned.Clear();

        // Extra safety: clear any leftover children under this spawner
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
#if UNITY_EDITOR
            if (!Application.isPlaying)
                DestroyImmediate(child.gameObject);
            else
                Destroy(child.gameObject);
#else
        Destroy(child.gameObject);
#endif
        }
    }
    
    [ContextMenu("Generate Dungeon")]
    public void GenerateDungeon()
    {
        if (gridMapGenerator == null)
            gridMapGenerator = GetComponent<GridMapGenerator>();

        if (gridMapGenerator == null)
        {
            Debug.LogError("No GridMapGenerator found!");
            return;
        }

        // Generate new map
        gridMapGenerator.GenerateGridMap(20, 20, 0.25f);

        // Then spawn prefabs
        Clear();
        var tiles = gridMapGenerator.tiles;
        if (tiles == null) return;

        int width = tiles.GetLength(0);
        int height = tiles.GetLength(1);

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                var tile = tiles[x, y];
                Vector3 worldPos = transform.TransformPoint(new Vector3(x * spacing, 0, y * spacing));

                GameObject prefab = null;
                switch (tile.type) {
                    case GridMapGenerator.TileType.Floor:
                        prefab = prefabBucket.Items[0];
                        break;
                    case GridMapGenerator.TileType.Wall:
                        prefab = prefabBucket.Items[1];
                        break;
                }

                if (prefab != null)
                    tile.instance = Instantiate(prefab, worldPos, Quaternion.identity, transform);
            }
        }
    }
    
    //update ui
    private void UpdateUI()
    {
        //TODO: find less expensive way to do this
        if (infoText != null)
        {
            string patternName = patternBucket.Items[currentPatternIndex].name;
            string prefabName = prefabBucket.Items[currentPrefabIndex].name;
            infoText.text = $"Pattern: {patternName}\nPrefab: {prefabName}";
        }
    }
    public void NextPattern()
    {
        if (patternBucket == null || patternBucket.Items == null || patternBucket.Items.Length == 0) return;
        currentPatternIndex = (currentPatternIndex + 1) % patternBucket.Items.Length;
        Spawn();
    }
    public void PreviousPattern()
    {
        if (patternBucket == null || patternBucket.Items == null || patternBucket.Items.Length == 0) return;
        currentPatternIndex = (currentPatternIndex - 1 + patternBucket.Items.Length) % patternBucket.Items.Length;
        Spawn();
    }
    public void NextPrefab()
    {
        if (prefabBucket == null || prefabBucket.Items == null || prefabBucket.Items.Length == 0) return;
        currentPrefabIndex = (currentPrefabIndex + 1) % prefabBucket.Items.Length;
        Spawn();
    }
    public void PreviousPrefab()
    {
        if (prefabBucket == null || prefabBucket.Items == null || prefabBucket.Items.Length == 0) return;
        currentPrefabIndex = (currentPrefabIndex - 1 + prefabBucket.Items.Length) % prefabBucket.Items.Length;
        Spawn();
    }
#if UNITY_EDITOR
    public enum LineMode {
        None,
        Sequential,   // connect each point to the next
        ToCenter,     // connect each point to center
        GridLike      // connect in approximate square layout (for 2D patterns)
    }
    
    [HideInInspector] public bool ghostPreview = true;   // Gizmo preview mode
    [HideInInspector] public bool autoSpawn;     // Auto-spawn prefabs in edit mode
    [HideInInspector] public bool drawLines = true;      // Connect gizmos with lines
    [HideInInspector] public LineMode lineMode = LineMode.Sequential;
    [HideInInspector] public float gizmoSphereSize = 0.2f;
    // Guard so we only schedule one delayed call at a time
    [NonSerialized] private bool _delayedSpawnScheduled;
    // OnValidate is called during editing (and import), but we MUST NOT instantiate here.
    private void OnValidate()
    {
        // Only schedule when we want auto-spawn and are NOT in ghost preview
        if (!Application.isPlaying && autoSpawn && !ghostPreview)
        {
            ScheduleDelayedSpawn();
        }
    }
    private void ScheduleDelayedSpawn()
    {
        if (_delayedSpawnScheduled) return;
        _delayedSpawnScheduled = true;

        // Delay the actual spawn until after Unity's validation pass finishes
        EditorApplication.delayCall += DelayedSpawnCallback;
    }
    private void DelayedSpawnCallback()
    {
        // Clear the flag (so future OnValidate calls can schedule again)
        _delayedSpawnScheduled = false;

        // Safety checks — bail out if running / compiling / about to change playmode
        if (this == null) return; // object was destroyed
        if (Application.isPlaying) return;
        if (EditorApplication.isPlayingOrWillChangePlaymode) return;
        if (EditorApplication.isCompiling) {
            // if compiling, re-schedule when compile finishes
            ScheduleDelayedSpawn();
            return;
        }

        try
        {
            // Do not call Spawn directly inside OnValidate; here is delayed and safe.
            Clear();
            Spawn();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    #if UNITY_EDITOR
private void OnDrawGizmosSelected()
{
    if (!ghostPreview) return;
    if (patternBucket == null || patternBucket.Items.Length == 0) return;

    var pattern = patternBucket.Items[Mathf.Clamp(currentPatternIndex, 0, patternBucket.Items.Length - 1)];
    if (pattern == null) return;

    var positions = pattern.GetPositions(count, spacing);
    if (positions == null || positions.Count == 0) return;

    Gizmos.color = Color.cyan;

    // Draw spheres
    for (int i = 0; i < positions.Count; i++)
    {
        Vector3 worldPos = transform.TransformPoint(positions[i]);
        Gizmos.DrawWireSphere(worldPos, gizmoSphereSize);
    }

    // Draw lines based on selected mode
    if (lineMode == LineMode.None || positions.Count < 2) return;

    switch (lineMode)
    {
        case LineMode.Sequential:
            for (int i = 1; i < positions.Count; i++)
            {
                Gizmos.DrawLine(transform.TransformPoint(positions[i - 1]),
                                transform.TransformPoint(positions[i]));
            }
            break;

        case LineMode.ToCenter:
            Vector3 center = transform.position;
            for (int i = 0; i < positions.Count; i++)
            {
                Gizmos.DrawLine(center, transform.TransformPoint(positions[i]));
            }
            break;

        case LineMode.GridLike:
            int side = Mathf.CeilToInt(Mathf.Sqrt(positions.Count));
            for (int x = 0; x < side; x++)
            {
                for (int z = 0; z < side; z++)
                {
                    int i = x * side + z;
                    if (i >= positions.Count) break;

                    Vector3 a = transform.TransformPoint(positions[i]);
                    if (x + 1 < side && i + side < positions.Count)
                        Gizmos.DrawLine(a, transform.TransformPoint(positions[i + side]));
                    if (z + 1 < side && i + 1 < positions.Count)
                        Gizmos.DrawLine(a, transform.TransformPoint(positions[i + 1]));
                }
            }
            break;
    }
}
#endif

#endif
}
