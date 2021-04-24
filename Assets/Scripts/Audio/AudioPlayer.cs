using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClipName clipToPlay;
    [SerializeField] private VoidEventChannelSO _onSceneReady = default;
    [SerializeField] private AudioSourceEventChannelSO _playMusicOn = default;

    private void OnEnable()
    {
        _onSceneReady.OnEventRaised += PlayMusic;
    }

    private void OnDisable()
    {
        _onSceneReady.OnEventRaised -= PlayMusic;
    }

    private void PlayMusic()
    {
        _playMusicOn.RaisePlayEvent(clipToPlay);
    }
}
