using System.Collections.Generic;
using UnityEngine;

public abstract class SimplePatternController : ScriptableObject
{
    //abstract to be overridden
    public abstract List<Vector3> GetPositions(int count, float spacing);
}

/* Example of a derived class implementing a pattern


    //override the abstract
    public override List<Vector3> GetPositions(int count, float spacing)
    {
        //make list of new positions
        var positions = new List<Vector3>();


        //make sure that there are enough prefabs to fill the pattern
        //since this is a ____,


        //iterate through number of prefabs to spawn
        //iterate through ____


        if (positions.Count >= count)
            {
                return positions;
                //break out if we reach the count
                    
            }



                //iterate through _____

                //add new positions to list
                positions.Add(new Vector3(j * spacing, 0, i * spacing));

                if (positions.Count >= count)
                    {
                        return positions;
                        //break out if we reach the count
                    
                    }
                    return positions;
    }
    
    */
