using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "interactionUIEventChannel", menuName = "Events/Toogle Interaction UI Event Channel")]
public class InteractionUIEventChannelSO : ScriptableObject
{
    public UnityAction<bool, InteractionType> OnEventRaised;

    public void RaiseEvent(bool state, InteractionType interactionType)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(state, interactionType);
    }


}
