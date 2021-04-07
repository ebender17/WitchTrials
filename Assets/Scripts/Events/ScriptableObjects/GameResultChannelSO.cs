using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Used to pass data regarding result of game and player's final score.
/// </summary>
[CreateAssetMenu(fileName = "gameUIEventChannel", menuName ="Events/ Game Result Data Channel")]
public class GameResultChannelSO : EventChannelBaseSO
{
    public UnityAction<bool, string> OnEventRaised;

    public void RaiseEvent(bool isWon, string playerScore)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(isWon, playerScore);
    }
}
