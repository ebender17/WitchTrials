using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "newSound", menuName = "Audio/Sound")]
public class Sound : ScriptableObject
{
    public string name;

    public AudioClip clip;

    public AudioMixerGroup audioMixerGroup;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop = false;

}
