using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Patterns/Composite/SomethingOfAnotherThing")]
public class SomethingOfAnotherThing : PatternBehaviour {
    public PatternBehaviour parentPattern; // e.g. Spiral
    public PatternBehaviour childPattern;  // e.g. Grid
    public int childCount = 4;

    public override List<Vector3> GetPositions(int count, float spacing) {
        var positions = new List<Vector3>();

        if (parentPattern == null || childPattern == null) return positions;

        // Parent controls the centers
        var parents = parentPattern.GetPositions(count, spacing);

        foreach (var center in parents) {
            var children = childPattern.GetPositions(childCount, spacing * 0.5f);
            foreach (var offset in children) {
                positions.Add(center + offset);
            }
        }
        return positions;
    }
}