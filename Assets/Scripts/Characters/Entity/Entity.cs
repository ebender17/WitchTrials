using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Parent class for enemy classes. 
/// Will not be placed on game object, but child will. 
/// Therefore, it is a MonoBehaviour.
/// </summary>
public class Entity : MonoBehaviour
{
   public EntityStateMachine stateMachine;
    public EntitySO entityData;
   public Rigidbody2D rb { get; private set; }
   public Animator anim { get; private set; }
   public GameObject aliveGO { get; private set; }

    [SerializeField] private Transform _wallCheck;
    [SerializeField] private Transform _ledgeCheck;

    [SerializeField] private Transform _playerCheck;

    public int facingDirection { get; private set; }
    private Vector2 tempVelocity; 

    public virtual void Start()
    {
        facingDirection = 1;

        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();

        stateMachine = new EntityStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.Execute();
    }

    public virtual void FixedUpdate()
    {

        stateMachine.currentState.ExecutePhysics();
    }

    public virtual void SetVelocityX(float velocity)
    {
        tempVelocity.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = tempVelocity;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(_wallCheck.position, aliveGO.transform.right, entityData.walkCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(_ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);

    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(_playerCheck.position, aliveGO.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(_playerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }


    public virtual void OnDrawGizmos()
    {
        //Can only see when came is running and facingDirection is set at Start
        Gizmos.DrawLine(_wallCheck.position, _wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.walkCheckDistance));
        Gizmos.DrawLine(_ledgeCheck.position, _ledgeCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.ledgeCheckDistance));
    }

}
