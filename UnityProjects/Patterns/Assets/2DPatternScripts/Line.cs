using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawn Patterns/LinePattern")]
public class LinePattern : PositionList
{
    public override List<Vector3> GetPositions(int count, float spacing)
    {
        var positions = new List<Vector3>();
        for (int i = 0; i < count; i++)
        {
            positions.Add(new Vector3(i * spacing, 0, 0));
        }
        return positions;
    }
}