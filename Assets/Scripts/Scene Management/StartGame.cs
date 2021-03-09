using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// Contains logic to call when play button is pressed on main menu. 
/// </summary>
public class StartGame : MonoBehaviour
{
    public LoadEventChannelSO onPlayButtonPress;
    public GameSceneSO[] locationsToLoad;
    public bool showLoadScreen;

    //TODO: Save System Implementation 

    public void OnPlayButtonPress()
    {
        onPlayButtonPress.RaiseEvent(locationsToLoad, showLoadScreen);
    }

}
