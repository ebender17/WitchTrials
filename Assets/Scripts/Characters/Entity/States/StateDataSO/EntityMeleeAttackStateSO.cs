using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Entity Data/State/Melee Attack State Data")]
public class EntityMeleeAttackStateSO : ScriptableObject
{
    public float attackRadius = 0.5f;
    public int attackDamage = 10;
}
