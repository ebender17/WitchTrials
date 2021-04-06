using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityState 
{
    protected EntityStateMachine stateMachine;
    protected Entity entity;

    protected float startTime;

    protected string animBoolName; 
    public EntityState(Entity entity, EntityStateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.entity = entity;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);

        DoChecks();
    }

    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
    }

    
    public virtual void Execute()
    {

    }

    public virtual void ExecutePhysics()
    {
        DoChecks();
    }

    /// <summary>
    /// Place checks to be performed in Enter and ExecutePhysics here. 
    /// </summary>
    public virtual void DoChecks()
    {

    }
}
