using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PatternSpawner))]
public class PatternSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PatternSpawner spawner = (PatternSpawner)target;

        // Default fields
        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Preview Controls", EditorStyles.boldLabel);

        // Toggles
        spawner.ghostPreview = EditorGUILayout.Toggle("Ghost Preview (Gizmos)", spawner.ghostPreview);
        spawner.drawLines = EditorGUILayout.Toggle("Draw Lines (Ghost Mode)", spawner.drawLines);
        spawner.autoSpawn = EditorGUILayout.Toggle("Auto-Spawn (Edit Mode)", spawner.autoSpawn);

        EditorGUILayout.Space();

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
            if (GUILayout.Button("Manual Refresh"))
            {
                spawner.Clear();
                spawner.Spawn();
                ForceSceneUpdate();
            }
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.HelpBox(
                "Ghost Mode: Scene view shows gizmos (spheres + optional lines).\n" +
                "Disable Ghost Preview to spawn real prefabs.",
                MessageType.Info
            );
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
