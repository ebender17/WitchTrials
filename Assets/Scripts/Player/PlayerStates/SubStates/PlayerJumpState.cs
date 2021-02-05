using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Jump is simplied used to set Y Velocity. 
 */
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
