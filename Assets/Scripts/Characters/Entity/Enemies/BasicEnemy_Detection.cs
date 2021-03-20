using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy_Detection : EntityDetectionState
{
    private BasicEnemy enemy; 
    public BasicEnemy_Detection(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDetectionStateSO stateData, BasicEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (!isPlayerInMaxAgroRange)
        {
            enemy.idleState.SetFlipAfterIdle(false);
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
