using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDodgeStateData", menuName = "Entity Data/State/Dodge State Data")]
public class EntityDodgeStateSO : ScriptableObject
{
    public float dodgeSpeed = 5.0f;
    public float dodgeTime = 0.2f;
    public float dodgeCooldown = 2f;
    public Vector2 dodgeAngle;

}
