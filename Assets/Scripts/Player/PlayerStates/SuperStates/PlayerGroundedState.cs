using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Vector2 input; 

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

        player.RawPlayerInput.Gameplay.Jump.performed += _ => stateMachine.RequestState(player.JumpState);
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
       
        //player.RawPlayerInput.Gameplay.Move.ReadValue<Vector2>()
        input = player.SetNormInput();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
