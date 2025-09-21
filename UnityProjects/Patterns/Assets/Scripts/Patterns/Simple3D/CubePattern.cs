using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Spawn Patterns/CubePattern")]
public class CubePattern : PositionList
{
    public override List<Vector3> GetPositions(int count, float spacing)
    {
        var positions = new List<Vector3>();
        int cubeSize = Mathf.CeilToInt(Mathf.Pow(count, 1f / 3f)); // cube root

        for (int x = 0; x < cubeSize; x++)
        {
            for (int y = 0; y < cubeSize; y++)
            {
                for (int z = 0; z < cubeSize; z++)
                {
                    positions.Add(new Vector3(x * spacing, y * spacing, z * spacing));
                    if (positions.Count >= count) return positions;
                }
            }
        }
        return positions;
    }
}