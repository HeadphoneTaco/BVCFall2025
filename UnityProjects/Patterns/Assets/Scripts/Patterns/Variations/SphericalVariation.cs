using UnityEngine;

[CreateAssetMenu(menuName = "FractalVariations/Spherical")]
public class SphericalVariation : FractalVariations.Variation
{
    public override Vector3 Apply(Vector3 p)
    {
        float r2 = p.sqrMagnitude + 1e-6f;
        return (p / r2) * weight;
    }
}