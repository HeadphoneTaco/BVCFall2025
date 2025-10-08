using Patterns.Base;
using Patterns.Variations;
using Spawner;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PatternSpawner))]
public class PatternSpawnerEditor : Editor
{
    private void DrawPatternInspector(PatternBehaviour pattern, string label = null)
    {
        if (pattern == null) return;

        EditorGUILayout.Space();
        if (!string.IsNullOrEmpty(label))
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
        else
            EditorGUILayout.LabelField($"Pattern: {pattern.name}", EditorStyles.boldLabel);

        SerializedObject patternSo = new SerializedObject(pattern);
        SerializedProperty iterator = patternSo.GetIterator();

        bool enterChildren = true;
        while (iterator.NextVisible(enterChildren))
        {
            if (iterator.name == "m_Script") continue; // skip the script reference

            EditorGUILayout.PropertyField(iterator, true);

            // If this property is a PatternBehaviour reference, recurse
            if (iterator.propertyType == SerializedPropertyType.ObjectReference)
            {
                var obj = iterator.objectReferenceValue as PatternBehaviour;
                if (obj != null)
                {
                    EditorGUI.indentLevel++;
                    DrawPatternInspector(obj, $"Sub-pattern ({iterator.displayName})");
                    EditorGUI.indentLevel--;
                }
            }

            enterChildren = false;
        }

        if (patternSo.ApplyModifiedProperties())
        {
            EditorUtility.SetDirty(pattern);
        }
    }

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
        EditorGUILayout.LabelField("Gizmo Preview", EditorStyles.boldLabel);

        spawner.gizmoSphereSize = EditorGUILayout.Slider("Sphere Size", spawner.gizmoSphereSize, 0.05f, 1f);
        spawner.lineMode = (PatternSpawner.LineMode)EditorGUILayout.EnumPopup("Line Mode", spawner.lineMode);

        if (GUILayout.Button("Refresh Gizmos"))
        {
            SceneView.RepaintAll();
        }

        
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
        // --- Inline Pattern Inspector ---
        if (spawner.patternBucket != null && spawner.patternBucket.Items.Length > 0)
        {
            var pattern = spawner.patternBucket.Items[spawner.currentPatternIndex];
            if (pattern != null)
            {
                DrawPatternInspector(pattern);

                if (spawner.autoSpawn && !spawner.ghostPreview)
                {
                    spawner.Clear();
                    spawner.Spawn();
                }
                SceneView.RepaintAll();
            }
        }
        
        // --- Variation Weight Controls ---
        if (spawner.patternBucket != null && spawner.patternBucket.Items.Length > 0)
        {
            var pattern = spawner.patternBucket.Items[spawner.currentPatternIndex];
            if (pattern != null && pattern.variationSet != null)
            {
                var set = pattern.variationSet;
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Variation Weights", EditorStyles.boldLabel);

                SerializedObject setSo = new SerializedObject(set);
                SerializedProperty variationsProp = setSo.FindProperty("variations");

                for (int i = 0; i < variationsProp.arraySize; i++)
                {
                    SerializedProperty element = variationsProp.GetArrayElementAtIndex(i);
                    var v = element.objectReferenceValue as Variation;
                    if (v == null) continue;

                    v.weight = EditorGUILayout.Slider(v.name, v.weight, 0f, 5f);
                }

                if (setSo.ApplyModifiedProperties())
                {
                    EditorUtility.SetDirty(set);
                    SceneView.RepaintAll();
                }
            }
        }
        spawner.randomizePrefabs = EditorGUILayout.Toggle("Randomize Prefabs", spawner.randomizePrefabs);
        spawner.useSeed = EditorGUILayout.Toggle("Use Seed", spawner.useSeed);
        if (spawner.useSeed)
            spawner.randomSeed = EditorGUILayout.IntField("Random Seed", spawner.randomSeed);
        
    }
    

    private void ForceSceneUpdate()
    {
        SceneView.RepaintAll();
        EditorUtility.SetDirty(target);
    }
}
