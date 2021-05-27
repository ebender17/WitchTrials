using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Used for Events that have an int arguement. 
/// </summary>
[CreateAssetMenu(menuName = "Events/Float Event Channel")]
public class FloatEventChannelSO : EventChannelBaseSO
{
    public UnityAction<float> OnEventRaised;
    public void RaiseEvent(float value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
