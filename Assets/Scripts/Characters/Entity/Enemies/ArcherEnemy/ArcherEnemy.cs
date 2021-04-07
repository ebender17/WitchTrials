using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : Entity
{
    public Archer_MoveState moveState { get; private set; }
    public Archer_IdleState idleState { get; private set; }
    public Archer_PlayerDetected playerDetectedState { get; private set; }
    public Archer_MeleeAttack meleeAttackState { get; private set; }
    public Archer_LookForPlayerState lookForPlayerState { get; private set; }
    public Archer_StunState stunState { get; private set; }
    public Archer_DeadState deadState { get; private set; }
    public Archer_DodgeState dodgeState { get; private set; }
    public Archer_RangedAttack rangeAttackState { get; private set; }

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
    [SerializeField]
    private EntityDodgeStateSO _dodgeStateData;
    [SerializeField] 
    private EntityRangedAttackStateSO _rangeAttackStateData;

    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private Transform rangeAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new Archer_MoveState(this, stateMachine, "Move", _moveStateData, this);
        idleState = new Archer_IdleState(this, stateMachine, "Idle", _idleStateData, this);
        playerDetectedState = new Archer_PlayerDetected(this, stateMachine, "PlayerDetected", _playerDetectedData, this);
        meleeAttackState = new Archer_MeleeAttack(this, stateMachine, "MeleeAttack", meleeAttackPosition, _meleeAttackData, this);
        lookForPlayerState = new Archer_LookForPlayerState(this, stateMachine, "LookForPlayer", _lookForPlayerData, this);
        stunState = new Archer_StunState(this, stateMachine, "Stun", _stunStateData, this);
        deadState = new Archer_DeadState(this, stateMachine, "Dead", _deadStateData, this);
        dodgeState = new Archer_DodgeState(this, stateMachine, "Dodge", _dodgeStateData, this);
        rangeAttackState = new Archer_RangedAttack(this, stateMachine, "RangeAttack", rangeAttackPosition, _rangeAttackStateData, this);

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
