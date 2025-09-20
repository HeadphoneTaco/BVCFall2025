using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPatternSO", menuName = "Scriptable Objects/SpawnPatternSO")]
public abstract class SpawnPatternSo : ScriptableObject
{
    public abstract List<Vector3> GetPositions(int count, float spacing);
}
