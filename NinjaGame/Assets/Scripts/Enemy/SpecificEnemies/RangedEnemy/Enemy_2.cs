using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Entity
{
    #region States
    public E2_MoveState moveState { get; private set; }
    public E2_IdleState idleState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState { get; private set; }
    public E2_MeleeAttackState meleeAttackState { get; private set; }

    #endregion

    [SerializeField]  public D_IdleState idleStateData;
    [SerializeField] public D_PlayerDetectedState playerDetectedStateData;

    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        idleState = new E2_IdleState(stateMachine, this, "idle", idleStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData,this);

        stateMachine.Initialize(idleState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
