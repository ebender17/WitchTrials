using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDeadState : EntityState
{
    protected EntityDeadStateSO stateData;
    public EntityDeadState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDeadStateSO stateData) : base(entity, stateMachine, animBoolName)
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

        GameObject.Instantiate(stateData.deathParticleOne, entity.aliveGO.transform.position, stateData.deathParticleOne.transform.rotation);
        GameObject.Instantiate(stateData.deathParticleTwo, entity.aliveGO.transform.position + stateData.deathParticleTwoOffset, stateData.deathParticleOne.transform.rotation);

        GameObject.Instantiate(stateData.deathParticleThree, entity.aliveGO.transform.position + stateData.deathParticleThreeOffset, stateData.deathParticleOne.transform.rotation);

        entity.gameObject.SetActive(false);
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
}
