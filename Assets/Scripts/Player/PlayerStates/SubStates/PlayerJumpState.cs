using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Jump is simplied used to set Y Velocity. 
 */
public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityY(playerData.jumpVelocity);
        IsAbilityDone = true; 
    }
}
