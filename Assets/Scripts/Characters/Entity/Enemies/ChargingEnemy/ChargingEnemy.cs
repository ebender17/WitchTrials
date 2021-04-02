using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemy : Entity
{
    public ChargingEnemy_Idle idleState { get; private set; }
    public ChargingEnemy_Move moveState { get; private set; }
    public ChargingEnemy_Detection detectionState { get; private set; }
    public ChargingEnemy_Charge chargeState { get; private set; }
    public ChargingEnemy_LookForPlayer lookForPlayerState { get; private set; }
    public ChargingEnemy_MeleeAttack meleeAttackState { get; private set;}
    public ChargingEnemy_Stun stunState { get; private set; }
    public ChargingEnemy_Dead deadState { get; private set; }

    [SerializeField] private EntityIdleStateSO _idleStateData; 
    [SerializeField] private EntityMoveStateSO _moveStateData;
    [SerializeField] private EntityDetectionStateSO _detectionStateData;
    [SerializeField] private EntityChargeStateSO _chargeStateData;
    [SerializeField] private EntityLookForPlayerStateSO _lookForPlayerStateData;
    [SerializeField] private EntityMeleeAttackStateSO _meleeAttackStateData;
    [SerializeField] private EntityStunStateSO _stunStateData;
    [SerializeField] private EntityDeadStateSO _deadStateData;

    [SerializeField] private Transform _meleeAttackPosition = default;

    public override void Start()
    {
        base.Start();

        idleState = new ChargingEnemy_Idle(this, stateMachine, "Idle", _idleStateData, this);
        moveState = new ChargingEnemy_Move(this, stateMachine, "Move", _moveStateData, this);
        detectionState = new ChargingEnemy_Detection(this, stateMachine, "PlayerDetected", _detectionStateData, this);
        chargeState = new ChargingEnemy_Charge(this, stateMachine, "Charge", _chargeStateData, this);
        lookForPlayerState = new ChargingEnemy_LookForPlayer(this, stateMachine, "LookForPlayer", _lookForPlayerStateData, this);
        meleeAttackState = new ChargingEnemy_MeleeAttack(this, stateMachine, "MeleeAttack", _meleeAttackPosition, _meleeAttackStateData, this);
        stunState = new ChargingEnemy_Stun(this, stateMachine, "Stun", _stunStateData, this);
        deadState = new ChargingEnemy_Dead(this, stateMachine, "Dead", _deadStateData, this);

        stateMachine.Initialize(idleState);

    }

#if UNITY_EDITOR
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(_meleeAttackPosition.position, _meleeAttackStateData.attackRadius);
    }
#endif

    public override void TakeDamage(float playerXPox, int damage)
    {
        base.TakeDamage(playerXPox, damage);

        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else if(isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }

        
    }

}
