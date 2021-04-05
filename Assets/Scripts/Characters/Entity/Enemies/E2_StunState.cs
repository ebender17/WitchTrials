using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_StunState : EntityStunState
{
    private Enemy2 _enemy;
    public E2_StunState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityStunStateSO stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if(isStunTimeOver)
        {
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(_enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(_enemy.lookForPlayerState);
            }
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
