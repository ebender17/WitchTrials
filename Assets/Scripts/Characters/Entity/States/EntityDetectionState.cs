using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for enemy specific player detection state. 
/// Player detected when within min agro range. 
/// Remains detected while inside max agro range. 
/// </summary>
public class EntityDetectionState : EntityState
{
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction; 

    protected EntityDetectionStateSO stateData;
    public EntityDetectionState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDetectionStateSO stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    //Called in EntityState parent class in enter and executephysics
    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocityX(0.0f);

        performLongRangeAction = false;

    }

    public override void Execute()
    {
        base.Execute();

        if (Time.time >= startTime + stateData.longRangeActionTime)
            performLongRangeAction = true; 
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
