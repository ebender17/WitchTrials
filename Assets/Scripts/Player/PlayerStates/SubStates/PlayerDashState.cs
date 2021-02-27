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

        //Cannot dash again until touched the ground
        CanDash = false;
        player.InputHandler.UseDashInput();

        isHolding = true;
        //dash direction points in direction player is facing by default
        dashDirection = Vector2.right * player.FacingDirection;
        // go into slow motion mode by setting time scale
        Time.timeScale = playerData.holdTimeScale;
        
        // Want to keep track of how long we have been in hold state
        //so that we dash when time is up
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
                dashDirectionInput = player.InputHandler.DashDirectionInput;
                dashInputStop = player.InputHandler.DashInputStop;
                
                //If we are not giving any input, dash direction will stay the same it was last dash
                if(dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }

                // returns angle in degrees between two angles 
                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);

                // Subtract 45 as sprite starts at 45 degree angle
                player.DashDirectionIndicator.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 45.0f); 

                //begin dash action
                if(dashInputStop || Time.unscaledTime >= startTime + playerData.maxHoldTime)
                {
                    //go into actual dash movement
                    isHolding = false;
                    //Reset time scale to normal
                    Time.timeScale = 1.0f;
                    /* 
                     * Track how long we have dashed for. If how long we have been in state 
                     * matters in the future, create a new variable for the value.
                     */
                    startTime = Time.time;

                    player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    player.RB.drag = playerData.drag;
                    player.SetVelocity(playerData.dashVelocity, dashDirection);
                    player.DashDirectionIndicator.gameObject.SetActive(false);
                }

            }
            //Sustain the dash action that was started
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

        //We do not want to descrease our y velocity if we are dashing down
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
