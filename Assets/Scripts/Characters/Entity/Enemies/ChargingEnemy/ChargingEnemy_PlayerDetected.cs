using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Charge state for basic enemy. 
/// Handles transitions to different basic enemy states 
/// from basic enemy detection state. 
/// </summary>
public class ChargingEnemy_PlayerDetected : EntityPlayerDetectedState
{
    private ChargingEnemy enemy; 
    public ChargingEnemy_PlayerDetected(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDetectionStateSO stateData, ChargingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();

        //TODO: Check
        if (performCloseRangeAction)
            stateMachine.ChangeState(enemy.meleeAttackState);
        else if (performLongRangeAction)
            stateMachine.ChangeState(enemy.chargeState);
        else if (!isPlayerInMaxAgroRange)
            stateMachine.ChangeState(enemy.lookForPlayerState);
        else if (!isDetectingLedge)
        {
            entity.Flip();
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
