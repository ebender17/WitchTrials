using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;

    [Header("Broadcasting on channels")]
    [SerializeField] private BoolEventChannelSO _togglePauseMenuUI;

    private static bool GameIsPaused;

    private void OnEnable()
    {
        _inputReader.openMenuEvent += OnMenuButtonPress;
    }

    private void OnDisable()
    {
        _inputReader.openMenuEvent -= OnMenuButtonPress;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameIsPaused = false;
        
    }

    private void OnMenuButtonPress()
    {
        if(GameIsPaused)
        {
            Resume();
        } else
        {
            Pause();
        }
        
    }

    public void Resume()
    {
        _inputReader.EnableGameplayInput();
        _togglePauseMenuUI.RaiseEvent(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        _inputReader.EnableMenuInput();
        _togglePauseMenuUI.RaiseEvent(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
