using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_Move moveState { get; private set; }
    public E2_Idle idleState { get; private set; }

    [SerializeField]
    private EntityMoveStateSO _moveStateData;
    [SerializeField]
    private EntityIdleStateSO _idleStateData;

    public override void Start()
    {
        base.Start();

        moveState = new E2_Move(this, stateMachine, "Move", _moveStateData, this);
        idleState = new E2_Idle(this, stateMachine, "Idle", _idleStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void TakeDamage(float playerXPox, int damage)
    {
        base.TakeDamage(playerXPox, damage);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
