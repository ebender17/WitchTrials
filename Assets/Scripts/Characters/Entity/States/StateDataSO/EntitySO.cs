using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newEntityData", menuName = "Entity Data/Entity/Entity Data")]
public class EntitySO : ScriptableObject
{
    public float walkCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public LayerMask whatIsGround; 

    public float minAgroDistance = 3.0f;
    public float maxAgroDistance = 4.0f;
    public LayerMask whatIsPlayer;

}
