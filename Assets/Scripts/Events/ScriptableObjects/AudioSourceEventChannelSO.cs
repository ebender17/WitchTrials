using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAudioSourceEventChannel", menuName = "Events/AudioSource Event Channel")]
public class AudioSourceEventChannelSO : EventChannelBaseSO
{
    public AudioSourcePlayAction OnAudioSourcePlayRequested;

    public void RaisePlayEvent(AudioClipName audioClipName)
    {
        if (OnAudioSourcePlayRequested != null)
            OnAudioSourcePlayRequested.Invoke(audioClipName);
    }
}

public delegate void AudioSourcePlayAction(AudioClipName audioClipName);
