using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimAtkState : PlayerAbilityState
{
    public PlayerPrimAtkState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
        
    }

   /* public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }*/

    /*public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }*/

   /* public override void DoChecks()
    {
        base.DoChecks();
    }*/

    public override void Enter()
    {
        base.Enter();

        IsAbilityDone = true;
        //Debug.Log()
    }

   /* public override void Execute()
    {
        base.Execute();
    }*/

   /* public override void ExecutePhysics()
    {
        base.ExecutePhysics();
    }*/

  /*  public override void Exit()
    {
        base.Exit();
    }*/
}
