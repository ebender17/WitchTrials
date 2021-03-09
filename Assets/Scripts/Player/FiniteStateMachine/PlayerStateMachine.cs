using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains reference to current Player state
// Removed MonoBehavior as it does not sit on a game object
public class PlayerStateMachine
{
    //Reference to current Player state 
    //Only able to set State in this script
    public PlayerState currentState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter(); 
    }
}
