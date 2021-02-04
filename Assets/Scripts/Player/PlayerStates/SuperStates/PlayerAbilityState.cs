using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool IsAbilityDone;
    private bool IsGrounded; 
    public PlayerAbilityState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        IsGrounded = player.CheckIfGrounded(); 
    }

    public override void Enter()
    {
        base.Enter();

        IsAbilityDone = false;
    }

    public override void Execute()
    {
        base.Execute();
        if(IsAbilityDone)
        {
            /*
             * Because jump ability is instantly over player will still be on ground when first jumping
             * Therefore if grounded and velocity is greater than 0.01 (meaning we just jumped) we
             * move to in air state
             */
           
            if (IsGrounded && player.CurrVelocity.y < 0.01f )
            {
                stateMachine.RequestState(player.IdleState);
            }
            else
            {
                stateMachine.RequestState(player.InAirState);
            }
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
