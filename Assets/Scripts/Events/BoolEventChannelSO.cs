using UnityEngine;
using UnityEngine.Events; 

/*
 * Summary:
 * Used for Events that have a boolean argument (ex. toggling a UI interface). 
 */

[CreateAssetMenu(menuName = "Events/Bool Event Channel")]
public class BoolEventChannelSO : ScriptableObject
{
    public UnityAction<bool> OnEventRaised; 
    public void RaiseEvent(bool value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
