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
    public PlayerStateMachine stateMachine { get; private set; }

    // Initialize Player States 
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerLandState landState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerCrouchIdleState crouchIdleState { get; private set; }
    public PlayerCrouchState crouchMoveState { get; private set; }
    public PlayerPrimAtkState primAtkState { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    #endregion

    #region Components

    private Camera _cam;

    public InputReader inputReader = default;
    //public PlayerInputHandler inputHandler { get; private set; }
    public Rigidbody2D rigidBody { get; private set; }
    public BoxCollider2D boxCollider { get; private set; }
    public Animator anim { get; private set; }
    public Transform dashDirectionIndicator { get; private set; }

    #endregion

    #region Input 
    public Vector2 rawMovementInput { get; private set; }
    public Vector2 rawDashDirectionInput { get; private set; }
    public Vector2Int dashDirectionInput { get; private set; }
    public int normInputX { get; private set; }
    public int normInputY { get; private set; }
    public bool jumpInput { get; private set; }
    public bool jumpInputStop { get; private set; }
    public bool dashInput { get; private set; }
    public bool dashInputStop { get; private set; }
    public bool primAtkInput { get; private set; }
    public bool primAtkInputStop { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float dashInputStartTime;
    private float primAtkInputStartTime;
    #endregion


    #region Check & Attack Transforms

    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private Transform _ceilingCheck;

    public Transform attackPoint;

    #endregion

    #region Other Variables 
    //Storing to avoid going to RB everytime we want RB velocity 
    public Vector2 currVelocity { get; private set; }
    private Vector2 _tempValue;

    public int facingDirection { get; private set; }
    public bool knockBack { get; private set; } = false;
    public float knockBackStartTime { get; private set; }

    public float currentHealth { get; private set; }

    public float playerScore { get; private set; }
    #endregion

    #region EventChannels
    [Header("Broadcasting on channels")]
    [SerializeField] private GameResultChannelSO _playerResults;

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, playerData, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, playerData, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, playerData, "InAir");
        inAirState = new PlayerInAirState(this, stateMachine, playerData, "InAir"); 
        landState = new PlayerLandState(this, stateMachine, playerData, "Land");
        dashState = new PlayerDashState(this, stateMachine, playerData, "InAir");
        crouchIdleState = new PlayerCrouchIdleState(this, stateMachine, playerData, "IdleCrouch");
        crouchMoveState = new PlayerCrouchState(this, stateMachine, playerData, "MoveCrouch");
        primAtkState = new PlayerPrimAtkState(this, stateMachine, playerData, "PrimAttack");

    }

    private void OnEnable()
    {
        inputReader.moveEvent += OnMove;
        inputReader.jumpEvent += OnJump;
        inputReader.jumpCanceledEvent += OnJumpCanceled;
        inputReader.dashEvent += OnDash;
        inputReader.dashCanceledEvent += OnDashCanceled;
        inputReader.dashDirectionEvent += OnDashDirection;
        inputReader.attackEvent += OnPrimAttack;
        inputReader.attackCanceledEvent += OnPrimAttackCanceled;
        
    }

    private void OnDisable()
    {
        inputReader.moveEvent -= OnMove;
        inputReader.jumpEvent -= OnJump;
        inputReader.jumpCanceledEvent -= OnJumpCanceled;
        inputReader.dashEvent -= OnDash;
        inputReader.dashCanceledEvent -= OnDashCanceled;
        inputReader.dashDirectionEvent -= OnDashDirection;
        inputReader.attackEvent -= OnPrimAttack;
        inputReader.attackCanceledEvent -= OnPrimAttackCanceled;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _cam = Camera.main;
        //inputHandler = GetComponent<PlayerInputHandler>();
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        dashDirectionIndicator = transform.Find("DashDirectionIndicator");

        // Player is in idle state upon game start 
        stateMachine.Initialize(idleState);

        facingDirection = 1;

        currentHealth = playerData.health;
        playerScore = playerData.score;

    }

    // Update is called once per frame
    void Update()
    {
        currVelocity = rigidBody.velocity; 
        stateMachine.currentState.Execute();

        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
        CheckKnockback();
        CheckPlayerFall();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.ExecutePhysics();
    }
    #endregion

    #region Event Listeners
    private void OnMove(Vector2 movement)
    {
        rawMovementInput = movement;

        // Normalize input so player moves with same speed on different input types
        // TODO: Check with controller. Also possible to do with input system.
        if (Mathf.Abs(rawMovementInput.x) > 0.5f)
        {
            normInputX = (int)(rawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            normInputX = 0;
        }

        if (Mathf.Abs(rawMovementInput.y) > 0.5f)
        {
            normInputY = (int)(rawMovementInput * Vector2.up).normalized.y;
        }
        else
        {
            normInputY = 0;
        }

    }

    private void OnJump()
    {
        jumpInput = true;
        jumpInputStop = false;
        jumpInputStartTime = Time.time;
    }

    void OnJumpCanceled() => jumpInputStop = true;

    public void UseJumpInput() => jumpInput = false;
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            jumpInput = false;
        }
    }

    private void OnDash()
    {
        dashInput = true;
        dashInputStop = false;
        dashInputStartTime = Time.time;
    }

    private void OnDashCanceled() => dashInputStop = true;

    private void OnDashDirection(Vector2 rawDashDirection, bool isDeviceMouse)
    {
        rawDashDirectionInput = rawDashDirection;

        if (isDeviceMouse)
        {
            // subtract player's transform to get vector pointing from player to world point
            rawDashDirectionInput = _cam.ScreenToWorldPoint(rawDashDirectionInput) - transform.position;
        }

        // normalize as magnitude does not matter, just direction
        dashDirectionInput = Vector2Int.RoundToInt(rawDashDirectionInput.normalized);
    }

    public void UseDashInput() => dashInput = false;

    private void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            dashInput = false;
        }
    }

    private void OnPrimAttack()
    {
        primAtkInput = true;
        primAtkInputStop = false;
        primAtkInputStartTime = Time.time;
    }

    private void OnPrimAttackCanceled() => primAtkInput = true; 

    public void UsePrimAtkInput() => primAtkInput = false;

    #endregion

    #region SetFunctions

    public void SetVelocityZero()
    {
        rigidBody.velocity = Vector2.zero;
        currVelocity = Vector2.zero;
    }
    public void SetVelocity(float velocity, Vector2 direction)
    {
        _tempValue = direction * velocity;
        rigidBody.velocity = _tempValue;
        currVelocity = _tempValue;
    }
    public void SetVelocityX(float velocity)
    {
        _tempValue.Set(velocity, currVelocity.y);
        rigidBody.velocity = _tempValue;
        currVelocity = _tempValue; 
    }

    public void SetVelocityY(float velocity)
    {
        _tempValue.Set(currVelocity.x, velocity);
        rigidBody.velocity = _tempValue;
        currVelocity = _tempValue;
    }

    
    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckForCeiling()
    {
        return Physics2D.OverlapCircle(_ceilingCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public void CheckKnockback()
    {
        if(Time.time >= knockBackStartTime + playerData.knockBackDuration && knockBack)
        {
            knockBack = false;
            SetVelocityX(0.0f);
        }
    }

    public void CheckPlayerFall()
    {
        if (rigidBody.position.y < -100.0f)
            DecreaseHealth(50);
    }

    #endregion

    #region Other Functions
    private void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();

    /*
     * Used to create events with animations in Unity editor. 
     * For example, after land animation this function is triggered. 
     * Do so here as animations only have access to functions in script
     * attached to object of animations. 
     */
    private void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger(); 
    private void Flip()
    {
        if(!knockBack)
        {
            facingDirection *= -1;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    public void SetColliderHeight(float height)
    {
        Vector2 center = boxCollider.offset;
        _tempValue.Set(boxCollider.size.x, height);
        // For every 2 units our size decreases, the offset decrease 1 unit 
        center.y += (height - boxCollider.size.y) / 2;

        boxCollider.size = _tempValue;
        boxCollider.offset = center;
    }
    #endregion

    #region Damage Functions
    public void TakeDamage(float enemyXPos, int damage)
    {
        if (stateMachine.currentState.stateName != StateNames.Dash)
        {
            int knockbackDirection;

            DecreaseHealth(damage);

            if (enemyXPos < transform.position.x)
                knockbackDirection = 1;
            else
                knockbackDirection = -1;

            KnockBack(knockbackDirection);

        }
    }
    private void KnockBack(int knockbackDirection)
    {
        knockBack = true;
        knockBackStartTime = Time.time;

        SetVelocity(knockbackDirection, playerData.knockBackSpeed);
    }

    private void DecreaseHealth(int damage)
    {
        currentHealth -= damage;

        Debug.Log(currentHealth);
        //TODO: damage anim 

        if(currentHealth <= 0.0f)
        {
            Death();
        }
    }

    private void Death()
    {
        //TODO: player death particles
        _playerResults.RaiseEvent(false, playerScore.ToString());
        Destroy(gameObject);
    }
    #endregion

    #region Gizmos 
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return; 

        Gizmos.DrawWireSphere(attackPoint.position, playerData.primAtkRange);

    }
    #endregion

}
