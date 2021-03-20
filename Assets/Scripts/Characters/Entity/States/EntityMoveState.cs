using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMoveState : EntityState
{
    protected EntityMoveStateSO stateData;

    protected bool isDetectingWall; 
    protected bool isDetectingLedge;
    protected bool isPlayerInMinAgroRange;

    public EntityMoveState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityMoveStateSO stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        //Remove if you do not want functionality in Enter() fun in base class
        base.Enter();

        entity.SetVelocityX(stateData.movementSpeed);

        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();

        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Exit()
    {
        base.Exit();
    }

}
