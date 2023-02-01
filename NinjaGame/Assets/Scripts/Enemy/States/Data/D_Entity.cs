using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{

    public float maxHp = 50f;
    public float damageKnockbackSpeed = 2f;

    public float wallCheckDistance = 0.1f;
    public float ledgeCheckDistance = 0.2f;
    public float groundCheckRadius = 0.3f;

    public float closeRangeActionDistance = 1f;

    public GameObject hitParticle;

    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;

    public float minAgroDistance = 2f;
    public float maxAgroDistance = 4f;

    public LayerMask ground;
    public LayerMask whatIsPlayer;
}
