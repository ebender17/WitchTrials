using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("SoundEmitters pool")]
    [SerializeField] private SoundEmitterFactorySO _factory = default;
    [SerializeField] private SoundEmitterPoolSO _pool = default;
    [SerializeField] private int _initialSize = 10;

    //TODO: AudioCueEventChannel 

    [Header("Audio control")]
    [SerializeField] private AudioMixer audioMixer = default;
    [Range(0f, 1f)]
    [SerializeField] private float _masterVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _musicVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _sfxVolume = 1f;
}
