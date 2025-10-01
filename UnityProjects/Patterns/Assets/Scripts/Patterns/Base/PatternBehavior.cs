using UnityEngine;
using System.Collections.Generic;


public abstract class PatternBehaviour : ScriptableObject
{
    public abstract List<Vector3> GetPositions(int count, float spacing);
}

/* Example of a derived class implementing a pattern


    //override the abstract
    public override List<Vector3> GetPositions(int count, float spacing)
    {
        //make list of new positions
        var positions = new List<Vector3>();

        //iterate through ____
        if (positions.Count >= count)
            {
                return positions;
                //break out if we reach the count

            }

                //iterate through _____

                //add new positions to list
                positions.Add(new Vector3(i * spacing, 0, k * spacing));

                if (positions.Count >= count)
                    {
                        return positions;
                        //break out if we reach the count

                    }
                    return positions;
    }

    */