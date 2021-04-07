using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemy_LookForPlayer : EntityLookForPlayer
{
    private ChargingEnemy enemy; 
    public ChargingEnemy_LookForPlayer(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityLookForPlayerStateSO stateData, ChargingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isAllTurnsDoneTime)
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
