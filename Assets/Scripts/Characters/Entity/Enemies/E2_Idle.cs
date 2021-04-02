using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_Idle : EntityIdleState
{
    private Enemy2 enemy; 

    public E2_Idle(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityIdleStateSO stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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

        //TODO: player detected state transition

        if(isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
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
