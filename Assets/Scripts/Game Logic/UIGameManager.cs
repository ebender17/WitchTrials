using UnityEngine;
using TMPro;

/// <summary>
/// Attach to UIGame 
/// </summary>
public class UIGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameResultText = default;
    [SerializeField] private TextMeshProUGUI _playerScoreText = default;
    public void SetGameResults(string gameResult, string playerScore)
    {
        _gameResultText.SetText($"You {gameResult}!");
        _playerScoreText.SetText($"Apples collected: {playerScore}");
    }
}
