using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    private bool jumpInput;
    private bool dashInput; 
    private bool isGrounded;
    private bool primAtkInput; 

    public PlayerGroundedState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
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

        player.JumpState.ResetNumJumpsLeft();
        player.DashState.ResetCanDash(); 

    }

    public override void Execute()
    {
        base.Execute();

       
        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput; 


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
        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }

        //Primary Attack
        /*if (primAtkInput)
        {
            stateMachine.ChangeState(player.PrimAtkState); 
        }*/
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
