using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy_Idle : EntityIdleState
{
    private PatrolEnemy _enemy;
    public PatrolEnemy_Idle(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityIdleStateSO stateData, PatrolEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        _enemy.CheckTouchDamage();

        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(_enemy.moveState);
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
