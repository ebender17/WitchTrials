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

    private string animBoolName; 

    public PlayerState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName; 

    }

    public virtual void Enter()
    {
        DoChecks();
        //player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        //Debug.Log(animBoolName);
    }

    public virtual void Exit()
    {
        //player.Anim.SetBool(animBoolName, false);
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
