using UnityEngine;
using UnityEngine.Events; 

/*
 * Summary: 
 * Used for events that have no argments (Ex. exit game). 
 */

[CreateAssetMenu(menuName ="Events/Void Event Channel")]
public class VoidEventChannelSO : ScriptableObject
{
    UnityAction OnEventRaised; 

    public void RaiseEvent()
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke();
    }
}
