using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player transitions to Jump for a split second before moving to InAir state. 
/// Used to set Y Velocity. 
/// </summary>
public class PlayerJumpState : PlayerAbilityState
{
    private int numJumpsLeft;
    public PlayerJumpState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
        numJumpsLeft = playerData.numJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseJumpInput();
        player.SetVelocityY(playerData.jumpVelocity);
        IsAbilityDone = true;
        numJumpsLeft--;
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (numJumpsLeft > 0) return true;
        else return false; 
    }

    public void ResetNumJumpsLeft() => numJumpsLeft = playerData.numJumps;
    public void DecreaseNumJumpsLeft() => numJumpsLeft--;

}
