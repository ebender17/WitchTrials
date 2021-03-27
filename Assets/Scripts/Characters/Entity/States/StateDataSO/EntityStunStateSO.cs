 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStunStateData", menuName = "Entity Data/State/Stun State Data")]
public class EntityStunStateSO : ScriptableObject
{
    public float stunTime = 3f;

    public float stunKnockbackTime = 0.2f;
    public float stunKnockbackSpeed = 20f;
    public Vector2 stunKnockbackAngle; 
    
}
