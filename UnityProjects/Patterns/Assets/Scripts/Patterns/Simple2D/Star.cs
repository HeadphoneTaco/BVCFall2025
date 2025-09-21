using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawn Patterns/StarPattern")]
public class StarPattern : SimplePatternController
{
    public int points = 5;
    public float radius = 5f;

    public override List<Vector3> GetPositions(int count, float spacing)
    {
        var positions = new List<Vector3>();
        for (int i = 0; i < count; i++)
        {
            int pointIndex = i % points;
            float angle = (pointIndex * 2 * Mathf.PI) / points;
            float r = (i / points) % 2 == 0 ? radius : radius * 0.5f; // alternate long/short
            positions.Add(new Vector3(Mathf.Cos(angle) * r, 0, Mathf.Sin(angle) * r));
        }
        return positions;
    }
}
