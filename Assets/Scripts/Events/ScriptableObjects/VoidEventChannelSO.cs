using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Used for events that have no arguments (Ex. exit game).
/// Propogates events with no data attached. 
/// </summary>
[CreateAssetMenu(menuName ="Events/Void Event Channel")]
public class VoidEventChannelSO : EventChannelBaseSO
{
    public UnityAction OnEventRaised; 

    public void RaiseEvent()
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke();
    }
}
