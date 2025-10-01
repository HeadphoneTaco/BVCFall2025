using System.Collections.Generic;
using Patterns.Base;
using UnityEngine;

[CreateAssetMenu(menuName = "Patterns/2D/Circle")]
public class Circle : PatternBehaviour
{
    public float radius = 5f;

    //override the abstract
    public override List<Vector3> GetPositions(int count, float spacing)
    {
        //make list of new positions
        var positions = new List<Vector3>();
        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2 / count;
            positions.Add(new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius));
            //add new positions to list
        }
        return positions;
    }
}
