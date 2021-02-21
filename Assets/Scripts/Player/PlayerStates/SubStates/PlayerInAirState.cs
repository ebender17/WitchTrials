using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    //CheckS
    private bool isGrounded;
    private bool isJumping;

    //Input 
    private int xInput; 
    private bool jumpInputStop;
    private bool jumpInput;
    private bool dashInput; 

    private bool coyoteTime;

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

        CheckCoyoteTime();

        //Get input
        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        dashInput = player.InputHandler.DashInput; 

        CheckJumpMultiplier();

        /*
        * Because jump ability is instantly over player will still be on ground when first jumping
        * Therefore if grounded and velocity is greater than 0.01 (meaning we just jumped) we
        * move to in air state
        */
        if (isGrounded && player.CurrVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if(jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if(dashInput && player.DashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
        else
        {
            /* Subtract fallMultiplier by 1 because Unity's Physics Engine is already aplying one multiple 
            * of gravity (normal gravity)
            * player.SetVelocityY(playerData.jumpVelocity * Physics2D.gravity.y * (playerData.fallMultiplier - 1) * Time.deltaTime);
            */

            //Flips sprite in air 
            player.CheckIfShouldFlip(xInput);

            //Allows x movement in air 
            player.SetVelocityX(playerData.movementVelocity * xInput);

            // Changes sprite based on velocity values
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
    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            // If player let go of jump key early (short jump) apply jump multiplier
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrVelocity.y * playerData.jumpHeightMultiplier);
                // So we only go through this code block once
                isJumping = false;
            }
            // If we were jumping but now we are just falling. Do not decrease velocity.
            else if (player.CurrVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }

    }

    public void SetIsJumping() => isJumping = true; 

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseNumJumpsLeft();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;
}
