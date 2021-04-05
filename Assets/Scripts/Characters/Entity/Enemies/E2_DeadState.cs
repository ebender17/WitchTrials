using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DeadState : EntityDeadState
{
    private Enemy2 _enemy;
    public E2_DeadState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDeadStateSO stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
