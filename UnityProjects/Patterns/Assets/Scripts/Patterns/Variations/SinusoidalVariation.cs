using UnityEngine;

[CreateAssetMenu(menuName = "FractalVariations/Sinusoidal")]
public class SinusoidalVariation : FractalVariations.Variation
{
    public override Vector3 Apply(Vector3 p)
    {
        return new Vector3(
            Mathf.Sin(p.x),
            Mathf.Sin(p.y),
            Mathf.Sin(p.z)
        ) * weight;
    }
}