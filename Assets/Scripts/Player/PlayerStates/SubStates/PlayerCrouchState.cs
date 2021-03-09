using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerGroundedState
{
    public PlayerCrouchState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetColliderHeight(playerData.crouchColliderHeight);
    }

    public override void Execute()
    {
        base.Execute();

        if(!isExitingState)
        {
            player.SetVelocityX(playerData.crouchMovementVelocity * player.facingDirection);
            player.CheckIfShouldFlip(xInput);

            if(xInput == 0)
            {
                stateMachine.ChangeState(player.crouchIdleState);
            }
            // No longer holding down key
            else if (yInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(player.moveState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeight(playerData.standColliderHeight);
    }
}
