using UnityEngine;

/// <summary>
/// Base class for Scriptable Objects that need a public description field. 
/// </summary>
public class DescriptionBaseSO : SerialiazableScriptableObject
{
    [TextArea]
    public string description; 
}
