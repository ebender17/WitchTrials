using System;
using UnityEngine;

/// <summary>
/// A collection of audio clips that are played in parallel and support randomization.
/// </summary>
[CreateAssetMenu(fileName = "newAudioCueSO", menuName ="Audio/Audio Cue")]
public class AudioCueSO : ScriptableObject
{
    public bool looping = false;
    [SerializeField] private AudioClipsGroup[] _audioClipsGroups = default;

    public AudioClip[] GetClips()
    {
        int numberOfClips = _audioClipsGroups.Length;
        AudioClip[] resultingClips = new AudioClip[numberOfClips]; 

        for(int i = 0; i < numberOfClips; i++)
        {
            resultingClips[i] = _audioClipsGroups[i].GetNextClip();
        }

        return resultingClips; 
    }
}

/// <summary>
/// Represents a group of AudioClips that can be treated as one, and provides automatic randomization or sequencing based on the <c> SequenceMode </c> value.
/// </summary>
[Serializable]
public class AudioClipsGroup
{
    public SequenceMode sequenceMode = SequenceMode.RandomNoImmediateRepeat;
    public AudioClip[] audioClips;

    private int _nextClipToPlay = -1;
    private int _lastClipPlayed = -1; 

    /// <summary>
    /// Chooses the next clip in the sequence, either following the order or randomly.
    /// </summary>
    /// <returns></returns>
    public AudioClip GetNextClip()
    {
        if (audioClips.Length == 1)
            return audioClips[0]; 

        if(_nextClipToPlay == -1)
        {
            //Index needs to be initialized: 0 if Sequential, random if otherwise
            _nextClipToPlay = (sequenceMode == SequenceMode.Sequential) ? 0 : UnityEngine.Random.Range(0, audioClips.Length);
        }
        else
        {
            //Select next clip index based on appropriate SequenceMode
            switch(sequenceMode)
            {
                case SequenceMode.Random:
                    _nextClipToPlay = UnityEngine.Random.Range(0, audioClips.Length);
                    break;
                case SequenceMode.RandomNoImmediateRepeat:
                    do
                    {
                        _nextClipToPlay = UnityEngine.Random.Range(0, audioClips.Length);
                    } while (_nextClipToPlay == _lastClipPlayed);
                    break;
                case SequenceMode.Sequential:
                    _nextClipToPlay = (int)Mathf.Repeat(++_nextClipToPlay, audioClips.Length);
                    break;
            }
        }

        _lastClipPlayed = _nextClipToPlay;

        return audioClips[_nextClipToPlay];

    }

}

public enum SequenceMode
{
    Random, //audio clips played randomly, can play same audio clip back to back
    RandomNoImmediateRepeat, //last played audio clip cannot be followed by same audio clip
    Sequential //audio clips played in order
}

