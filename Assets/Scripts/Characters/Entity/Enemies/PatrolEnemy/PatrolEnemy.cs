using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : Entity
{
    public PatrolEnemy_Move moveState { get; private set; }
    public PatrolEnemy_Idle idleState { get; private set; }
    public PatrolEnemy_StunState stunState { get; private set; }
    public PatrolEnemy_DeadState deadState { get; private set; }

    [SerializeField]
    private EntityMoveStateSO _moveStateData;
    [SerializeField]
    private EntityIdleStateSO _idleStateData;
    [SerializeField]
    private EntityStunStateSO _stunStateData;
    [SerializeField]
    private EntityDeadStateSO _deadStateData;

    private float lastTouchDamage;
    public float touchDamageCooldown = 1f;
    public float attackRadius = 0.5f;
    [SerializeField] private Transform _touchDamagePosition = default;


    public override void Start()
    {
        base.Start();

        moveState = new PatrolEnemy_Move(this, stateMachine, "Move", _moveStateData, this);
        idleState = new PatrolEnemy_Idle(this, stateMachine, "Idle", _idleStateData, this);
        stunState = new PatrolEnemy_StunState(this, stateMachine, "Stun", _stunStateData, this);
        deadState = new PatrolEnemy_DeadState(this, stateMachine, "Dead", _deadStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void TakeDamage(float playerXPox, int damage)
    {
        base.TakeDamage(playerXPox, damage);

        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
    }

    public void CheckTouchDamage()
    {
        if(Time.time >= lastTouchDamage + touchDamageCooldown)
        {
            Collider2D player = Physics2D.OverlapCircle(_touchDamagePosition.position, attackRadius, entityData.whatIsPlayer);

            if(player != null)
            {
                lastTouchDamage = Time.time;

                PlayerController playerController = player.GetComponent<PlayerController>();
                
                if (playerController)
                    playerController.TakeDamage(aliveGO.transform.position.x, entityData.touchDamage);
            }

            /*foreach (Collider2D player in detectedObjects)
            {
                lastTouchDamage = Time.time;

                PlayerController playerController = player.GetComponent<PlayerController>();

                if (playerController)
                    playerController.TakeDamage(aliveGO.transform.position.x, entityData.touchDamage);
            }*/
        } 
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
