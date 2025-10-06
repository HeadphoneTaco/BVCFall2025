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
            Vector3 p = new Vector3(
            Mathf.Cos(angle) * radius,
            0f,
            Mathf.Sin(angle) * radius
        );
            // Apply variation warp before storing
            if (variationSet != null)
                p = variationSet.ApplyAll(p);

            positions.Add(p);
        }
        return positions;
    }
}