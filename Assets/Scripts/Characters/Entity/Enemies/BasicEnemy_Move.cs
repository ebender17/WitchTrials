using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy_Move : EntityMoveState
{
    private BasicEnemy enemy; 
    public BasicEnemy_Move(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityMoveStateSO stateData, BasicEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if(isDetectingWall || !isDetectingLedge)
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
