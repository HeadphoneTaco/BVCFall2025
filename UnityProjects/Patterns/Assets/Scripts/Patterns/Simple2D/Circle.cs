using System.Collections.Generic;
using Patterns.Base;
using UnityEngine;

[CreateAssetMenu(menuName = "Patterns/2D/Circle")]
public class Circle : PatternBehaviour
{
    public override List<Vector3> GetPositions(int count, float spacing) {
        var positions = new List<Vector3>();
        float radius = spacing * (count / (2 * Mathf.PI)); // keeps density manageable

        for (int i = 0; i < count; i++) {
            float angle = i * Mathf.PI * 2f / count;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            positions.Add(new Vector3(x, 0, z));
        }
        return positions;
    }
}
