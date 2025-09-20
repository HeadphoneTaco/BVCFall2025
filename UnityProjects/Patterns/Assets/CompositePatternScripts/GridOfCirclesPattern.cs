using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawn Patterns/Grid of Circles")]
public class CompositePatternScripts : CompositePatternController, PositionList
{
    public int gridSize = 3;
    public float gridSpacing = 10f;

    public override List<Vector3> GetPositions(int count, float spacing)
    {
        var positions = new List<Vector3>();

        for (int gx = 0; gx < gridSize; gx++)
        {
            for (int gz = 0; gz < gridSize; gz++)
            {
                Vector3 offset = new Vector3(gx * gridSpacing, 0, gz * gridSpacing);

                if (childPattern != null)
                {
                    var childPositions = childPattern.GetPositions(childCount, childSpacing);
                    foreach (var pos in childPositions)
                    {
                        positions.Add(pos + offset);
                        if (positions.Count >= count) return positions;
                    }
                }
            }
        }

        return positions;
    }
}
