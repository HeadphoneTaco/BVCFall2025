using System.Collections.Generic;
using Patterns.Base;
using UnityEngine;

[CreateAssetMenu(menuName = "Patterns/2D/Spiral")]
public class Spiral : PatternBehaviour
{
       public float angleStep = Mathf.PI / 1f;  
       public float radiusStep = 0.5f;
    public override List<Vector3> GetPositions(int count, float spacing) {
        var positions = new List<Vector3>();

        for (int i = 0; i < count; i++) {
            float angle = i * angleStep;
            float radius = i * radiusStep;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            positions.Add(new Vector3(x, 0, z));
        }
        return positions;
    }
}