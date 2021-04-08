using UnityEngine;

[CreateAssetMenu(fileName ="newAudioCueEventChannel", menuName = "Audio/AudioCue Event Channel")]
public class AudioCueEventChannelSO : EventChannelBaseSO
{
    public AudioCuePlayAction OnAudioCuePlayRequested;
    public AudioCueStopAction OnAudioCueStopRequested;
    public AudioCueFinishAction OnAudioCueFinishRequested;

    public AudioCueKey RaisePlayEvent(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 positionInSpace = default)
    {
        AudioCueKey audioCueKey = AudioCueKey.Invalid;

        if (OnAudioCuePlayRequested != null)
        {
            audioCueKey = OnAudioCuePlayRequested.Invoke(audioCue, audioConfiguration, positionInSpace);
        }
        else
        {
            Debug.LogWarning("An AudioCue play event was requested, but nobodyt picked it up." +
                "Check why there is no AudioManager already loaded," +
                "and make sure it's listening on this AudioCue Event channel.");
        }
        return audioCueKey;
    }

    public bool RaisedStopEvent(AudioCueKey audioCueKey)
    {
        bool requestSucceed = false;

        if(OnAudioCueStopRequested != null)
        {
            requestSucceed = OnAudioCueStopRequested.Invoke(audioCueKey);
        }
        else
        {
            Debug.LogWarning("An AudioCue stop event was requested, but nobody picked it up." +
                "Check why there is no AudioManager already loaded, " +
                "and make sure it's listening on thios AudioCue Event channel.");
        }

        return requestSucceed;
    }

    public bool RaisedFinishEvent(AudioCueKey audioCueKey)
    {
        bool requestSucceed = false;

        if (OnAudioCueFinishRequested != null)
        {
            requestSucceed = OnAudioCueFinishRequested.Invoke(audioCueKey);
        }
        else
        {
            Debug.LogWarning("An AudioCue finish event was requested, but nobody picked it up." +
                "Check why there is no AudioManager already loaded, " +
                "and make sure it's listening on thios AudioCue Event channel.");
        }

        return requestSucceed;
    }
}

public delegate AudioCueKey AudioCuePlayAction(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 positionInSpace);
public delegate bool AudioCueStopAction(AudioCueKey emitterKey);
public delegate bool AudioCueFinishAction(AudioCueKey emitterKey);
