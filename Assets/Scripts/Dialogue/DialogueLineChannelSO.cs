using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "newDialogueLineChannel", menuName = "Events/UI/Dialogue line Channel")]
public class DialogueLineChannelSO : ScriptableObject
{
    public UnityAction<string, ActorSO> OnEventRaised;
    public void RaiseEvent(string line, ActorSO actor)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(line, actor);
    }
   
}
