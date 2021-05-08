using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach to player and trigger methods using animation events.
/// Dash and takeDamage are played inside the player controller and dash state.
/// </summary>
public class PlayerAudioManager : MonoBehaviour
{
    [Header("SFX Channels")]
    public AudioSoundEventChannelSO SFXChannel;
    [Tooltip("Use to play random audio clips from list")]
    public AudioSoundsEventChannelSO SFXRandomChannel;
    public PlayerSounds playerSounds;

    public void PlayLand()
    {
        SFXChannel.RaiseEvent(playerSounds.land);
    }

    public void PlayFootsteps()
    {
        SFXRandomChannel.RaiseEvent(playerSounds.grassFootsteps);
    }
}


