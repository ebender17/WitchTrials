using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles game logic. 
/// </summary>

public class GameManager : MonoBehaviour
{
    [Header("Broadcasting on channels")]
    [SerializeField] private GameResultChannelSO _openUIGameEvent = default;

    [Header("Listening on channels")]
    [SerializeField] private GameResultChannelSO _gameResultEvent = default;

    private bool gameHasEnded = false;

    private void OnEnable()
    {
        //picked up from Player Controller when game is won or death occurs
        if (_gameResultEvent != null)
            _gameResultEvent.OnEventRaised += HandleGameResult;
    }

    private void OnDisable()
    {
        if (_gameResultEvent != null)
            _gameResultEvent.OnEventRaised -= HandleGameResult;
    }

    private void HandleGameResult(bool isWon, string playerScore)
    {
        Debug.Log("Handle Game Result Triggered");
        //picked up by UI Manager
        _openUIGameEvent.RaiseEvent(isWon, playerScore);
    }
}
