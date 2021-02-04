using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Execute()
    {
        base.Execute();

        player.CheckIfShouldFlip(xInput);

        player.SetVelocityX(playerData.movementVelocity * xInput);

        if (xInput == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();


       // player.SetVelocityX(playerData.movementVelocity * xInput);

    }

    public override void Exit()
    {
        base.Exit();
    }
}
