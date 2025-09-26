using System.Collections.Generic;
using Patterns.Base;
using UnityEngine;
[CreateAssetMenu(menuName = "Spawn Patterns/HelixPattern")]
public class HelixPattern : SimplePatternController
{
    public float radius = 5f;
    public float heightStep = 0.5f;

    public override List<Vector3> GetPositions(int count, float spacing)
    {
        var positions = new List<Vector3>();

        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2 / spacing; // wrap around
            float x = Mathf.Cos(angle) * radius;
            float y = i * heightStep;
            float z = Mathf.Sin(angle) * radius;

            positions.Add(new Vector3(x, y, z));
        }

        return positions;
    }
}