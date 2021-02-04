using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    private bool JumpInput; 

    public PlayerGroundedState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        //Jump Callback
        player.PlayerInputHandler.Gameplay.Jump.performed += _ => stateMachine.ChangeState(player.JumpState);
    }

    public override void Execute()
    {
        base.Execute();

        //Move
        xInput = player.NormalizeInputX();

        //Jump 
        /*JumpInput = player.JumpInput;
        if (JumpInput)
        {
            player.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
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
