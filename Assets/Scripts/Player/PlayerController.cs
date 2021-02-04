using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Rigidbody2D RB { get; private set; }
    private BoxCollider2D BoxCollider;
    // Have a getter so states has access 
    public Animator Anim { get; private set; }
    public PlayerInputActions RawPlayerInput { get; private set; }
    #endregion

    #region Check Transform 
    //private Transform groundCheck;
    #endregion

    #region Other Variables 
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    
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

        // Initatiate player inputs
        RawPlayerInput = new PlayerInputActions();
    }

    private void OnEnable()
    {
        RawPlayerInput.Enable(); 
    }

    public void OnDisable()
    {
        RawPlayerInput.Disable(); 
    }

    // Start is called before the first frame update
    private void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();

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
    // Normalize input so player moves with same speed on different input types
    public Vector2 SetNormInput()
    {
        NormInputX = (int)(RawPlayerInput.Gameplay.Move.ReadValue<Vector2>() * Vector2.right).normalized.x;
        NormInputY = (int)(RawPlayerInput.Gameplay.Move.ReadValue<Vector2>() * Vector2.up).normalized.y;

        return new Vector2(NormInputX, NormInputY);
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
        return Physics2D.BoxCast(BoxCollider.bounds.center, BoxCollider.bounds.size, 0.0f, Vector2.down, playerData.groundCheckDistance, playerData.whatIsGround);
    }
    #endregion

    #region Other Functions
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
    
}
