using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy_LookForPlayer : EntityLookForPlayer
{
    private BasicEnemy enemy; 
    public BasicEnemy_LookForPlayer(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityLookForPlayerStateSO stateData, BasicEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();

        if(isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.detectionState);
        }
        else if(isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
