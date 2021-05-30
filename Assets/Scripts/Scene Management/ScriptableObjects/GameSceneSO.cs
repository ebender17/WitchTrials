using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// This class contains common elements to all games scenes (locations, menus, managers)
/// </summary>
public class GameSceneSO : DescriptionBaseSO
{
    public GameSceneType sceneType;
    public AssetReference sceneRefernce; //Used at runtime to load the scene from the right AssetBundle

    /*
     * Used by the SceneSelector tool to discern what type of scene it needs to load
     */
    public enum GameSceneType
    {
        //Playable scenes 
        Location, 
        Menu, 

        //Special scenes 
        Initialization, 
        PersistentManagers,
        Gameplay

    }
   
}
