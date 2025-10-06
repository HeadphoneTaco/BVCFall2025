using System.Collections.Generic;
using UnityEngine;

namespace FractalVariations
{
    /// <summary>
    /// A set of variations blended together, similar to an Apophysis transform.
    /// </summary>
    [CreateAssetMenu(menuName = "FractalVariations/Variation Set")]
    public class VariationSet : ScriptableObject
    {
        public List<Variation> variations = new();

        public Vector3 ApplyAll(Vector3 point)
        {
            if (variations == null || variations.Count == 0)
                return point;

            Vector3 sum = Vector3.zero;
            foreach (var v in variations)
            {
                if (v == null) continue;
                sum += v.Apply(point);
            }
            return sum / variations.Count;
        }
    }
}