using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stun state for charging enemy. Transition handled in <see cref="ChargingEnemy"/> so enemy can be stunned at anytime.
/// </summary>
public class ChargingEnemy_Stun : EntityStunState
{
    private ChargingEnemy enemy;
    public ChargingEnemy_Stun(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityStunStateSO stateData, ChargingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if(isStunTimeOver)
        {
            if (performCloseRangeAction)
                stateMachine.ChangeState(enemy.meleeAttackState);
            else if (isPlayerInMinAgroRange)
                stateMachine.ChangeState(enemy.chargeState);
            else
            {
                enemy.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }

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
