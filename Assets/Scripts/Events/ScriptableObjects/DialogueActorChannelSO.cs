using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu (fileName = "newDialogueActorChannel", menuName = "Events/Dialogue Actor Channel" )]
public class DialogueActorChannelSO : ScriptableObject
{
    public UnityAction<ActorSO> OnEventRaised;

    public void RaiseEvent(ActorSO actor)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(actor);
    }
}
