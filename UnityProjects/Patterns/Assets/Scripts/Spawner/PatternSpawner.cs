using UnityEngine;
using System.Collections.Generic;
using CoreUtils.AssetBuckets;
using TMPro; 

public class PatternSpawner : MonoBehaviour
{
    [Header("Buckets")]
    public PatternBucket patternBucket;   // typed wrapper from PatternBucket.cs
    public PrefabBucket prefabBucket;     // CoreUtils provided bucket for GameObjects

    [Header("Settings")]
    public int count = 10;
    public float spacing = 2f;

    private int _currentPatternIndex;
    private int _currentPrefabIndex;
    private readonly List<GameObject> _spawned = new();
    
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

    private void Spawn()
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
        _currentPatternIndex = Mathf.Clamp(_currentPatternIndex, 0, patternBucket.Items.Length - 1);
        _currentPrefabIndex  = Mathf.Clamp(_currentPrefabIndex, 0, prefabBucket.Items.Length - 1);

        var pattern = patternBucket.Items[_currentPatternIndex];
        var prefab  = prefabBucket.Items[_currentPrefabIndex];

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
        catch (System.Exception ex)
        {
            Debug.LogError($"PatternSpawner: exception when evaluating pattern '{pattern.name}': {ex}");
            return;
        }

        Debug.Log($"Spawning pattern '{pattern.name}' ({positions.Count} positions) with prefab '{prefab.name}'");

        foreach (var localPos in positions)
        {
            Vector3 worldPos = transform.TransformPoint(localPos);

            var go = Instantiate(prefab, worldPos, Quaternion.identity, transform);
            _spawned.Add(go);
        }
        UpdateUI();
    }

    private void Clear()
    {
        for (int i = _spawned.Count - 1; i >= 0; i--)
        {
            var obj = _spawned[i];
            if (obj == null) { _spawned.RemoveAt(i); continue; }

#if UNITY_EDITOR
            if (!Application.isPlaying)
                DestroyImmediate(obj);
            else
                Destroy(obj);
#else
            Destroy(obj);
#endif
            _spawned.RemoveAt(i);
        }
        _spawned.Clear();
    }
    
    //update ui
    private void UpdateUI()
    {
        //TODO: find less expensive way to do this
        if (infoText != null)
        {
            string patternName = patternBucket.Items[_currentPatternIndex].name;
            string prefabName = prefabBucket.Items[_currentPrefabIndex].name;
            infoText.text = $"Pattern: {patternName}\nPrefab: {prefabName}";
        }
    }

    private void NextPattern()
    {
        if (patternBucket == null || patternBucket.Items == null || patternBucket.Items.Length == 0) return;
        _currentPatternIndex = (_currentPatternIndex + 1) % patternBucket.Items.Length;
        Spawn();
    }

    private void PreviousPattern()
    {
        if (patternBucket == null || patternBucket.Items == null || patternBucket.Items.Length == 0) return;
        _currentPatternIndex = (_currentPatternIndex - 1 + patternBucket.Items.Length) % patternBucket.Items.Length;
        Spawn();
    }

    private void NextPrefab()
    {
        if (prefabBucket == null || prefabBucket.Items == null || prefabBucket.Items.Length == 0) return;
        _currentPrefabIndex = (_currentPrefabIndex + 1) % prefabBucket.Items.Length;
        Spawn();
    }

    private void PreviousPrefab()
    {
        if (prefabBucket == null || prefabBucket.Items == null || prefabBucket.Items.Length == 0) return;
        _currentPrefabIndex = (_currentPrefabIndex - 1 + prefabBucket.Items.Length) % prefabBucket.Items.Length;
        Spawn();
    }
}
