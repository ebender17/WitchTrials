using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAudioSourceEventChannel", menuName = "Events/Int Event Channel")]
public class IntEventChannelSO : EventChannelBaseSO
{
    public IntAction OnEventRaised;

    public void RaiseEvent(int value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }

}

public delegate void IntAction(int value);