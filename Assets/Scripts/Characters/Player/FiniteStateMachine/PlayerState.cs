using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateNames { CrouchIdle, Crouch, Dash, Idle, InAir, Jump, Land, Move, PrimAtk, Ability, Grounded }
//Base class for all states, any state we create inherits from this class
public class PlayerState 
{
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    public StateNames stateName { get; protected set; }

    protected float startTime;

    private string _animName;

    protected bool isAnimationFinished;
    protected bool isExitingState; 

    public PlayerState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this._animName = animName;

    }

    public virtual void Enter()
    {
        DoChecks();
        player.anim.SetBool(_animName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        player.anim.SetBool(_animName, false);
        isExitingState = true;
    }

    public virtual void Execute()
    {

    }

    public virtual void ExecutePhysics()
    {
        DoChecks();
    }

    /*
     * Used for checks such as looking for ground or wall. 
     * Placed in Enter() and ExecutePhysics() once so we do 
     * not have to do so in every state.
     */
    public virtual void DoChecks() { }
    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;

}
