using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMeleeAttackState : EntityAttackState
{
    protected EntityMeleeAttackStateSO stateData;
    public EntityMeleeAttackState(Entity entity, EntityStateMachine stateMachine, string animBoolName, Transform attackPosition, EntityMeleeAttackStateSO stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
       
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, entity.entityData.whatIsPlayer);

        foreach(Collider2D player in detectedObjects)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();

            if(playerController)
                playerController.TakeDamage(entity.aliveGO.transform.position.x, stateData.attackDamage, false);
        }
    }
}
