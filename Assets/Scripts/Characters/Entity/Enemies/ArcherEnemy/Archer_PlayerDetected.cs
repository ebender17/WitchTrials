using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_PlayerDetected : EntityPlayerDetectedState
{
    private ArcherEnemy enemy;
    public Archer_PlayerDetected(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDetectionStateSO stateData, ArcherEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
       
            if(Time.time >= enemy.dodgeState.dodgeTime)
            {
                stateMachine.ChangeState(enemy.dodgeState);
            }
            else
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
        }
        else if(performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.rangeAttackState);
        }
        else if(!isPlayerInMaxAgroRange)
        {
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
