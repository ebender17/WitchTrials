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
        //player.Anim.Play("Move");

    }

    public override void Execute()
    {
        base.Execute();

        player.CheckIfShouldFlip((int)input.x); 

        if(input.x == 0f)
        {
            stateMachine.RequestState(player.IdleState);
        }
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();

        player.SetVelocityX(playerData.movementVelocity * input.x);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
