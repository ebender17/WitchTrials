using UnityEngine;

/*
 * Summary:
 * Base class for Scriptable Objects that need a public description field. 
 */
public class DescriptionBaseSO : SerialiazableScriptableObject
{
    [TextArea]
    public string description; 
}
