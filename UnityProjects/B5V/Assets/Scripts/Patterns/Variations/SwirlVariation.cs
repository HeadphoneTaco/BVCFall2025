using UnityEngine;

namespace Patterns.Variations
{
    [CreateAssetMenu(menuName = "FractalVariations/Swirl")]
    public class SwirlVariation : Variation
    {
        public override Vector3 Apply(Vector3 p)
        {
            float r2 = p.sqrMagnitude;
            float sinr = Mathf.Sin(r2);
            float cosr = Mathf.Cos(r2);
            //p.x * cosr + p.y * sinr
            //put this y to get a 3D swirl
            return new Vector3(p.x * sinr - p.y * cosr, p.x * cosr + p.y * sinr, p.z
            ) * weight;
        }
    }
}