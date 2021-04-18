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
    public bool canFlip { get; private set; }

    private float _currentHealth;

    private int _playerScore;

    private Vector2 _currentCheckpoint; 
    public Vector2 CurrentCheckpoint
    {
        get { return _currentCheckpoint; }
        set { _currentCheckpoint = value; }
    }

    [SerializeField] private float _fallHeight;

    bool isInvincible;
    float invincibleTimer; //used to regulate how player can be hurt

    #endregion

    #region EventChannels
    [Header("Broadcasting on channels")]

    [Tooltip("Event for communicating game result")]
    [SerializeField] private GameResultChannelSO _playerResults;

    [Tooltip("Events for updating HUD")]
    [SerializeField] private FloatEventChannelSO _playerHealthUI;
    [SerializeField] private IntEventChannelSO _playerScoreUI;


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
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        dashDirectionIndicator = transform.Find("DashDirectionIndicator");

        // Player is in idle state upon game start 
        stateMachine.Initialize(idleState);

        facingDirection = 1;

        _currentHealth = playerData.maxHealth;
        _currentCheckpoint = transform.position;
        _playerScore = 0;
        canFlip = true;

    }

    // Update is called once per frame
    void Update()
    {
        currVelocity = rigidBody.velocity;
        stateMachine.currentState.Execute();

        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
        CheckKnockback();
        CheckPlayerPosition();
        CheckInvincible();
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

    private void OnPrimAttackCanceled() => primAtkInput = false; 

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

    public void EnableFlip() => canFlip = true;

    public void DisableFlip() => canFlip = false;
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

    public void CheckPlayerPosition()
    {
        if (rigidBody.position.y < playerData.fallHeightCutoff)
        {
            DecreaseHealth(playerData.fallDamage);

            if (_currentHealth > 0)
                LoadLastCheckpoint();
        }     
    }

    //Called during primAtk anim in editor
    private void CheckAttackHitBox()
    {
        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, playerData.primAtkRange, playerData.whatIsDamagable);

        //Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            Entity entity = enemy.GetComponentInParent<Entity>();
            if (entity != null)
                entity.TakeDamage(gameObject.transform.position.x, playerData.attackDamage);
        }
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
        if(canFlip && !knockBack)
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

    private void LoadLastCheckpoint()
    {
        transform.position = _currentCheckpoint;
    }

    public void CollectableGathered()
    {
        _playerScore++;

        _playerScoreUI.RaiseEvent(_playerScore);

    }

    #endregion

    #region Damage and Health Functions
    private void CheckInvincible()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    public void TakeDamage(float enemyXPos, uint damage, bool isSpike)
    {
        if (stateMachine.currentState.stateName != StateNames.Dash)
        {
            Debug.Log("Take Damage called.");

            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = playerData.timeInvincible;

            int knockbackDirection;

            DecreaseHealth(damage);

            if (enemyXPos < transform.position.x)
                knockbackDirection = 1;
            else
                knockbackDirection = -1;

            KnockBack(knockbackDirection, isSpike);

        }
    }
    private void KnockBack(int knockbackDirection, bool isSpike)
    {
        knockBack = true;
        knockBackStartTime = Time.time;

        if(isSpike)
            SetVelocityY(playerData.knockBackSpeedSpike);
        else
            SetVelocity(knockbackDirection, playerData.knockBackSpeed);
        
    }

    private void DecreaseHealth(uint damage)
    {
        Debug.Log("Inside decrease health.");

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, playerData.maxHealth);

        //Raising event to change healthbar UI
        if(_playerHealthUI != null)
            _playerHealthUI.OnEventRaised(_currentHealth / playerData.maxHealth);

        //Raising event to play player hit SFX
        if(playerData.SFXEventChannel != null)
            playerData.SFXEventChannel.RaisePlayEvent(AudioClipName.PlayerHit);

        //TODO: damage anim 

        if(_currentHealth <= 0.0f)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("Inside Death.");
        //TODO: player death particles
        _playerResults.RaiseEvent(false, _playerScore.ToString());
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
