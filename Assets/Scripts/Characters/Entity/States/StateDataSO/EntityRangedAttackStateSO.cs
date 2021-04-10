using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Entity Data/State/Ranged Attack State Data")]
public class EntityRangedAttackStateSO : ScriptableObject
{
    public GameObject projectile;
    public uint projectileDamage = 10;
    public float projectileSpeed = 12f;
    public float projectileTravelDistance;
    
}
