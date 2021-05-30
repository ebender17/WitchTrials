using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
        this.stateName = StateNames.Land;
    }

    public override void Enter()
    {
        base.Enter();

        //playerData.SFXChannel.RaiseEvent(playerData.playerSounds.land);
    }

    public override void Execute()
    {
        base.Execute();

        if(xInput != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }
        else if(isAnimationFinished)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
