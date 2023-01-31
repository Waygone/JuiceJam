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

    [SerializeField] private Transform meleeAttackPosition;

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;    

    public override void Start()
    {
        base.Start();

        moveState = new Enemy1_MoveState(stateMachine,  this, "walk", moveStateData, this);
        idleState = new Enemy1_IdleState(stateMachine, this, "idle", idleStateData, this);
        playerDetectedState = new Enemy1_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData, this);
        chargeState = new Enemy1_ChargeState(stateMachine, this, "charge", chargeStateData, this);
        lookForPlayerState = new Enemy1_LookForPlayerState(stateMachine, this, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new Enemy1_MeleeAttackState(stateMachine, this, "meleeAttack",meleeAttackPosition ,meleeAttackStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
