using System.Collections.Generic;
using UnityEngine;


public class Template : PositionList
{
    [Header("Custom Settings")]
    public float somethinggoeshere;

    //override the abstract
    public override List<Vector3> GetPositions(int count, float spacing)
    {
        //make list of new positions
        var positions = new List<Vector3>();

        //make sure that there are enough prefabs to fill the pattern
        //since this is a ____,
        for (int i = 0; i < count; i++)
        {
            // iterate through ____
            float x = i * spacing;
            float y = 0; 
            float z = 0;

            //add new positions to list
            positions.Add(new Vector3(x, y, z));
        }

        return positions;
    }
}