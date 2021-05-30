using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerSounds", menuName = "Audio/Player Sounds")]
public class PlayerSounds : ScriptableObject
{
    public Sound takeDamage;
    public Sound land;
    public Sound dash;
    public Sound[] grassFootsteps;

}
