using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stun state for charging enemy. Transition handled in <see cref="ChargingEnemy"/> so enemy can be pronounced dead at anytime.
/// </summary>
public class ChargingEnemy_Dead : EntityDeadState
{
    private ChargingEnemy enemy;
    public ChargingEnemy_Dead(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDeadStateSO stateData, ChargingEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
