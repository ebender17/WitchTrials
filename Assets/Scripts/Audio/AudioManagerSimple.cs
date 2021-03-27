using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    private AudioMixerGroup audioMixerGroup;

    private AudioSource source;

    public string clipName;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 3f)]
    public float pitch;

    public bool loop = false;
    public bool playOnAwake = false;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.pitch = pitch;
        source.volume = volume;
        source.playOnAwake = playOnAwake;
        source.loop = loop;
        source.outputAudioMixerGroup = audioMixerGroup;
    }

    public void Play()
    {
        source.Play();
    }
}
public class AudioManagerSimple : MonoBehaviour
{
    [SerializeField]
    Sound[] sounds;

    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].clipName);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>()); 
        }
        PlaySound("MainTheme");
    }

    public void PlaySound(string _name)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].clipName == _name)
            {
                sounds[i].Play();
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
