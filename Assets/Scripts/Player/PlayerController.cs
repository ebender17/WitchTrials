using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


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
    public PlayerPrimAtkState PrimAtkState { get; private set; }

    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Rigidbody2D RB { get; private set; }
    private BoxCollider2D BoxCollider;
    public Animator Anim { get; private set; }
    public PlayerInputActions PlayerInputHandler { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    #endregion

    #region Check Transforms

    [SerializeField]
    private Transform groundCheck;
    #endregion

    #region Input Variables 
    [SerializeField]
    private float inputHoldTime = 0.2f;

    //Jump
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    private float jumpInputStartTime; 

    //Dash 
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    private float dashInputStartTime;

    //Dash Direction 
    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }
    private Camera MainCam; 

    //PrimaryAttack 
    public bool PrimAtkInput { get; private set; }
    #endregion
    #region Control Schemes 
    //Gamepad 


    #endregion
    #region Other Variables 
    //Storing to avoid going to RB everytime we want RB velocity 
    public Vector2 CurrVelocity { get; private set; }
    private Vector2 tempVelocity;

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
        PrimAtkState = new PlayerPrimAtkState(this, StateMachine, playerData, "PrimAtk");

        // Initatiate player inputs
        PlayerInputHandler = new PlayerInputActions();

        //Jump Callback
        PlayerInputHandler.Gameplay.Jump.started += ctx => OnJumpInput(ctx);
        PlayerInputHandler.Gameplay.Jump.canceled += ctx => OnJumpInput(ctx);

        //Dash Callback 
        PlayerInputHandler.Gameplay.Dash.started += ctx => OnDashInput(ctx);
        PlayerInputHandler.Gameplay.Dash.canceled += ctx => OnDashInput(ctx);

        //Primary Attack Callback 
        PlayerInputHandler.Gameplay.PrimaryAttack.started += ctx => OnPrimAtkInput(ctx);
        PlayerInputHandler.Gameplay.PrimaryAttack.canceled += ctx => OnPrimAtkInput(ctx);

    }

    private void OnEnable()
    {
        PlayerInputHandler.Enable(); 
    }

    public void OnDisable()
    {
        PlayerInputHandler.Disable(); 
    }

    // Start is called before the first frame update
    private void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");

        MainCam = Camera.main; 

        // Player is in idle state upon game start 
        StateMachine.Initialize(IdleState);

        FacingDirection = 1;

    }

    // Update is called once per frame
    void Update()
    {
        CurrVelocity = RB.velocity; 
        StateMachine.CurrentState.Execute();

        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.ExecutePhysics();
    }
    #endregion

    #region InputFunctions
    // Normalize input so player moves with same speed on different input types
    // TODO: Check with controller. Also possible to do with input system.
    public int NormalizeInputX()
    {
        return (int)(PlayerInputHandler.Gameplay.Move.ReadValue<Vector2>() * Vector2.right).normalized.x;
    }

    public int NormalizeInputY()
    {
        return (int)(PlayerInputHandler.Gameplay.Move.ReadValue<Vector2>() * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void UseJumpInput() => JumpInput = false;

   
    //input is held at false until we use jump button or time runs out 
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime) JumpInput = false;
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            DashInputStop = true; 
        }

    }

    public Vector2 GetDashDirectionInput()
    {
        RawDashDirectionInput = PlayerInputHandler.Gameplay.DashDirection.ReadValue<Vector2>();

        //Convert from screen cords to vector from player to mouse
        //Need to check if current control scheme is keyboard somehow 
        RawDashDirectionInput = MainCam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position; 
        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
        return DashDirectionInput; 
        /*if(PlayerInputHandler.currentControlScheme == "Keyboard")
        {
            RawDashDirectionInput = MainCam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;
        }*/
    }

    public void UseDashInput() => DashInput = false;
    public void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime) DashInput = false;
    }

    public void OnPrimAtkInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            PrimAtkInput = true; 
            Debug.Log("Primary Attack Started"); 
        }

        if(context.canceled)
        {
            Debug.Log("Primary Attack Cancelled");
        }
    }
    #endregion

    #region SetFunctions
    public void SetVelocity(float velocity, Vector2 direction)
    {
        tempVelocity = direction * velocity;
        RB.velocity = tempVelocity;
        CurrVelocity = tempVelocity;
    }
    public void SetVelocityX(float velocity)
    {
        tempVelocity.Set(velocity, CurrVelocity.y);
        RB.velocity = tempVelocity;
        CurrVelocity = tempVelocity; 
    }

    public void SetVelocityY(float velocity)
    {
        tempVelocity.Set(CurrVelocity.x, velocity);
        RB.velocity = tempVelocity;
        CurrVelocity = tempVelocity;
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
        //Physics2D.BoxCast(BoxCollider.bounds.center, BoxCollider.bounds.size, 0.0f, Vector2.down, playerData.groundCheckDistance, playerData.whatIsGround);
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
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
    #endregion
    
}
