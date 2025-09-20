using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grid", menuName = "Scriptable Objects/Grid")]
public class Grid : PositionList
{
    //override the abstract
    public override List<Vector3> GetPositions(int count, float spacing)
    {
        //make list of new positions
        var positions = new List<Vector3>();

        //make sure that there are enough prefabs to fill the pattern
        //since this is a grid, square rooting and then rounding should be enough?
        int size = Mathf.CeilToInt(Mathf.Sqrt(count));

        //iterate through number of prefabs to spawn
        //iterate through rows
        for (int i = 0; i < size; i++)
        {
            if (positions.Count >= count)
            {
                return positions;
                //break out if we reach the count
                    
            }
            
            //iterate through columns
            for (int j = 0; j < size; j++)
            {
                //add new positions to list
                positions.Add(new Vector3(j * spacing, 0, i * spacing));
                
                
                if (positions.Count >= count)
                {
                    return positions;
                //break out if we reach the count
                    
                }
            }
        }
        return positions;
    }
}