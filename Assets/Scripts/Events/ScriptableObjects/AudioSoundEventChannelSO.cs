using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioSoundEventChannel", menuName = "Events/Audio Sound Event Channel")]
public class AudioSoundEventChannelSO : EventChannelBaseSO
{
    public AudioSoundEventAction OnEventRaised;

    public void RaiseEvent(Sound sound)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(sound);
    }
}

public delegate void AudioSoundEventAction(Sound sound);
