using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newEntityData", menuName = "Entity Data/Entity/Entity Data")]
public class EntitySO : ScriptableObject
{
    public int health = 30;
    public float damageHopSpeed = 3f;

    [Header("Check Variables")]
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public LayerMask whatIsGround; 

    [Header("Player Attack Variables")]
    public float minAgroDistance = 3.0f;
    public float maxAgroDistance = 4.0f;
    public LayerMask whatIsPlayer;
    public float closeRangeActionDistance = 1.0f; 

}
