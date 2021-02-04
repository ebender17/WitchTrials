using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        // Stop movement when enter idle state
        player.SetVelocityX(0.0f);
        //player.Anim.Play("Idle");

    }

    public override void Execute()
    {
        base.Execute();

        if(input.x != 0)
        {
            stateMachine.RequestState(player.MoveState);
        }
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
