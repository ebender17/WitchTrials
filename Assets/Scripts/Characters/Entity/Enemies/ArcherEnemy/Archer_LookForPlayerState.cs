using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_LookForPlayerState : EntityLookForPlayer

{
    private ArcherEnemy _enemy;
    public Archer_LookForPlayerState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityLookForPlayerStateSO stateData, ArcherEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isPlayerInMinAgroRange)
            stateMachine.ChangeState(_enemy.playerDetectedState);
        else if (isAllTurnsDoneTime)
            stateMachine.ChangeState(_enemy.moveState);
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
