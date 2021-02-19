using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimAtkState : PlayerAbilityState
{
    public bool CanPrimAtk { get; private set; }
    private float lastPrimAtkTime;
    public PlayerPrimAtkState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();

        CanPrimAtk = false;
        player.InputHandler.UsePrimAtkInput();

        Debug.Log("Entered Attack State!");
        
    }

    public bool CheckIfCanPrimAtk()
    {
        return CanPrimAtk && Time.time >= lastPrimAtkTime + playerData.primAtkCoolDown;
    }

    public void ResetCanPrimAtk() => CanPrimAtk = true;

   
}
