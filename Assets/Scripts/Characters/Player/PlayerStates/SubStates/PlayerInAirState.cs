using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    //CheckS
    private bool _isGrounded;
    private bool _isJumping;

    //Input 
    private int _xInput; 
    private bool _jumpInputStop;
    private bool _jumpInput;
    private bool _dashInput; 

    private bool _coyoteTime;

    public PlayerInAirState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
        this.stateName = StateNames.InAir;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = player.CheckIfGrounded();
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
        _xInput = player.normInputX;
        _jumpInput = player.jumpInput;
        _jumpInputStop = player.jumpInputStop;
        _dashInput = player.dashInput; 

        CheckJumpMultiplier();

        /*
        * Because jump ability is instantly over player will still be on ground when first jumping
        * Therefore if grounded and velocity is greater than 0.01 (meaning we just jumped) we
        * move to in air state
        */
        if (_isGrounded && player.currVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.landState);
        }
        else if(_jumpInput && player.jumpState.CanJump())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if(_dashInput && player.dashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
        else
        {
            /* Subtract fallMultiplier by 1 because Unity's Physics Engine is already aplying one multiple 
            * of gravity (normal gravity)
            * player.SetVelocityY(playerData.jumpVelocity * Physics2D.gravity.y * (playerData.fallMultiplier - 1) * Time.deltaTime);
            */

            //Flips sprite in air 
            player.CheckIfShouldFlip(_xInput);

            //Allows x movement in air 
            player.SetVelocityX(playerData.movementVelocity * _xInput);

            // Changes sprite based on velocity values
            player.anim.SetFloat("yVelocity", player.currVelocity.y);
            //Pass in Abs value of x Velocity as Blend Tree does not account for negative direction
            player.anim.SetFloat("xVelocity", Mathf.Abs(player.currVelocity.x));
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
        if (_isJumping)
        {
            // If player let go of jump key early (short jump) apply jump multiplier
            if (_jumpInputStop)
            {
                player.SetVelocityY(player.currVelocity.y * playerData.jumpHeightMultiplier);
                // So we only go through this code block once
                _isJumping = false;
            }
            // If we were jumping but now we are just falling. Do not decrease velocity.
            else if (player.currVelocity.y <= 0f)
            {
                _isJumping = false;
            }
        }

    }

    public void SetIsJumping() => _isJumping = true; 

    private void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            _coyoteTime = false;
            player.jumpState.DecreaseNumJumpsLeft();
        }
    }

    public void StartCoyoteTime() => _coyoteTime = true;
}
