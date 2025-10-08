using UnityEngine;

namespace Patterns.Variations
{
    [CreateAssetMenu(menuName = "FractalVariations/Spherical")]
    public class SphericalVariation : Variation
    {
        public override Vector3 Apply(Vector3 p)
        {
            float r2 = p.sqrMagnitude + 1e-6f;
            return (p / r2) * weight;
        }
    }
}