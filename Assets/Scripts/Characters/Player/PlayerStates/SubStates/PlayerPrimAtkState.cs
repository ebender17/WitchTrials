using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimAtkState : PlayerAbilityState
{
    public bool canPrimAtk { get; private set; }
    private float _lastPrimAtkTime;
    public PlayerPrimAtkState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
        this.stateName = StateNames.PrimAttack;
    }

    public override void Enter()
    {
        base.Enter();

        canPrimAtk = false;
        _lastPrimAtkTime = Time.time;
        player.UsePrimAtkInput();
    }

    public override void Execute()
    {
        base.Execute();

        if (isAnimationFinished)
        {
            isAbilityDone = true;
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

    public bool CheckIfCanPrimAtk()
    {
        return Time.time >= _lastPrimAtkTime + playerData.primAtkCoolDown;
    }

    public void ResetCanPrimAtk() => canPrimAtk = true;

}
