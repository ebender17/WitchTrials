using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (isIdleTimeOver)
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
