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

    [SerializeField] private EntityIdleStateSO _idleStateData; 
    [SerializeField] private EntityMoveStateSO _moveStateData;
    [SerializeField] private EntityDetectionStateSO _detectionStateData;
    [SerializeField] private EntityChargeStateSO _chargeStateData;
    [SerializeField] private EntityLookForPlayerStateSO _lookForPlayerStateData; 

    public override void Start()
    {
        base.Start();

        idleState = new BasicEnemy_Idle(this, stateMachine, "Idle", _idleStateData, this);
        moveState = new BasicEnemy_Move(this, stateMachine, "Move", _moveStateData, this);
        detectionState = new BasicEnemy_Detection(this, stateMachine, "PlayerDetected", _detectionStateData, this);
        chargeState = new BasicEnemy_Charge(this, stateMachine, "Charge", _chargeStateData, this);
        lookForPlayerState = new BasicEnemy_LookForPlayer(this, stateMachine, "LookForPlayer", _lookForPlayerStateData, this);

        stateMachine.Initialize(idleState);

    }
}
