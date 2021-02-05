using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    private bool JumpInput;
    private bool isGrounded;

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

    }

    public override void Execute()
    {
        base.Execute();

        //Move
        xInput = player.NormalizeInputX();

        //Jump 
        JumpInput = player.JumpInput;
        if (JumpInput && player.JumpState.CanJump())
        {
            player.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        } 
        // Player falls off ground
        else if(!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
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
