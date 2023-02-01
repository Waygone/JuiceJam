using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_1 : Entity
{
    public Enemy1_IdleState idleState { get; private set; }
    public Enemy1_MoveState moveState { get; private set; }
    public Enemy1_PlayerDetectedState playerDetectedState { get; private set; }

    public Enemy1_ChargeState chargeState { get; private set; }

    public Enemy1_LookForPlayerState lookForPlayerState { get; private set; }

    public Enemy1_MeleeAttackState meleeAttackState { get; private set; }

    public Enemy1_StunState stunState { get; private set; }

    public Enemy1_DeadState deadState { get; private set; }

    [SerializeField] private Transform meleeAttackPosition;

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;

    public override void Start()
    {
        base.Start();

        moveState = new Enemy1_MoveState(stateMachine,  this, "walk", moveStateData, this);
        idleState = new Enemy1_IdleState(stateMachine, this, "idle", idleStateData, this);
        playerDetectedState = new Enemy1_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData, this);
        chargeState = new Enemy1_ChargeState(stateMachine, this, "charge", chargeStateData, this);
        lookForPlayerState = new Enemy1_LookForPlayerState(stateMachine, this, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new Enemy1_MeleeAttackState(stateMachine, this, "meleeAttack",meleeAttackPosition ,meleeAttackStateData, this);
        stunState = new Enemy1_StunState(stateMachine, this, "stun", stunStateData, this);
        deadState = new Enemy1_DeadState(stateMachine, this, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
    }
}
