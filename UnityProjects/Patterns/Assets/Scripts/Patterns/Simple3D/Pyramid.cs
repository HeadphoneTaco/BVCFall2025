using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Patterns/3D/Pyramid")]
public class Pyramid : PatternBehaviour {
    public override List<Vector3> GetPositions(int count, float spacing) {
        var positions = new List<Vector3>();
        int layers = Mathf.CeilToInt(Mathf.Sqrt(count));

        for (int y = 0; y < layers && positions.Count < count; y++) 
        {
            int layerSize = layers - y; // smaller each layer
            
            for (int x = 0; x < layerSize; x++) 
            {
                for (int z = 0; z < layerSize; z++) 
                {
                    positions.Add(new Vector3(x * spacing, y * spacing, z * spacing));
                    if (positions.Count >= count) return positions;
                }
            }
        }
        return positions;
    }
}