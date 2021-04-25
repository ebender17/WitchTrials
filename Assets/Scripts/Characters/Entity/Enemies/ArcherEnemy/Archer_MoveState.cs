using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_MoveState : EntityMoveState
{
    private ArcherEnemy enemy;

    public Archer_MoveState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityMoveStateSO stateData, ArcherEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
         else if(isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
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
