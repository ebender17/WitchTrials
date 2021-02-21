using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;
    private bool jumpInput;
    private bool dashInput; 
    private bool isGrounded;
    private bool primAtkInput;

    protected bool isTouchingCeiling;

    public PlayerGroundedState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingCeiling = player.CheckForCeiling();

    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetNumJumpsLeft();
        player.DashState.ResetCanDash(); 

    }

    public override void Execute()
    {
        base.Execute();

        //Get inputs
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        jumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput;
        primAtkInput = player.InputHandler.PrimAtkInput;


        //Jump 
        if (jumpInput && player.JumpState.CanJump())
        {
            //player.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        } 
        // Player falls off ground
        else if(!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        //Dash
        else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.DashState);
        }
        //Primary Attack
        else if(primAtkInput && player.PrimAtkState.CheckIfCanPrimAtk() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.PrimAtkState);
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
