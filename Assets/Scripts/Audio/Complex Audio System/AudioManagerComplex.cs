using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerComplex : MonoBehaviour
{
    [Header("SoundEmitters pool")]
    [SerializeField] private SoundEmitterFactorySO _factory = default;
    [SerializeField] private SoundEmitterPoolSO _pool = default;
    [SerializeField] private int _initialSize = 10;

    [Header("Listening on channels")]
    [SerializeField] private AudioCueEventChannelSO _SFXEventChannel = default; // fired by objects in scene to play SFXs
    [SerializeField] private AudioCueEventChannelSO _musicEventChannel = default; //fired by objects in scene to play Music

    [Header("Audio control")]
    [SerializeField] private AudioMixer audioMixer = default;
    [Range(0f, 1f)]
    [SerializeField] private float _masterVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _musicVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _sfxVolume = 1f;

    private SoundEmitterVault _soundEmitterVault;
    private SoundEmitter _musicSoundEmitter;

    private void Awake()
    {
        //TODO: Get the initial volume levels from the setttings
        _soundEmitterVault = new SoundEmitterVault();

        _pool.Prewarm(_initialSize);
        _pool.SetParent(gameObject.transform);

    }

    private void OnEnable()
    {
        _SFXEventChannel.OnAudioCuePlayRequested += PlayAudioCue;
        _SFXEventChannel.OnAudioCueStopRequested += StopAudioCue;
        _SFXEventChannel.OnAudioCueFinishRequested += FinishAudioCue;

        _musicEventChannel.OnAudioCuePlayRequested += PlayMusicTrack;
        _musicEventChannel.OnAudioCueStopRequested += StopMusicTrack;
    }

    private void OnDestroy()
    {
        _SFXEventChannel.OnAudioCuePlayRequested -= PlayAudioCue;
        _SFXEventChannel.OnAudioCueStopRequested -= StopAudioCue;
        _SFXEventChannel.OnAudioCueFinishRequested -= FinishAudioCue;

        _musicEventChannel.OnAudioCuePlayRequested -= PlayMusicTrack;
        _musicEventChannel.OnAudioCueStopRequested -= StopMusicTrack;
    }

    private void OnValidate()
    {
        if(Application.isPlaying)
        {
            SetGroupVolume("MasterVolume", _masterVolume);
            SetGroupVolume("MusicVolume", _musicVolume);
            SetGroupVolume("SFXVolume", _sfxVolume);
        }

    }

    public void SetGroupVolume(string parameterName, float normalizedVoume)
    {
        bool volumeSet = audioMixer.SetFloat(parameterName, NormalizedToMixerValue(normalizedVoume));
        if (!volumeSet)
            Debug.LogError("The AudioMixer parameter was not found.");
    }

    public float GetGroupVolume(string parameterName)
    {
        if(audioMixer.GetFloat(parameterName, out float rawVolume))
        {
            return MixerValueToNormalized(rawVolume); 
        }
        else
        {
            Debug.LogError("The AudioMixer paramter was not found.");
            return 0f;
        }
    }

    //Both MixerValueNormalized and NormalizedToMixerValue functions are used for easier transformations
    //when using UI sliders normalized format
    private float MixerValueToNormalized(float mixerValue)
    {
        //Assuming range [-80dB to 0dB] becomes [0 to 1]
        return 1f + (mixerValue / 80f);
    }

    private float NormalizedToMixerValue(float normalizedValue)
    {
        //Assuming the range [0 to 1] becomes [-80dB to 0dB] 
        //Doesn't allow values over 0dB
        return (normalizedValue - 1f) * 80f;
    }
    private AudioCueKey PlayMusicTrack(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 positionInSpace)
    {
        float fadeDuration = 2f;
        float startTime = 0f;

        if(_musicSoundEmitter != null && _musicSoundEmitter.IsPlaying())
        {
            AudioClip songToPlay = audioCue.GetClips()[0];
            if (_musicSoundEmitter.GetClip() == songToPlay)
                return AudioCueKey.Invalid;

            //TODO: Music is already playing, need to fade it out
        }

        _musicSoundEmitter = _pool.Request();
        //_musicSoundEmitter.FadeMusicIn(audioCue.GetClips()[0], audioConfiguration, 1f, startTime);
        _musicSoundEmitter.OnSoundFinishedPlaying += StopMusicEmitter;

        return AudioCueKey.Invalid; //No need to return a valid key for music
    }

    private bool StopMusicTrack(AudioCueKey key)
    {
        if (_musicSoundEmitter != null && _musicSoundEmitter.IsPlaying())
        {
            _musicSoundEmitter.Stop();
            return true;
        }
        else
        {
            return false;
        }    
    }

    private void StopMusicEmitter(SoundEmitter soundEmitter)
    {
        soundEmitter.OnSoundFinishedPlaying -= StopMusicEmitter;
        _pool.Return(soundEmitter);
    }

    public AudioCueKey PlayAudioCue(AudioCueSO audioCue, AudioConfigurationSO settings, Vector3 position = default)
    {
        AudioClip[] clipsToPlay = audioCue.GetClips();
        SoundEmitter[] soundEmitterArray = new SoundEmitter[clipsToPlay.Length];

        for(int i = 0; i < clipsToPlay.Length; i++)
        {
            soundEmitterArray[i] = _pool.Request(); 
            if(soundEmitterArray[i] != null)
            {
                soundEmitterArray[i].PlayAudioClip(clipsToPlay[i], settings, audioCue.looping, position);
                if (!audioCue.looping)
                    soundEmitterArray[i].OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;
            }
        }

        return _soundEmitterVault.Add(audioCue, soundEmitterArray);
    }

    public bool FinishAudioCue(AudioCueKey audioCueKey)
    {
        bool isFound = _soundEmitterVault.Get(audioCueKey, out SoundEmitter[] soundEmitters);

        if(isFound)
        {
            for(int i = 0; i < soundEmitters.Length; i++)
            {
                soundEmitters[i].Finish();
                soundEmitters[i].OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;
            }
        }
        else
        {
            Debug.LogWarning("Finshing an AudioCue was requested, but the AudioCue was not found.");
        }

        return isFound;
    }

    public bool StopAudioCue(AudioCueKey audioCueKey)
    {
        bool isFound = _soundEmitterVault.Get(audioCueKey, out SoundEmitter[] soundEmitters); 

        if(isFound)
        {
            for(int i = 0; i < soundEmitters.Length; i++)
            {
                StopAndCleanEmitter(soundEmitters[i]);
            }

            _soundEmitterVault.Remove(audioCueKey);
        }

        return isFound;
    }


    private void OnSoundEmitterFinishedPlaying(SoundEmitter soundEmitter)
    {
        StopAndCleanEmitter(soundEmitter);
    }

    private void StopAndCleanEmitter(SoundEmitter soundEmitter)
    {
        if (!soundEmitter.IsLooping())
            soundEmitter.OnSoundFinishedPlaying -= OnSoundEmitterFinishedPlaying;

        soundEmitter.Stop();
        _pool.Return(soundEmitter); 

        //TODO: _soundEmitterVault.Remove is not called if StopAndClean is called after a finish event
        //How is key removed from the vault?

    }

    
}
