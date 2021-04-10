using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy_Move : EntityMoveState
{
    private PatrolEnemy _enemy;
    public PatrolEnemy_Move(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityMoveStateSO stateData, PatrolEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this._enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();

       // _enemy.CheckTouchDamage();

        if (isDetectingWall || !isDetectingLedge)
        {
            _enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(_enemy.idleState);
        }
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
