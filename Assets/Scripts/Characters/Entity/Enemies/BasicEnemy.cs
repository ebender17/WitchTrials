using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Entity
{
    public BasicEnemy_Idle idleState { get; private set; }
    public BasicEnemy_Move moveState { get; private set; }
    public BasicEnemy_Detection detectionState { get; private set; }
    public BasicEnemy_Charge chargeState { get; private set; }
    public BasicEnemy_LookForPlayer lookForPlayerState { get; private set; }
    public BasicEnemy_MeleeAttack meleeAttackState { get; private set;}

    [SerializeField] private EntityIdleStateSO _idleStateData; 
    [SerializeField] private EntityMoveStateSO _moveStateData;
    [SerializeField] private EntityDetectionStateSO _detectionStateData;
    [SerializeField] private EntityChargeStateSO _chargeStateData;
    [SerializeField] private EntityLookForPlayerStateSO _lookForPlayerStateData;
    [SerializeField] private EntityMeleeAttackStateSO _meleeAttackStateData;

    [SerializeField] private Transform _meleeAttackPosition = default;

    public override void Start()
    {
        base.Start();

        idleState = new BasicEnemy_Idle(this, stateMachine, "Idle", _idleStateData, this);
        moveState = new BasicEnemy_Move(this, stateMachine, "Move", _moveStateData, this);
        detectionState = new BasicEnemy_Detection(this, stateMachine, "PlayerDetected", _detectionStateData, this);
        chargeState = new BasicEnemy_Charge(this, stateMachine, "Charge", _chargeStateData, this);
        lookForPlayerState = new BasicEnemy_LookForPlayer(this, stateMachine, "LookForPlayer", _lookForPlayerStateData, this);
        meleeAttackState = new BasicEnemy_MeleeAttack(this, stateMachine, "MeleeAttack", _meleeAttackPosition, _meleeAttackStateData, this);

        stateMachine.Initialize(idleState);

    }

#if UNITY_EDITOR
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(_meleeAttackPosition.position, _meleeAttackStateData.attackRadius);
    }
#endif
}
