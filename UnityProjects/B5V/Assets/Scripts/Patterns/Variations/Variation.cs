using UnityEngine;

namespace Patterns.Variations
{
    /// <summary>
    /// Base class for all variations (Apophysis-style transforms).
    /// Each variation maps a point (x,y,z) to a new (x',y',z').
    /// </summary>
    public abstract class Variation : ScriptableObject
    {
        [Range(0f, 2f)] public float weight = 1f;
        [ColorUsage(true, true)] public Color previewColor = Color.cyan;

#if UNITY_EDITOR
        [HideInInspector] public bool editorFoldout = true;
#endif
        /// <summary>
        /// Apply the variation's transformation to the input point.
        /// </summary>
        public abstract Vector3 Apply(Vector3 p);
    }
}
