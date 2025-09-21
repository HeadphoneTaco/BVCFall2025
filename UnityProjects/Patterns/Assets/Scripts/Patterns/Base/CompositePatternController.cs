using UnityEngine;
public abstract class CompositePatternController : ScriptableObject
{
    public SpawnController childPattern;   // the sub-pattern
    public int childCount = 5;            // how many objects in the child
    public float childSpacing = 1f;       // spacing inside the child
}
