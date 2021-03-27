using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Charge state for basic enemy. 
/// Handles transitions to different basic enemy states 
/// from basic enemy move state. 
/// </summary>
public class ChargingEnemy_Move : EntityMoveState
{
    private ChargingEnemy enemy; 
    public ChargingEnemy_Move(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityMoveStateSO stateData, ChargingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();

        if(isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.detectionState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
