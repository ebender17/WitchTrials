using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Listening on channels")]
    [SerializeField] private AudioSoundEventChannelSO _playMusicEvent = default;
    [SerializeField] private AudioSoundEventChannelSO _playSFXEvent = default;
    [SerializeField] private AudioSoundsEventChannelSO _playSFXRandomEvent = default;
    [SerializeField] private AudioSoundEventChannelSO _endMusicEvent = default;

    [SerializeField]
    private int objectPoolLength = 20;

    [SerializeField]
    private bool logSounds = false;

    private List<AudioSource> pool = new List<AudioSource>();

    private void OnEnable()
    {
        _playMusicEvent.OnEventRaised += PlaySound;
        _playSFXEvent.OnEventRaised += PlaySound;
        _playSFXRandomEvent.OnEventRaised += SelectSound;
        _endMusicEvent.OnEventRaised += ReturnToPool;
    }

    private void OnDisable()
    {
        _playMusicEvent.OnEventRaised -= PlaySound;
        _playSFXEvent.OnEventRaised -= PlaySound;
        _playSFXRandomEvent.OnEventRaised -= SelectSound;
        _endMusicEvent.OnEventRaised -= ReturnToPool;
    }

    private void Awake()
    {

        for (int i = 0; i < objectPoolLength; i++)
        {
            GameObject soundObject = new GameObject();
            soundObject.transform.SetParent(transform);
            soundObject.name = "Sound Effect";
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.gameObject.SetActive(false);
            pool.Add(audioSource);
        }
    }

    public void PlaySound(Sound audio)
    {
        if (!audio.clip)
        {
            Debug.Log("Clip is null!");
            return;
        }

        if (logSounds)
        {
            Debug.Log("Playing Audio: " + audio.name);
        }

        for (int i = 0; i < pool.Count; i++)
        {
            //Picks first audio source not active in the scene
            if (!pool[i].gameObject.activeInHierarchy)
            {
                SetSource(pool[i], audio);

                pool[i].name = audio.name;
                pool[i].transform.position = transform.position;
                pool[i].gameObject.SetActive(true);
                pool[i].Play();

                if (!audio.loop)
                    StartCoroutine(ReturnToPool(pool[i].gameObject, audio.clip.length));

                return;
            }
        }

        //If we run out of objects in the pool, create another audio source
        GameObject soundObject = new GameObject();
        soundObject.transform.SetParent(transform);
        soundObject.name = audio.name;
        //soundObject.name = "Sound Effect";

        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        pool.Add(audioSource);

        SetSource(audioSource, audio);

        soundObject.transform.position = transform.position;
        audioSource.Play();

        if (!audio.loop)
            StartCoroutine(ReturnToPool(soundObject, audio.clip.length));

    }

    private void SetSource(AudioSource source, Sound audio)
    {
        source.clip = audio.clip;
        source.pitch = audio.pitch;
        source.volume = audio.volume;
        source.loop = audio.loop;
        source.outputAudioMixerGroup = audio.audioMixerGroup;

    }

    public void SelectSound(Sound[] clips)
    {
        Sound audio = GetRandomClip(clips);
        PlaySound(audio);

    }

    private Sound GetRandomClip(Sound[] clips)
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }

    private IEnumerator ReturnToPool(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
    
    public void ReturnToPool(Sound sound)
    {

        AudioSource audioSource = pool.Find(x => x.gameObject.name == sound.name);

        if(audioSource)
        {
            audioSource.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("No audio source with game object name " + sound.name);
        }
    }
}