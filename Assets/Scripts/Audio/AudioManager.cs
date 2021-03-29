using System;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public enum AudioClipName
{
    MainTheme,
    PlayerHit,
    EnemyHit
}

[System.Serializable]
public class Sound
{
    public AudioClipName name;

    public AudioMixerGroup audioMixerGroup;

    private AudioSource source;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop = false;
    //public bool playOnAwake = false;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.pitch = pitch;
        source.volume = volume;
        //source.playOnAwake = playOnAwake;
        source.loop = loop;
        source.outputAudioMixerGroup = audioMixerGroup;
    }

    public void Play()
    {
        source.Play();
    }
}

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    Sound[] sounds;

    [Header("Listening on channels")]
    [SerializeField] AudioSourceEventChannelSO _playAudioEvent;

    //public static AudioManager instance;

    private void Awake()
    {
        /*if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);*/

        foreach(Sound s in sounds)
        {
            s.SetSource(gameObject.AddComponent<AudioSource>());
        }
    }

    private void OnEnable()
    {
        if (_playAudioEvent != null)
            _playAudioEvent.OnAudioSourcePlayRequested += Play;
    }

    private void OnDisable()
    {
        if (_playAudioEvent != null)
            _playAudioEvent.OnAudioSourcePlayRequested -= Play;
    }

    private void Start()
    {
        //Play(AudioClipName.MainTheme);
    }


    public void Play(AudioClipName name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;

        }

        s.Play();
        
    }
}
