using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Patterns/3D/Sphere")]
public class Sphere : PatternBehaviour
{
    public float radius = 5f;

    public override List<Vector3> GetPositions(int count, float spacing)
    {
        var positions = new List<Vector3>();
        
        float offset = 2f / count;
        float increment = Mathf.PI * (3f - Mathf.Sqrt(5f)); 

        for (int i = 0; i < count; i++)
        {
            float y = i * offset - 1f + (offset / 2f);
            float r = Mathf.Sqrt(1f - y * y);
            float phi = i * increment;

            float x = Mathf.Cos(phi) * r;
            float z = Mathf.Sin(phi) * r;

            positions.Add(new Vector3(x, y, z) * radius);
        }

        return positions;
    }
}
