using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private float mapSize = 50f; // Size of the area to place trees
    [SerializeField] private int numberOfTrees;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (treePrefab == null)
        {
            Debug.LogError("treePrefab needs to be set");
            return;

        }

        if (!treePrefab.CompareTag(nameof(Tree)))
        {
            Debug.LogWarning("Tree Prefab should have the 'Tree' tag set!");
        }
        //Start placing trees
        PlaceTrees();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// Iterate through the number of trees to place, and report their positions.
    /// </summary>
    private void PlaceTrees()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            Vector3 position = GenerateRandomPosition();
            Instantiate(treePrefab, position, Quaternion.identity);
            Debug.Log($"Placed tree at {i} of {numberOfTrees}!: {position}");
        }
    }
    ///
    /// <summary>
    /// Generates a random position within the defined area.
    /// </summary>
    /// <returns>A Vector3 representing the random position.</returns>
    private Vector3 GenerateRandomPosition()
    {
        float randomX = Random.Range(-mapSize, mapSize); //(-50 to 50)
        float randomZ = Random.Range(-mapSize, mapSize); //(-50 to 50)
        
        //return with no height change
        return new Vector3(randomX, 0f, randomZ);
        
    }
    
}
