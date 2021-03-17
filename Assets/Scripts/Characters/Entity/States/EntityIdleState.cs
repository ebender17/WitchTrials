using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for Entity Idle State. 
/// Transitions are done in specific entity states so every entity 
/// does not have to implement every state. 
/// </summary>
public class EntityIdleState : EntityState
{
    protected EntityIdleStateSO stateData;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;

    protected float idleTime;
    public EntityIdleState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityIdleStateSO stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocityX(0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Execute()
    {
        base.Execute();

        if (Time.time >= startTime + idleTime)
            isIdleTimeOver = true; 
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle)
            entity.Flip();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
   
