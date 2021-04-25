using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private float travelDistance;
    private float startPosX;

    private Rigidbody2D rb;

    private bool isGravityOn;
    private bool hasHitGround;

    [SerializeField] private float _gravity;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform damagePosition;
    [SerializeField] private float damageRadius;
    private uint damageAmount;

    private void Start()
    {
        startPosX = transform.position.x;

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;

        isGravityOn = false;

    }

    private void Update()
    {
        if(!hasHitGround)
        {
            if(isGravityOn)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate()
    {
        if(!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround); 

            if(damageHit)
            {
                PlayerController playerController = damageHit.GetComponent<PlayerController>();

                if (playerController)
                    playerController.TakeDamage(transform.position.x, damageAmount, false);
              
                Destroy(gameObject);
            }

            if(groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }

            if (Mathf.Abs(startPosX - transform.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                rb.gravityScale = _gravity;
            }

        }
        
    }

    public void FireProjectile(float speed, float travelDistance, uint damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        this.damageAmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
