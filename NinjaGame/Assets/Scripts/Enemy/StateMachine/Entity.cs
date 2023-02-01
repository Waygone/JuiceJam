using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;
    public int facingDirection { get; private set; }
    public int lastDamageDirection { get; private set; }

    public Rigidbody2D rb { get; private set; }

    public Animator anim { get; private set; }
    public GameObject enemyGO { get; private set; }
    public AnimationToStateMachine animToState { get; private set; }



    private Vector2 velocityWorkspace;
    [SerializeField]
    private Transform
        wallCheck,
        ledgeCheck,
        playerCheck,
        groundCheck;
    [SerializeField] protected Transform textPosition;

    protected float currentHp;
    private float currentStunResistance;
    private float lastDamageTime;

    protected bool 
        isStunned, 
        isDead;


    public virtual void Start()
    {
        facingDirection = 1;

        currentHp = entityData.maxHp;
        currentStunResistance = entityData.stunResistance;

        enemyGO = transform.Find("Enemy").gameObject;
        rb = enemyGO.GetComponent<Rigidbody2D>();
        anim = enemyGO.GetComponent<Animator>();
        animToState = enemyGO.GetComponent<AnimationToStateMachine>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        if(Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    #region Motion
    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = velocityWorkspace;
    }
    #endregion

    #region Check
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, enemyGO.transform.right, entityData.wallCheckDistance, entityData.ground);
    }
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.ground);
    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.ground);
    }

    public virtual bool CheckPlayerMinRange()
    {
        return Physics2D.Raycast(playerCheck.position, enemyGO.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerMaxRange()
    {
        return Physics2D.Raycast(playerCheck.position, enemyGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, enemyGO.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }
    #endregion

    #region Damage Logic
    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }
    public virtual void Damage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;
        currentHp -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunAmount;

        DamageKnockback(entityData.damageKnockbackSpeed);

        Instantiate(entityData.hitParticle, enemyGO.transform.position, Quaternion.Euler(0f,0f,Random.Range(0f, 360f)));

        if(attackDetails.position.x > enemyGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if(currentStunResistance <= 0)
        {
            isStunned = true;
        }
        if(currentHp <= 0f)
        {
            isDead = true;
        }
    }

    public virtual void DamageKnockback(float velocity)
    {
        velocityWorkspace.Set(rb.velocity.x, velocity);
        rb.velocity = velocityWorkspace;
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        enemyGO.transform.Rotate(0f, 180f, 0f);
    }
    #endregion 

    #region Gizmos
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        Gizmos.DrawWireSphere(groundCheck.position, entityData.groundCheckRadius);

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
    }
    #endregion
}
