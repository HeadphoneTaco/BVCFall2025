using System.Collections.Generic;
using Patterns.Base;
using UnityEngine;

[CreateAssetMenu(menuName = "Patterns/2D/Grid")]
public class Grid : PatternBehaviour
{
    //override the abstract
    public override List<Vector3> GetPositions(int count, float spacing)
    {
        //make list of new positions
        var positions = new List<Vector3>();
        int size = Mathf.CeilToInt(Mathf.Sqrt(count));//Square root of count rounded up

        //iterate through rows
        for (int i = 0; i < size; i++)
        {
            if (positions.Count >= count) return positions;
            //break out if we reach the count
            
            //iterate through columns
            for (int j = 0; j < size; j++)
            {
                //add new positions to list
                positions.Add(new Vector3(j * spacing, 0, i * spacing));
                if (positions.Count >= count) return positions;
                //break out if we reach the count
            }
        }
        return positions;
    }
}