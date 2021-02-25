using UnityEngine;

#if UNITY_EDITOR 
using UnityEditor;
#endif

public class SerialiazableScriptableObject : ScriptableObject
{
    [SerializeField, HideInInspector]
    private string _guid; //TODO: documentation on guid 
    public string Guid => _guid;

    //TODO: documentation
#if UNITY_EDITOR
    private void OnValidate()
    {
        var path = AssetDatabase.GetAssetPath(this);
        _guid = AssetDatabase.AssetPathToGUID(path); 
    }
#endif

}
