using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Charge state for basic enemy. 
/// Handles transitions to different basic enemy states 
/// from basic enemy idle state. 
/// </summary>
public class BasicEnemy_Idle : EntityIdleState
{
    private BasicEnemy enemy;
    public BasicEnemy_Idle(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityIdleStateSO stateData, BasicEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isPlayerInMinAgroRange)
            stateMachine.ChangeState(enemy.detectionState);
        else if (isIdleTimeOver)
            stateMachine.ChangeState(enemy.moveState);
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
