using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void Execute()
    {
        base.Execute();

        if(xInput != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        else if(isAnimationFinished)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
