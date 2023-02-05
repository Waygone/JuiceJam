using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_ShootState : AttackState
{
    private Enemy_2 enemy;
    private D_ShootState stateData;

    protected AttackDetails attackDetails;

    private float fireRateTimer = 0f;

    public E2_ShootState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition, D_ShootState stateData, Enemy_2 enemy) : base(stateMachine, entity, animBoolName, attackPosition)
    {
        this.enemy = enemy;
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        CheckArrowSpawn();
    }

    public override void Enter()
    {
        base.Enter();
        //attackDetails.damageAmount = stateData.attackDamage;
        //attackDetails.position = entity.enemyGO.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        /*Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("Damage", attackDetails);
        }*/
        Debug.Log("Damaging player (kinda)");
    }

    private void CheckArrowSpawn()
    {
        fireRateTimer += Time.deltaTime;
        if(fireRateTimer > enemy.fireRate)
        {
            fireRateTimer = 0;
            Shoot();
        }
    }
    private void Shoot()
    {
        enemy.ShootArrow();
        enemy.Enemy2Weapon.PlayOneShot(enemy.Enemy2WeaponClip);
    }
}
