#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace FractalVariations.EditorTools
{
    [CustomEditor(typeof(VariationSet))]
    public class VariationSetEditor : Editor
    {
        private static int _previewResolution = 10;
        private static float _previewSpacing = 1.0f;
        private static float _previewSize = 0.05f;
        private static bool _showLines = true;

        private void OnEnable()
        {
            SceneView.duringSceneGui += DrawPreviewInScene;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= DrawPreviewInScene;
        }

        private void DrawPreviewInScene(SceneView sceneView) {
            if (Selection.activeObject != target)
                return;

            VariationSet set = (VariationSet)target;
            if (set == null || set.variations == null || set.variations.Count == 0)
                return;

            int half = _previewResolution / 2;
            for (int x = -half; x <= half; x++) {
                for (int z = -half; z <= half; z++) {
                    Vector3 p = new Vector3(x * _previewSpacing, 0, z * _previewSpacing);
                    Vector3 warpedSum = Vector3.zero;
                    float totalWeight = 0f;
                    Color blended = Color.black;
                    // Draw each variation’s contribution separately
                    foreach (var v in set.variations) {
                        if (v == null) continue;
                        float w = v.weight;
                        Vector3 wPos = v.Apply(p) * w;
                        warpedSum += wPos;
                        totalWeight += w;
                        blended += v.previewColor * w; }
                    if (totalWeight < 1e-5f) continue;
                    Vector3 finalPos = warpedSum / totalWeight;
                    Handles.color = blended / totalWeight;
                    Handles.DrawWireDisc(finalPos, Vector3.up, _previewSize);
                    if (_showLines) Handles.DrawLine(p, finalPos);
                }
            }
            

            SceneView.RepaintAll();
        }

            // rotation animation
            //TODO: Make rotation optional with a toggle in the inspector, this goes in drawpreviewinscene
            //Handles.matrix = Matrix4x4.Rotate(Quaternion.Euler(0, Time.realtimeSinceStartup * 10f, 0));

        public override void OnInspectorGUI() {
    VariationSet set = (VariationSet)target;
    serializedObject.Update();

    EditorGUI.BeginChangeCheck();

    EditorGUILayout.LabelField("Variation Set", EditorStyles.boldLabel);
    EditorGUILayout.HelpBox(
        "Edit variations inline. Each one has its own color; the Scene View preview updates live.",
        MessageType.Info
    );

    SerializedProperty variationsProp = serializedObject.FindProperty("variations");

    for (int i = 0; i < variationsProp.arraySize; i++) {
        SerializedProperty element = variationsProp.GetArrayElementAtIndex(i);
        Variation v = element.objectReferenceValue as Variation;

        if (v == null) {
            EditorGUILayout.PropertyField(element);
            continue;
        }

        // Foldout with color bar
        EditorGUILayout.BeginVertical("box");
        GUI.backgroundColor = v.previewColor;
        v.editorFoldout = EditorGUILayout.Foldout(v.editorFoldout, v.name, true);
        GUI.backgroundColor = Color.white;

        if (v.editorFoldout) {
            SerializedObject vSo = new SerializedObject(v);
            SerializedProperty vIter = vSo.GetIterator();
            bool enterChildren = true;
            while (vIter.NextVisible(enterChildren)) {
                if (vIter.name == "m_Script") continue;
                EditorGUILayout.PropertyField(vIter, true);
                enterChildren = false;
            }
            if (vSo.ApplyModifiedProperties()) {
                EditorUtility.SetDirty(v);
                SceneView.RepaintAll();
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
    }

    // Allow adding/removing variations normally
    EditorGUILayout.PropertyField(variationsProp, true);

    // Preview controls
    EditorGUILayout.Space();
    EditorGUILayout.LabelField("Preview Settings", EditorStyles.boldLabel);
    _previewResolution = EditorGUILayout.IntSlider("Grid Resolution", _previewResolution, 4, 20);
    _previewSpacing = EditorGUILayout.Slider("Grid Spacing", _previewSpacing, 0.5f, 2f);
    _previewSize = EditorGUILayout.Slider("Point Size", _previewSize, 0.02f, 0.2f);
    _showLines = EditorGUILayout.Toggle("Show Lines", _showLines);

    if (EditorGUI.EndChangeCheck()) {
        serializedObject.ApplyModifiedProperties();
        SceneView.RepaintAll();
    }
}


    }
}
#endif
