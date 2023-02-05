using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_DeadState : DeadState
{
    private Enemy_1 enemy;
    public Enemy1_DeadState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_DeadState stateData, Enemy_1 enemy) : base(stateMachine, entity, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.deathCrumbles.PlayOneShot(enemy.deathCrumblesClip);
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
