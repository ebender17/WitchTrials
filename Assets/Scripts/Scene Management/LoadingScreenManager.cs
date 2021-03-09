using UnityEngine;

/// <summary>
/// Handles loading screen. 
/// </summary>
public class LoadingScreenManager : MonoBehaviour
{
    [Header("Loading Screen Event")]
    [SerializeField] private BoolEventChannelSO _ToggleLoadingScreen = default;

    [Header("Loading Screen")]
    public GameObject loadingScreen;

    private void OnEnable()
    {
        if(_ToggleLoadingScreen != null)
        {
            _ToggleLoadingScreen.OnEventRaised += ToggleLoadingScreen;
        }
    }

    private void OnDisable()
    {
        if(_ToggleLoadingScreen != null)
        {
            _ToggleLoadingScreen.OnEventRaised += ToggleLoadingScreen; //Check this
        }
    }
    private void ToggleLoadingScreen(bool state)
    {
        loadingScreen.SetActive(state);
    }
}
