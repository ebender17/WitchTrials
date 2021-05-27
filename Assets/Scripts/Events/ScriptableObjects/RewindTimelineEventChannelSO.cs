using UnityEngine;

[CreateAssetMenu(fileName = "newRewindEventChannel", menuName = "Events/Rewind Timeline Event Channel")]
public class RewindTimelineEventChannelSO : EventChannelBaseSO
{
    public RewindTimelineAction OnEventRaised;

    public void RaiseEvent(float value1, float value2)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value1, value2);
    }
}
public delegate void RewindTimelineAction(float rewindTime, float advanceTime);
