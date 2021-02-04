using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains reference to current Player state
// Removed MonoBehavior as it does not sit on a game object
public class PlayerStateMachine
{
    //Reference to current Player state 
    //Only able to set State in this script
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    // Takes place of blend tree 
    // TODO: Do we need requeststate? May not due to how FMS is structed. Only correct states will be called from within functions. 
    public void RequestState(PlayerState requestedState)
    {
        //Idle 
        if (requestedState is PlayerIdleState)
        {
            ChangeState(requestedState);
        }

        //Move (Walk)
        if (requestedState is PlayerMoveState)
        {
            if(CurrentState is PlayerIdleState || CurrentState is PlayerAbilityState)
            {
                ChangeState(requestedState);
            }
        }

        //Jump
        if(requestedState is PlayerJumpState)
        {
            if(CurrentState is PlayerGroundedState)
            {
                ChangeState(requestedState);
            }
        }

        //InAir 
        if(requestedState is PlayerInAirState)
        {
            if(CurrentState is PlayerAbilityState)
            {
                ChangeState(requestedState);
            }
        }

        //Land 
        if(requestedState is PlayerLandState)
        {
            if(CurrentState is PlayerInAirState)
            {
                ChangeState(requestedState);
            }
        }
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter(); 
    }
}
