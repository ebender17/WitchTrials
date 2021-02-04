using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int xInput; 

    private bool isGrounded; 
    public PlayerInAirState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();

        xInput = player.NormalizeInputX();

        /*
        * Because jump ability is instantly over player will still be on ground when first jumping
        * Therefore if grounded and velocity is greater than 0.01 (meaning we just jumped) we
        * move to in air state
        */
        if (isGrounded && player.CurrVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
            Debug.Log("Landed!");
        }
        else
        {
            //Flips sprite in air 
            player.CheckIfShouldFlip(xInput);

            //Allows x movement in air 
            player.SetVelocityX(playerData.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", player.CurrVelocity.y);
            //Pass in Abs value of x Velocity as Blend Tree does not account for negative direction
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrVelocity.x));
        }
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
