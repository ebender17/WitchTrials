using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimAtkState : PlayerAbilityState
{
    public bool canPrimAtk { get; private set; }
    private float _lastPrimAtkTime;
    public PlayerPrimAtkState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();

        canPrimAtk = false;
        player.UseAtkInput();

        Debug.Log("Entered Attack State!");
        
    }

    public bool CheckIfCanPrimAtk()
    {
        return canPrimAtk && Time.time >= _lastPrimAtkTime + playerData.primAtkCoolDown;
    }

    public void ResetCanPrimAtk() => canPrimAtk = true;

   
}
