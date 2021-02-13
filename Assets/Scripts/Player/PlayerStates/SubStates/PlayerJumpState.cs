using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Jump is simply used to set Y Velocity. 
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

        player.InputHandler.UseJumpInput();
        player.SetVelocityY(playerData.jumpVelocity);
        IsAbilityDone = true;
        numJumpsLeft--;
        Debug.Log("Entered jump state and decreased jumps!");
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        Debug.Log("Can Jump! " + numJumpsLeft);
        if (numJumpsLeft > 0) return true;
        else return false; 
    }

    public void ResetNumJumpsLeft() => numJumpsLeft = playerData.numJumps;
    public void DecreaseNumJumpsLeft() => numJumpsLeft--;

}
