using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;
    private bool _jumpInput;
    private bool _dashInput; 
    private bool _isGrounded;
    private bool _primAtkInput;

    protected bool isTouchingCeiling;

    public PlayerGroundedState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
        this.stateName = StateNames.Grounded;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = player.CheckIfGrounded();
        isTouchingCeiling = player.CheckForCeiling();

    }

    public override void Enter()
    {
        base.Enter();

        player.jumpState.ResetNumJumpsLeft();
        player.dashState.ResetCanDash(); 

    }

    public override void Execute()
    {
        base.Execute();

        //Get inputs
        xInput = player.normInputX;
        yInput = player.normInputY;
        _jumpInput = player.jumpInput;
        _dashInput = player.dashInput;
        _primAtkInput = player.primAtkInput;


        //Jump 
        //Make sure movement cannot override knockback 
        if (_jumpInput && player.jumpState.CanJump() && !player.knockBack)
        {
            //player.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);
        } 
        // Player falls off ground
        else if(!_isGrounded)
        {
            player.inAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.inAirState);
        }
        //Dash
        //TODO: do we want to dash from ground?
        else if (_dashInput && player.dashState.CheckIfCanDash() && !isTouchingCeiling && !player.knockBack)
        {
            stateMachine.ChangeState(player.dashState);
        }
        //Primary Attack
        else if(_primAtkInput && player.primAtkState.CheckIfCanPrimAtk() && !isTouchingCeiling && !player.knockBack)
        {
            Debug.Log("Switching to primary Attack State");
            stateMachine.ChangeState(player.primAtkState);
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

}
