using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for all states, any state we create inherits from this class
public class PlayerState 
{
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;

    private string animName;

    protected bool isAnimationFinished;
    protected bool isExitingState; 

    public PlayerState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animName = animName; 

    }

    public virtual void Enter()
    {
        DoChecks();
        player.Anim.SetBool(animName, true);
        startTime = Time.time;
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {
        player.Anim.SetBool(animName, false);
        //player.Anim.StopPlayback();
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
