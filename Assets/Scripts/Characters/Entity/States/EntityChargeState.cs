using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityChargeState : EntityState
{
    EntityChargeStateSO stateData;

    protected bool isPlayerInMinAgroRange;

    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver; 

    public EntityChargeState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityChargeStateSO stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData; 
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;

        entity.SetVelocityX(stateData.chargeSpeed);

    }

    public override void Execute()
    {
        base.Execute();

        if (Time.time >= startTime + stateData.chargeTime)
            isChargeTimeOver = true; 
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
    }

    public override void Exit()
    {
        base.Exit();
    }

    //Called in EntityState parent class in enter and executephysics
    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
    }
}
