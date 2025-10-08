using System.Collections.Generic;
using Patterns.Base;
using UnityEngine;

namespace Patterns.Simple3D
{
    [CreateAssetMenu(menuName = "Patterns/3D/Cube")]
    public class Cube : PatternBehaviour
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
}