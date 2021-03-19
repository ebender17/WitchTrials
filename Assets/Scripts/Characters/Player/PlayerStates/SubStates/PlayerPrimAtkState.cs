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
        player.UsePrimAtkInput();

        Debug.Log("Entered Attack State!");
        
    }

    public override void Execute()
    {
        base.Execute();

        //able to move while in attack animation 

        //isAbilityDone = true; when animation is done playing -> similar to LandState?
        if (isAnimationFinished)
        {
            isAbilityDone = true;
            _lastPrimAtkTime = Time.time;

        }

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(player.attackPoint.position, playerData.primAtkRange, playerData.whatIsDamagable); 

        //Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
        }
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exited Attack State!");
    }

    public bool CheckIfCanPrimAtk()
    {
        //return canPrimAtk && Time.time >= _lastPrimAtkTime + playerData.primAtkCoolDown;
        return Time.time >= _lastPrimAtkTime + playerData.primAtkCoolDown;
    }

    public void ResetCanPrimAtk() => canPrimAtk = true;

}
