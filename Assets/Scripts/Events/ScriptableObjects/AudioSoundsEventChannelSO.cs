using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioSoundEventChannel", menuName = "Events/Audio Sounds Event Channel")]
public class AudioSoundsEventChannelSO : EventChannelBaseSO
{
    public AudioSoundsEventAction OnEventRaised;

    public void RaiseEvent(Sound[] sound)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(sound);
    }

}
public delegate void AudioSoundsEventAction(Sound[] sound);
