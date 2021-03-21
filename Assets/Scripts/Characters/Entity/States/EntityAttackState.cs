using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for specific attack states.
/// </summary>
public class EntityAttackState : EntityState
{
    protected Transform attackPosition;

    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;

    public EntityAttackState(Entity entity, EntityStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        //Anim trigger knows which attackState to call anim on
        entity.atsm.attackState = this;

        isAnimationFinished = false;
        entity.SetVelocityX(0.0f);
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

    //Called at beginning of attack animation
    public virtual void TriggerAttack()
    {

    }

    //Called after attack animation finishes
    //This is how we know to transition to another state
    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
}
