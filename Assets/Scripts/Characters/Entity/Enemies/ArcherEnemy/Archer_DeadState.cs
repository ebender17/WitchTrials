using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_DeadState : EntityDeadState
{
    private ArcherEnemy _enemy;
    public Archer_DeadState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDeadStateSO stateData, ArcherEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
