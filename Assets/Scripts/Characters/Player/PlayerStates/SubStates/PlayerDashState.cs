using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool canDash { get; private set; }
    private bool _isHolding;
    private Vector2 _dashDirection; 
    private Vector2 _dashDirectionInput;
    private bool _dashInputStop; 
    private float _lastDashTime;
    public PlayerDashState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
        this.stateName = StateNames.Dash;
    }
    public override void Enter()
    {
        base.Enter();

        //Cannot dash again until touched the ground
        canDash = false;
        player.UseDashInput();

        _isHolding = true;
        //dash direction points in direction player is facing by default
        _dashDirection = Vector2.right * player.facingDirection;
        // go into slow motion mode by setting time scale
        Time.timeScale = playerData.holdTimeScale;
        
        // Want to keep track of how long we have been in hold state
        //so that we dash when time is up
        startTime = Time.unscaledTime;

        player.dashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Execute()
    {
        base.Execute();

        if(!isExitingState)
        {

            if(_isHolding)
            {
                _dashDirectionInput = player.dashDirectionInput;
                _dashInputStop = player.dashInputStop;
                
                //If we are not giving any input, dash direction will stay the same it was last dash
                if(_dashDirectionInput != Vector2.zero)
                {
                    _dashDirection = _dashDirectionInput;
                    _dashDirection.Normalize();
                }

                // returns angle in degrees between two angles 
                float angle = Vector2.SignedAngle(Vector2.right, _dashDirection);

                // Subtract 45 as sprite starts at 45 degree angle
                player.dashDirectionIndicator.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 45.0f); 

                //begin dash action
                if(_dashInputStop || Time.unscaledTime >= startTime + playerData.maxHoldTime)
                {
                    //go into actual dash movement
                    _isHolding = false;
                    //Reset time scale to normal
                    Time.timeScale = 1.0f;
                    /* 
                     * Track how long we have dashed for. If how long we have been in state 
                     * matters in the future, create a new variable for the value.
                     */
                    startTime = Time.time;

                    player.CheckIfShouldFlip(Mathf.RoundToInt(_dashDirection.x));
                    player.rigidBody.drag = playerData.drag;
                    player.SetVelocity(playerData.dashVelocity, _dashDirection);
                    player.dashDirectionIndicator.gameObject.SetActive(false);

                    playerData.SFXChannel.RaiseEvent(playerData.playerSounds.dash);
                }

            }
            //Sustain the dash action that was started
            else
            {
                player.SetVelocity(playerData.dashVelocity, _dashDirection);

                if (Time.time >= startTime + playerData.dashTime)
                {
                    player.rigidBody.drag = 0.0f;
                    isAbilityDone = true;
                    _lastDashTime = Time.time; 
                }

            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        //We do not want to descrease our y velocity if we are dashing down
        if(player.currVelocity.y > 0)
        {
            player.SetVelocityY(player.currVelocity.y * playerData.dashEndYMultiplier);
        }
    }

    public bool CheckIfCanDash()
    {
        return canDash && Time.time >= _lastDashTime + playerData.dashCoolDown;
    }

    public void ResetCanDash() => canDash = true;

}
