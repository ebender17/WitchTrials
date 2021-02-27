using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Used for Events that have a boolean argument (ex. toggling a UI interface). 
/// </summary>
[CreateAssetMenu(menuName = "Events/Bool Event Channel")]
public class BoolEventChannelSO : EventChannelBaseSO
{
    public UnityAction<bool> OnEventRaised; 
    public void RaiseEvent(bool value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
