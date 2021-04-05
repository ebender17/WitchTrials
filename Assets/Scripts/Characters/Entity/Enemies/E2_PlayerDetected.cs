using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_PlayerDetected : EntityPlayerDetectedState
{
    private Enemy2 enemy;
    public E2_PlayerDetected(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDetectionStateSO stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            //TODO: replace with variable dodgeTime from dodgeState
            if(Time.time >= enemy.dodgeState.startTime + enemy._dodgeStateData.dodgeCooldown)
            {
                stateMachine.ChangeState(enemy.dodgeState);
            }
            else
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
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
