using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/*
 * Summary: 
 * This class manages the scene loading and unloading. 
 * 
 */

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private GameSceneSO _gameplayScene = default;

    [Header("Load Events")]
    [SerializeField] private LoadEventChannelSO _loadLocation = default;
    [SerializeField] private LoadEventChannelSO _loadMenu = default;

    [Header("Broadcasting on")]
    [SerializeField] private BoolEventChannelSO _toggleLoadingScreen = default;
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;

    private List<AsyncOperationHandle<SceneInstance>> _loadingOperationHandles = new List<AsyncOperationHandle<SceneInstance>>();
    private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOpHandle;

    //Parameters coming from scene loading requests 
    private GameSceneSO[] _scenesToLoad;
    private GameSceneSO[] _currentlyLoadedScenes = new GameSceneSO[] { };
    private bool _showLoadingScreen;

    private SceneInstance _gameplayManagerSceneInstance = new SceneInstance();

    private void OnEnable()
    {
        _loadLocation.OnLoadingRequested += LoadLocation;
        _loadMenu.OnLoadingRequested += LoadMenu; 
    }

    private void OnDisable()
    {
        _loadLocation.OnLoadingRequested -= LoadLocation;
        _loadMenu.OnLoadingRequested -= LoadMenu; 
    }

    /*
     * Summary: 
     * Loads the location scenes passed as array parameter.
     */
    private void LoadLocation(GameSceneSO[] locationsToLoad, bool showLoadingScreen)
    {
        _scenesToLoad = locationsToLoad;
        _showLoadingScreen = showLoadingScreen; 

        //In case we are coming from the main menu , we need to load the persistent Gameplay manager scene first 
        if(_gameplayManagerSceneInstance.Scene == null || !_gameplayManagerSceneInstance.Scene.isLoaded)
        {
            //StartCoroutine(ProcessGameplaySceneLoading(locationsToLoad, showLoadingScreen));
        }
        else
        {
            //LEFT OFF HERE
        }
    }

    /*
     * Summary:
     * Prepares to load the main menu scene, first removing the Gameplay scene in case the game is coming back from 
     * gamplay to menus. 
     */
    private void LoadMenu(GameSceneSO[] menusToLoad, bool showLoadingScreen)
    {
        _scenesToLoad = menusToLoad;
        _showLoadingScreen = showLoadingScreen;

        //In case we are coming from a location back to manin menu, we need to get rid of persistent Gameplay manager scene 
        if (_gameplayManagerSceneInstance.Scene != null && _gameplayManagerSceneInstance.Scene.isLoaded)
            Addressables.UnloadSceneAsync(_gameplayManagerLoadingOpHandle, true); 

        //TODO

    }
}
