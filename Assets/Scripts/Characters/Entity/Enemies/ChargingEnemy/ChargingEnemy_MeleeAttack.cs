using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemy_MeleeAttack : EntityMeleeAttackState
{
    private ChargingEnemy enemy; 
    public ChargingEnemy_MeleeAttack(Entity entity, EntityStateMachine stateMachine, string animBoolName, Transform attackPosition, EntityMeleeAttackStateSO stateData, ChargingEnemy enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

        if(isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
                stateMachine.ChangeState(enemy.playerDetectedState);
            else
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
