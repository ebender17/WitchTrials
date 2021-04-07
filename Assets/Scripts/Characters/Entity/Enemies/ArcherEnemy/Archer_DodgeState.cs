using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_DodgeState : EntityDodgeState
{
    private ArcherEnemy _enemy;

    public float dodgeTime { get; private set; }
    public Archer_DodgeState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDodgeStateSO stateData, ArcherEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        dodgeTime = startTime + stateData.dodgeCooldown;
    }

    public override void Execute()
    {
        base.Execute();

        if(isDodgeOver)
        {
            if(isPlayerInMaxAgroRange && performCloseRangeAction)
            {
                stateMachine.ChangeState(_enemy.meleeAttackState);
            }
            else if(isPlayerInMaxAgroRange && !performCloseRangeAction)
            {
                stateMachine.ChangeState(_enemy.rangeAttackState);
            }
            else if(!isPlayerInMaxAgroRange)
            {
                //TODO: can add turn if wanted
                stateMachine.ChangeState(_enemy.lookForPlayerState);
            }

        }

        //TODO: melle attack if there is a ledge behind the enemy
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
