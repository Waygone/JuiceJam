using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class E2_PlayerDetectedState : PlayerDetectedState
{
    private Enemy_2 enemy;
    public E2_PlayerDetectedState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_PlayerDetectedState stateData, Enemy_2 enemy) : base(stateMachine, entity, animBoolName, stateData)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            Debug.Log("");
            //stateMachine.ChangeState(enemy.meleeAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
