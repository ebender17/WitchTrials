using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_Move moveState { get; private set; }
    public E2_Idle idleState { get; private set; }
    public E2_PlayerDetected playerDetectedState { get; private set; }
    public E2_MeleeAttack meleeAttackState { get; private set; }
    public E2_LookForPlayerState lookForPlayerState { get; private set; }
    public E2_StunState stunState { get; private set; }
    public E2_DeadState deadState { get; private set; }
    public E2_DodgeState dodgeState { get; private set; }
    public E2_RangedAttack rangeAttackState { get; private set; }

    [SerializeField]
    private EntityMoveStateSO _moveStateData;
    [SerializeField]
    private EntityIdleStateSO _idleStateData;
    [SerializeField]
    private EntityDetectionStateSO _playerDetectedData;
    [SerializeField]
    private EntityMeleeAttackStateSO _meleeAttackData;
    [SerializeField]
    private EntityLookForPlayerStateSO _lookForPlayerData;
    [SerializeField]
    private EntityStunStateSO _stunStateData;
    [SerializeField]
    private EntityDeadStateSO _deadStateData;
    //TODO: make private again after placing variable in dodge state for player detected state
    public EntityDodgeStateSO _dodgeStateData;
    [SerializeField] private EntityRangedAttackStateSO _rangeAttackStateData;

    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private Transform rangeAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new E2_Move(this, stateMachine, "Move", _moveStateData, this);
        idleState = new E2_Idle(this, stateMachine, "Idle", _idleStateData, this);
        playerDetectedState = new E2_PlayerDetected(this, stateMachine, "PlayerDetected", _playerDetectedData, this);
        meleeAttackState = new E2_MeleeAttack(this, stateMachine, "MeleeAttack", meleeAttackPosition, _meleeAttackData, this);
        lookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "LookForPlayer", _lookForPlayerData, this);
        stunState = new E2_StunState(this, stateMachine, "Stun", _stunStateData, this);
        deadState = new E2_DeadState(this, stateMachine, "Dead", _deadStateData, this);
        dodgeState = new E2_DodgeState(this, stateMachine, "Dodge", _dodgeStateData, this);
        rangeAttackState = new E2_RangedAttack(this, stateMachine, "RangeAttack", rangeAttackPosition, _rangeAttackStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void TakeDamage(float playerXPox, int damage)
    {
        base.TakeDamage(playerXPox, damage);

        if(isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else if(isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
        else if(CheckPlayerInMinAgroRange())
        {
            stateMachine.ChangeState(rangeAttackState);
        }
        //Enemy turns immediately if hit from behind
        else if(!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
            
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, _meleeAttackData.attackRadius);
    }
}
