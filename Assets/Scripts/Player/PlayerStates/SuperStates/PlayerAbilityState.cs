using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;

    private bool isGrounded; 
    public PlayerAbilityState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded(); 
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Execute()
    {
        base.Execute();

        if(isAbilityDone)
        {
            /*
             * Because jump ability is instantly over player will still be on ground when first jumping
             * Therefore if grounded and velocity is greater than 0.01 (meaning we just jumped) we
             * move to in air state
             */
           
            if (isGrounded && player.currVelocity.y < 0.01f )
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.inAirState);
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
