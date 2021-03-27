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
   public AnimationToStateMachine atsm { get; private set; }

   [SerializeField] private Transform _wallCheck;
   [SerializeField] private Transform _ledgeCheck;

   [SerializeField] private Transform _playerCheck;
   [SerializeField] private Transform _groundCheck;

   public int facingDirection { get; private set; }
   private Vector2 tempVelocity;

   private int currentHealth;
   private float currentStunResistance;
   private float lastDamageTime;
   protected bool isStunned; //flag used to transition in enemy specific stun state
   protected bool isDead; //flag used to transition in enemy specific dead state
    public int lastDamageDirection { get; private set; }


    public virtual void Start()
    {
        facingDirection = 1;
        currentHealth = entityData.health;
        currentStunResistance = entityData.stunResistance;

        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        atsm = aliveGO.GetComponent<AnimationToStateMachine>();

        stateMachine = new EntityStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.Execute();

        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime && isStunned)
            ResetStunResistance();
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

    public virtual void SetVelocityAndAngle(float speed, Vector2 angle, int direction)
    {
        angle.Normalize();
        tempVelocity.Set(angle.x * speed * direction, angle.y * speed);
        rb.velocity = tempVelocity;

    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(_wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
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

    public bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(_playerCheck.position, aliveGO.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, entityData.groundCheckRadius, entityData.whatIsGround);
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void DamageHop(float velocity)
    {
        tempVelocity.Set(rb.velocity.x, velocity);
        rb.velocity = tempVelocity;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }
    public virtual void TakeDamage(float playerXPox, int damage)
    {
        lastDamageTime = Time.time;
        currentStunResistance--;
        currentHealth -= damage;

        DamageHop(entityData.damageHopSpeed);

        Instantiate(entityData.hitParticle, aliveGO.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if(playerXPox > aliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if (currentStunResistance <= 0)
            isStunned = true;

        if(currentHealth <= 0)
            isDead = true;
    }

#if UNITY_EDITOR 
    public virtual void OnDrawGizmos()
    {
        //Wall and ledge checks 
        //Can only see when came is running and facingDirection is set at Start
        Gizmos.DrawLine(_wallCheck.position, _wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(_ledgeCheck.position, _ledgeCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.ledgeCheckDistance));

        //Close range action distance, min agro distance and max agro distance
        Gizmos.DrawWireSphere(_playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(_playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(_playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
        
    }
#endif


}
