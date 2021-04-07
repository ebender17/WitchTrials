using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStunState : EntityState
{
    protected EntityStunStateSO stateData;

    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovememtStopped;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAgroRange;
    public EntityStunState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityStunStateSO stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = entity.CheckGround();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;
        entity.SetVelocityAndAngle(stateData.stunKnockbackSpeed, stateData.stunKnockbackAngle, entity.lastDamageDirection);
    }

    public override void Execute()
    {
        base.Execute();

        if (Time.time >= startTime + stateData.stunTime)
            isStunTimeOver = true;

        //second check is necessary b/c as soon as we knockback enemy, isGrounded will be true 
        //Give buffer time to allow enemy to get into air 
        if(isGrounded && Time.time >= startTime + stateData.stunKnockbackTime && !isMovememtStopped)
        {
            isMovememtStopped = true;
            entity.SetVelocityX(0f);
        }

        //While enemy is stunned we still want to be able to hit enemy and allow it to bounce
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
    }

    public override void Exit()
    {
        base.Exit();
        entity.ResetStunResistance();
    }
}
