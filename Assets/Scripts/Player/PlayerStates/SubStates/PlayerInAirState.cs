using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private Vector2 input; 

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

        input = player.SetNormInput();

        /*
        * Because jump ability is instantly over player will still be on ground when first jumping
        * Therefore if grounded and velocity is greater than 0.01 (meaning we just jumped) we
        * move to in air state
        */

        if (isGrounded && player.CurrVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else
        {
            player.CheckIfShouldFlip((int)input.x);
            player.SetVelocityX(playerData.movementVelocity * input.x); 
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
