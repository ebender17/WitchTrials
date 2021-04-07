using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Used for events to toggle the interaction UI with bool and interaction type.
/// </summary>
[CreateAssetMenu(fileName = "interactionUIEventChannel", menuName = "Events/Toogle Interaction UI Event Channel")]
public class InteractionUIEventChannelSO : EventChannelBaseSO
{
    public UnityAction<bool, InteractionType> OnEventRaised;

    public void RaiseEvent(bool state, InteractionType interactionType)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(state, interactionType);
    }


}
