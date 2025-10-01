using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PatternSpawner))]
public class PatternSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector first (shows buckets + settings)
        DrawDefaultInspector();

        PatternSpawner spawner = (PatternSpawner)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Pattern Preview Controls", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("← Previous Pattern"))
        {
            spawner.PreviousPattern();
            ForceSceneUpdate();
        }
        if (GUILayout.Button("Next Pattern →"))
        {
            spawner.NextPattern();
            ForceSceneUpdate();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("← Previous Prefab"))
        {
            spawner.PreviousPrefab();
            ForceSceneUpdate();
        }
        if (GUILayout.Button("Next Prefab →"))
        {
            spawner.NextPrefab();
            ForceSceneUpdate();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        if (GUILayout.Button("Preview Pattern"))
        {
            spawner.Spawn();
            ForceSceneUpdate();
        }

        if (GUILayout.Button("Clear Preview"))
        {
            spawner.Clear();
            ForceSceneUpdate();
        }
    }
    
 

    

    private void ForceSceneUpdate()
    {
        // Make Unity repaint the Scene view so preview updates immediately
        SceneView.RepaintAll();
        EditorUtility.SetDirty(target);
    }
}