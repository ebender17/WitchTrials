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

    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter(); 
    }
}
