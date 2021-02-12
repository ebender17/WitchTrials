using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }
    private bool isHolding;
    private Vector2 dashDirection; 
    private Vector2 dashDirectionInput;
    private bool dashInputStop; 
    private float lastDashTime;
    public PlayerDashState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        player.UseDashInput();

        isHolding = true;
        dashDirection = Vector2.right * player.FacingDirection;

        Time.timeScale = playerData.holdTimeScale;
        startTime = Time.unscaledTime;

        player.DashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Execute()
    {
        base.Execute();

        if(!isExitingState)
        {
            if(isHolding)
            {
                dashDirectionInput = player.GetDashDirectionInput();
                dashInputStop = player.DashInputStop;
                //Dash input states the same direction if not giving
                if(dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                // Subtract 45 as sprite starts at 45 
                player.DashDirectionIndicator.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 45.0f); 

                if(dashInputStop || Time.unscaledTime >= startTime + playerData.maxHoldTime)
                {
                    isHolding = false;
                    Time.timeScale = 1.0f;
                    startTime = Time.time;

                    player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    player.RB.drag = playerData.drag;
                    player.SetVelocity(playerData.dashVelocity, dashDirection);
                    player.DashDirectionIndicator.gameObject.SetActive(false);
                }

            }
            else
            {
                player.SetVelocity(playerData.dashVelocity, dashDirection);

                if (Time.time >= startTime + playerData.dashTime)
                {
                    player.RB.drag = 0.0f;
                    IsAbilityDone = true;
                    lastDashTime = Time.time; 
                }

            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        //Check if greater than 0 because we do not want to descrease our y velocity if we are jumping down
        if(player.CurrVelocity.y > 0)
        {
            player.SetVelocityY(player.CurrVelocity.y * playerData.dashEndYMultiplier);
        }
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.dashCoolDown;
    }

    public void ResetCanDash() => CanDash = true;

}
