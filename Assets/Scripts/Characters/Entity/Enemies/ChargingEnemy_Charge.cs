using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Charge state for basic enemy. 
/// Handles transitions to different basic enemy states 
/// from basic enemy charge state. 
/// </summary>
public class ChargingEnemy_Charge : EntityChargeState
{
    private ChargingEnemy enemy; 

    public ChargingEnemy_Charge(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityChargeStateSO stateData, ChargingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if(isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
                stateMachine.ChangeState(enemy.detectionState);
            else
                stateMachine.ChangeState(enemy.lookForPlayerState);
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
