using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Channel for broadcasting dialogue line events. 
/// </summary>
[CreateAssetMenu(fileName = "newDialogueLineChannel", menuName = "Events/UI/Dialogue Line Channel")]
public class DialogueLineChannelSO : ScriptableObject
{
    public UnityAction<string, ActorSO> OnEventRaised;
    public void RaiseEvent(string line, ActorSO actor)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(line, actor);
    }
   
}
