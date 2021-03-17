using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Entity
{
    public BasicEnemy_Idle idleState { get; private set; }
    public BasicEnemy_Move moveState { get; private set; }

    [SerializeField] private EntityIdleStateSO _idleStateData; 
    [SerializeField] private EntityMoveStateSO _moveStateData;

    public override void Start()
    {
        base.Start();

        idleState = new BasicEnemy_Idle(this, stateMachine, "idle", _idleStateData, this);
        moveState = new BasicEnemy_Move(this, stateMachine, "move", _moveStateData, this);

        stateMachine.Initialize(idleState);

    }
}
