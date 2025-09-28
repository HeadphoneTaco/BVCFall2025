using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Patterns/3D/Helix")]
public class Helix : PatternBehaviour
{

    public override List<Vector3> GetPositions(int count, float spacing){
        var positions = new List<Vector3>();
        
        float radius = spacing * 2;
        float heightStep = spacing * 0.5f;

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