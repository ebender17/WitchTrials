using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_RangedAttack : EntityRangedAttackState
{
    private ArcherEnemy _enemy;
    public Archer_RangedAttack(Entity entity, EntityStateMachine stateMachine, string animBoolName, Transform attackPosition, EntityRangedAttackStateSO stateData, ArcherEnemy enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this._enemy = enemy;
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
                stateMachine.ChangeState(_enemy.playerDetectedState);
            else
                stateMachine.ChangeState(_enemy.lookForPlayerState);
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
