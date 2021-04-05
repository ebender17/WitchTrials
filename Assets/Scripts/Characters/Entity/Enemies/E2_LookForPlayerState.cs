using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_LookForPlayerState : EntityLookForPlayer

{
    private Enemy2 _enemy;
    public E2_LookForPlayerState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityLookForPlayerStateSO stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
