using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PatternSpawner))]
public class PatternSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PatternSpawner spawner = (PatternSpawner)target;

        // Draw default fields (buckets + settings)
        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Preview Controls", EditorStyles.boldLabel);

        // Toggle between Ghost and Real preview
        spawner.ghostPreview = EditorGUILayout.Toggle("Ghost Preview (Gizmos)", spawner.ghostPreview);

        if (!spawner.ghostPreview)
        {
            EditorGUILayout.BeginHorizontal();
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
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.HelpBox("Ghost Mode: Scene view shows gizmos (wire spheres).\nDisable Ghost Preview to spawn real prefabs.", MessageType.Info);
        }

        EditorGUILayout.Space();
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
    }

    private void ForceSceneUpdate()
    {
        SceneView.RepaintAll();
        EditorUtility.SetDirty(target);
    }
}
