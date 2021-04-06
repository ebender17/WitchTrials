using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_IdleState : EntityIdleState
{
    private ArcherEnemy enemy; 

    public Archer_IdleState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityIdleStateSO stateData, ArcherEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            stateMachine.ChangeState(enemy.playerDetectedState);
        else if(isIdleTimeOver)
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
