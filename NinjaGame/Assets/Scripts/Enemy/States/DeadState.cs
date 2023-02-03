using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    private D_DeadState stateData;
    public DeadState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_DeadState stateData) : base(stateMachine, entity, animBoolName)
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

        GameObject.Instantiate(stateData.deathBloodParticle, entity.enemyGO.transform.position, stateData.deathBloodParticle.transform.rotation);
        GameObject.Instantiate(stateData.deathPiecesParticle, entity.enemyGO.transform.position, stateData.deathPiecesParticle.transform.rotation);

        entity.enemyGO.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
