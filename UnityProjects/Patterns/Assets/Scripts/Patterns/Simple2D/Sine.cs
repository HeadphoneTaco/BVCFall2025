using System.Collections.Generic;
using Patterns.Base;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawn Patterns/SinePattern")]
public class SinePattern : SimplePatternController
{
    public float amplitude = 2f;
    public float wavelength = 2f;

    public override List<Vector3> GetPositions(int count, float spacing)
    {
        var positions = new List<Vector3>();
        for (int i = 0; i < count; i++)
        {
            float x = i * spacing;
            float y = 0;
            float z = Mathf.Sin(i / wavelength) * amplitude;
            positions.Add(new Vector3(x, y, z));
        }
        return positions;
    }
}