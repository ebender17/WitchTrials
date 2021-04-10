using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newEntityData", menuName = "Entity Data/Entity/Entity Data")]
public class EntitySO : ScriptableObject
{
    public int health = 30;
    public float damageHopSpeed = 10f;
    public GameObject hitParticle;

    [Header("Broadcasting on")]
    public AudioSourceEventChannelSO SFXEventChannel;

    [Header("Check Variables")]
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround; 

    [Header("Player Attack Variables")]
    public float minAgroDistance = 3.0f;
    public float maxAgroDistance = 4.0f;
    public LayerMask whatIsPlayer;
    public float closeRangeActionDistance = 1.0f;
    public uint touchDamage = 5;

    [Header("Stun variables")]
    public int stunResistance = 3; //indicates number of hits before stun
    public float stunRecoveryTime = 2f;

}
