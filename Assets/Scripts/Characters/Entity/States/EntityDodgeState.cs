using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDodgeState : EntityState
{
    protected EntityDodgeStateSO stateData;

    protected bool performCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;

    protected bool isGrounded;
    protected bool isDodgeOver;

    public EntityDodgeState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDodgeStateSO stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();

        isGrounded = entity.CheckGround();
    }

    public override void Enter()
    {
        base.Enter();

        isDodgeOver = false;

        entity.SetVelocityAndAngle(stateData.dodgeSpeed, stateData.dodgeAngle, -entity.facingDirection);
    }

    public override void Execute()
    {
        base.Execute();

        if(Time.time >= startTime + stateData.dodgeTime && isGrounded)
        {
            isDodgeOver = true;
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
