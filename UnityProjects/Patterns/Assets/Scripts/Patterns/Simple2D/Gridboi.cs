using System.Collections.Generic;
using Patterns.Base;
using UnityEngine;

namespace Patterns.Simple2D
{
    [CreateAssetMenu(menuName = "Patterns/2D/Gridboi")]
    public class Gridboi : PatternBehaviour
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
                //iterate through columns
                for (int j = 0; j < size; j++)
                {
                    if (positions.Count >= count) break;
                    //break out if we reach the count
            
                    Vector3 p = new Vector3(
                        (i - size / 2f) * spacing,
                        0f, 
                        (j - size / 2f) * spacing
                    );
            
            
                    // Apply variation warp before storing
                    if (variationSet != null)
                        p = variationSet.ApplyAll(p);

                    positions.Add(p);
                }
            }
            return positions;
        }
    }
}