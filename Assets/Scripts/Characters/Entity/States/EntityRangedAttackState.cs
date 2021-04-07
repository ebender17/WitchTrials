using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRangedAttackState : EntityAttackState
{
    protected EntityRangedAttackStateSO stateData;

    protected GameObject projectile;
    protected Projectile projectileScript;
    public EntityRangedAttackState(Entity entity, EntityStateMachine stateMachine, string animBoolName, Transform attackPosition, EntityRangedAttackStateSO stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.gameObject.GetComponent<Projectile>();

        projectileScript.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage);
    }
}
