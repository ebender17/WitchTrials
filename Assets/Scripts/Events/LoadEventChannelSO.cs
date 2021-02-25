using UnityEngine;
using UnityEngine.Events; 

/*
 * Summary: 
 * This class is used for scene loading events. 
 * Takes an array of the scenes we want to laod and a bool to specify if we want to show a loading screen. 
 */
public class LoadEventChannelSO : EventChannelBaseSO
{
    public UnityAction<GameSceneSO[], bool> OnLoadingRequested; 

    public void RaiseEvent(GameSceneSO[] locationsToLoad, bool showLoadingScreen = false)
    {
        if(OnLoadingRequested != null)
        {
            OnLoadingRequested.Invoke(locationsToLoad, showLoadingScreen); 
        }
        else
        {
            Debug.LogWarning("A Scene loading was requested, but no one picked it up." +
                "Check why there is no SceneLoader already present," +
                "and make sure it is listening on this Load Event channel.");
        }
    }
}
