using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player controller. Stores and executes player's state. 
/// </summary>

public class PlayerController : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    // Initialize Player States 
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchState CrouchMoveState { get; private set; }
    public PlayerPrimAtkState PrimAtkState { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    #endregion

    #region Components
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public BoxCollider2D BoxCollider { get; private set; }
    public Animator Anim { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }

    #endregion

    #region Check Transforms

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform ceilingCheck;

    #endregion

    #region Other Variables 
    //Storing to avoid going to RB everytime we want RB velocity 
    public Vector2 CurrVelocity { get; private set; }
    private Vector2 tempValue;

    public int FacingDirection { get; private set; }
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "InAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "InAir"); 
        LandState = new PlayerLandState(this, StateMachine, playerData, "Land");
        DashState = new PlayerDashState(this, StateMachine, playerData, "InAir");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "IdleCrouch");
        CrouchMoveState = new PlayerCrouchState(this, StateMachine, playerData, "MoveCrouch");
        PrimAtkState = new PlayerPrimAtkState(this, StateMachine, playerData, "PrimAtk");

    }

    // Start is called before the first frame update
    private void Start()
    {
        InputHandler = GetComponent<PlayerInputHandler>();
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");

        // Player is in idle state upon game start 
        StateMachine.Initialize(IdleState);

        FacingDirection = 1;

    }

    // Update is called once per frame
    void Update()
    {
        CurrVelocity = RB.velocity; 
        StateMachine.CurrentState.Execute();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.ExecutePhysics();
    }
    #endregion

    #region SetFunctions

    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrVelocity = Vector2.zero;
    }
    public void SetVelocity(float velocity, Vector2 direction)
    {
        tempValue = direction * velocity;
        RB.velocity = tempValue;
        CurrVelocity = tempValue;
    }
    public void SetVelocityX(float velocity)
    {
        tempValue.Set(velocity, CurrVelocity.y);
        RB.velocity = tempValue;
        CurrVelocity = tempValue; 
    }

    public void SetVelocityY(float velocity)
    {
        tempValue.Set(CurrVelocity.x, velocity);
        RB.velocity = tempValue;
        CurrVelocity = tempValue;
    }
    
    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckForCeiling()
    {
        return Physics2D.OverlapCircle(ceilingCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    #endregion

    #region Other Functions
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    /*
     * Used to create events with animations in Unity editor. 
     * For example, after land animation this function is triggered. 
     * Do so here as animations only have access to functions in script
     * attached to object of animations. 
     */
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger(); 
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void SetColliderHeight(float height)
    {
        Vector2 center = BoxCollider.offset;
        tempValue.Set(BoxCollider.size.x, height);
        // For every 2 units our size decreases, the offset decrease 1 unit 
        center.y += (height - BoxCollider.size.y) / 2;

        BoxCollider.size = tempValue;
        BoxCollider.offset = center;
    }
    #endregion
    
}
