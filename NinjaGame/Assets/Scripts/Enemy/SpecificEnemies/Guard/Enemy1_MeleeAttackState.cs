
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_MeleeAttackState : MeleeAttackState
{
    private Enemy_1 enemy;
    private float fireRateTimer = 0f;
    private bool canAttack = true;
    public Enemy1_MeleeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, Enemy_1 enemy) : base(stateMachine, entity, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        CheckCanAttack();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Enemy1Weapon.PlayOneShot(enemy.Enemy1WeaponClip);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }

    private void CheckCanAttack()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer > enemy.fireRate)
        {
            fireRateTimer = 0;
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }

}
