using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLookForPlayer : EntityState
{
    EntityLookForPlayerStateSO stateData;

    protected bool turnImmediately; 

    protected bool isPlayerInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsDoneTime;

    protected float lastTurnTime;

    protected int amountOfTurnsDone; 

    public EntityLookForPlayer(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityLookForPlayerStateSO stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        isAllTurnsDone = false;
        isAllTurnsDoneTime = false;

        lastTurnTime = startTime;
        amountOfTurnsDone = 0;

        entity.SetVelocityX(0.0f);
    }

    public override void Execute()
    {
        base.Execute();

        if(turnImmediately)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            turnImmediately = false;
        }
        else if(Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++; 
        }

        //Check if all turns are done 
        if (amountOfTurnsDone >= stateData.amountOfTurns)
            isAllTurnsDone = true;

        //Final turn is done and has looked in last dir for alloted time
        if(Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone)
            isAllTurnsDoneTime = true; 
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void SetTurnImmediately(bool flip) => turnImmediately = flip; 
}
