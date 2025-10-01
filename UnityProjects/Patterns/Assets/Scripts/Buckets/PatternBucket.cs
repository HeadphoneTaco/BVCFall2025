using UnityEngine;
using CoreUtils.AssetBuckets;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Buckets/Pattern Bucket")]
public class PatternBucket : GenericAssetBucket<PatternBehaviour>
{
#if UNITY_EDITOR
    // Called whenever the ScriptableObject is modified or reloaded
    private void OnValidate()
    {
        RefreshPatterns();
    }

    private void RefreshPatterns()
    {
        // Clear out old refs
        EDITOR_Clear();

        // Limit search to Assets/Patterns/
        string[] guids = AssetDatabase.FindAssets("t:PatternBehaviour", new[] { "Assets/ScriptableObjects/Patterns" });
        var found = new System.Collections.Generic.List<Object>();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<PatternBehaviour>(path);
            if (asset != null) found.Add(asset);
        }

        // Add into bucket
        EDITOR_ForceAdd(found);
    }
#endif
}