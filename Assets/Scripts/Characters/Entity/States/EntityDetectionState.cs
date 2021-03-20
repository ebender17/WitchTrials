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

    protected EntityDetectionStateSO stateData;
    public EntityDetectionState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDetectionStateSO stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocityX(0.0f);

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
