using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Patterns/Recursive Pattern")]
public class RecursivePattern : PatternBehaviour {
    [Tooltip("The base pattern used at each recursion level.")]
    public PatternBehaviour basePattern;

    [Tooltip("How many recursion levels to spawn.")]
    public int depth = 2;

    [Tooltip("How many objects per branch (per recursion step).")]
    public int branchCount = 3;

    [Tooltip("Scale spacing each recursion step (e.g. 0.5 halves spacing each level).")]
    public float spacingScale = 0.75f;

    public override List<Vector3> GetPositions(int count, float spacing) {
        var positions = new List<Vector3>();
        GenerateRecursive(Vector3.zero, depth, spacing, positions);
        return positions;
    }

    private void GenerateRecursive(Vector3 origin, int level, float spacing, List<Vector3> results) {
        if (level <= 0 || basePattern == null) return;

        var branchPositions = basePattern.GetPositions(branchCount, spacing);
        foreach (var offset in branchPositions) {
            Vector3 pos = origin + offset;
            results.Add(pos);

            // recurse deeper
            GenerateRecursive(pos, level - 1, spacing * spacingScale, results);
        }
    }
}