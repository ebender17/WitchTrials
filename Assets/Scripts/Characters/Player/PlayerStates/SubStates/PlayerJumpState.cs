using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player transitions to Jump for a split second before moving to InAir state. 
/// Used to set Y Velocity. 
/// </summary>
public class PlayerJumpState : PlayerAbilityState
{
    private int _numJumpsLeft;
    public PlayerJumpState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
        _numJumpsLeft = playerData.numJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.UseJumpInput();
        player.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        _numJumpsLeft--;
        player.inAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (_numJumpsLeft > 0) return true;
        else return false; 
    }

    public void ResetNumJumpsLeft() => _numJumpsLeft = playerData.numJumps;
    public void DecreaseNumJumpsLeft() => _numJumpsLeft--;

}
