using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawn Patterns/SpiralPattern")]
public class SpiralPattern : SimplePatternController
{
    public float radiusStep = 0.5f;
    public float angleStep = 15f; // degrees between objects

    public override List<Vector3> GetPositions(int count, float spacing)
    {
        var positions = new List<Vector3>();
        float angle = 0f;
        float radius = 0f;

        for (int i = 0; i < count; i++)
        {
            float rad = angle * Mathf.Deg2Rad;
            positions.Add(new Vector3(Mathf.Cos(rad) * radius, 0, Mathf.Sin(rad) * radius));

            angle += angleStep;
            radius += radiusStep * spacing;
        }

        return positions;
    }
}