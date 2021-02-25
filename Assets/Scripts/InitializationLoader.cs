using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/*
 * Summary:
 * This class is responsible for starting the game by loading the persistent managers scene 
 * and raising the event to laod the Main Menu. 
 */
public class InitializationLoader : MonoBehaviour
{

    [Header("Persistent managers Scene")]
    [SerializeField] private GameSceneSO _persistenManagersScene = default;

    [Header("Loading setttings")]
    [SerializeField] private GameSceneSO[] _menuToLoad = default;

    [Header("Broadcasting on")]
    [SerializeField] private AssetReference _menuLoadChannel = default;
    // Start is called before the first frame update
    void Start()
    {
        //Load the persistent managers scene 
        _persistenManagersScene.sceneRefernce.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadEventChannel;

    }

    private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj)
    {
        _menuLoadChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += LoadMainMenu;
    }

    private void LoadMainMenu(AsyncOperationHandle<LoadEventChannelSO> obj)
    {
        LoadEventChannelSO loadEventChannelSO = (LoadEventChannelSO)_menuLoadChannel.Asset;
        loadEventChannelSO.RaiseEvent(_menuToLoad);

        SceneManager.UnloadSceneAsync(0); //Initialization is the only scene in BuildSettings, thus it has index 0 
    }
}
