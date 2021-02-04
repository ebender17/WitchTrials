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
        player.Anim.Play(animName);
        startTime = Time.time;
        Debug.Log(animName);
    }

    public virtual void Exit()
    {
        //player.Anim.SetBool(animBoolName, false);
        //player.Anim.StopPlayback();
    }

    public virtual void Execute()
    {

    }

    public virtual void ExecutePhysics()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }


}
