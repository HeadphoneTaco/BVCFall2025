using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [Header("Prefab Settings")]
    public List<GameObject> prefabs; //list of prefabs to choose from

    public int count; //how many prefabs to spawn
    public float spacing; //how much space between each prefab

    [Header("Pattern Settings")]
    public List<PositionList> patterns; //list of scriptable pattern oooobjectss to choose from

    private int _currentPatternIndex;
    private int _currentPrefabIndex;
    //keep track of spawned objects to nuke em later
    private List<GameObject> _spawnedObjects = new();
    
    [Header("UI stuff")]
    public TextMeshProUGUI infoText;

    void Start()
    {
        SpawnCurrentPattern();
    }
    //have a default pattern and prefab to spawn on start

    //listen for button presses to change prefab and pattern
    
    //change this to switch case for efficiency?
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
    //next pattern
    public void NextPattern()
    {
        _currentPatternIndex = (_currentPatternIndex + 1) % patterns.Count;
        SpawnCurrentPattern();
    }
    //previous pattern
    public void PreviousPattern()
    {
        _currentPatternIndex = (_currentPatternIndex - 1 + patterns.Count) % patterns.Count;
        SpawnCurrentPattern();
    }
    //next prefab
    public void NextPrefab()
    {
        _currentPrefabIndex = (_currentPrefabIndex + 1) % patterns.Count;
        SpawnCurrentPattern();
    }
    //previous prefab
    public void PreviousPrefab()
    {
        _currentPrefabIndex = (_currentPrefabIndex - 1 ) % patterns.Count;
        SpawnCurrentPattern();
    }


    //Change pattern or prefab
    private void SpawnCurrentPattern()
    {
        // 'unalive' previously spawned objects
        foreach (var obj in _spawnedObjects)
        {
            Destroy(obj);
        }
        _spawnedObjects.Clear();

        //get new info
        var positions = patterns[_currentPatternIndex].GetPositions(count, spacing);

        //spawn new objects based on pattern
        foreach (var pos in positions)
        {
            var obj = Instantiate(prefabs[_currentPrefabIndex], pos, Quaternion.identity, transform);
            _spawnedObjects.Add(obj);
        }

        UpdateUI();
    }
    //update ui
    private void UpdateUI()
    {
        //TODO: find less expensive way to do this
        if (infoText != null)
        {
            string patternName = patterns[_currentPatternIndex].name;
            string prefabName = prefabs[_currentPrefabIndex].name;
            infoText.text = $"Pattern: {patternName}\nPrefab: {prefabName}";
        }
    }
}

    //error handling where?

