using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Entity
{
    #region States
    public E2_LookForPlayerState lookForPlayerState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState { get; private set; }

    #endregion

    [SerializeField]  public D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] public D_PlayerDetectedState playerDetectedStateData;

    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        lookForPlayerState = new E2_LookForPlayerState(stateMachine, this, "idle", lookForPlayerStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData,this);

        stateMachine.Initialize(lookForPlayerState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
