using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 6f;

    [Header("Jump State")]
    public float jumpForce = 10f;
    public int amountOfJumps = 1;

    [Header("Attack State")]
    public float aproxDamage = 10f;
    public float stunDamage = 1f;
    public float attackRadius = .3f;
    public LayerMask damageableLayer;
    public float knockbackDuration = .2f;
    public Vector2 knockbackSpeed = new Vector2(5,8);

    [Header("Dash State")]
    public float dashCooldown = 1f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.25f;
    public float dashTime = .2f;
    public float dashForce = 25f;
    public float drag = 10f;
    public float dashEndYMultiplier = 0.2f;
    public float distanceBetweenTrail = 0.5f;

    [Header("Air State")]
    public float coyoteTime = 0.2f;
    public float jumpHeightMultiplier = 0.4f;

    [Header("Check Variables")]
    public float groundCheckRadius = .3f;
    public LayerMask whatIsGround;
}
