using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    // Initialize Player States 
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }


    public PlayerInputActions RawPlayerInput { get; private set; }
    // public int NormInputX { get; private set; }
   // public int NormInputY { get; private set; }
    
    public Rigidbody2D RB { get; private set; }

    // Have a getter so states has access 
    public Animator Anim { get; private set; }

    
    [SerializeField]
    private PlayerData playerData;

    private Vector2 tempVelocity;

    //Storing to avoid going to RB everytime we want RB velocity 
    public Vector2 CurrVelocity { get; private set; }

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "Move");

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

        // Player is in idle state upon game start 
        StateMachine.Initialize(IdleState);

        //TODO: Normalizing input data so player does not move faster on controller

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

    public void SetVelocityX(float velocity)
    {
        tempVelocity.Set(velocity, CurrVelocity.y);
        RB.velocity = tempVelocity;
        CurrVelocity = tempVelocity; 
    }
}
