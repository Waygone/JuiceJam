using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Walking,
        Damaged,
        Dead
    }

    private State currentState;

    [SerializeField]
    private Transform
        groundCheck,
        wallCheck,
        touchDamageCheck;

    [SerializeField]
    private Vector2 knockbackSpeed;
    [SerializeField]
    private LayerMask 
        ground,
        whatIsPlayer;

    [SerializeField]
    private float
        movementSpeed,
        maxHp,
        knockbackDuration,
        groundCheckDistance, 
        wallCheckDistance,
        touchDamageCooldown,
        touchDamage,
        touchDamageWidth,
        touchDamageHeight;

    private float lastTouchDamageTime;

    [SerializeField]
    private GameObject
        hitParticle,
        deathChunkParticle,
        deathBloodParticle;

    private bool
        groundDetected,
        wallDetected;

    private int 
        facingDirection,
        damageDirection;

    private float 
        knockbackStartTime,
        currentHp;

    private Vector2 
        movement,
        touchDamageBotLeft,
        touchDamageTopRight;

    private float[] attackDetails = new float[2];

    private GameObject enemy;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        enemy = transform.Find("Enemy").gameObject;
        rb = enemy.GetComponent<Rigidbody2D>();
        animator = enemy.GetComponent<Animator>();

        currentHp = maxHp;
        facingDirection = 1;
    }
    private void Update()
    {
        switch(currentState)
        {
            case State.Walking:
                UpdateMovingState(); break;
            case State.Damaged:
                UpdateMovingState(); break;
            case State.Dead:
                UpdateMovingState(); break;
        }    
    }

    /// 
    /// WALKING ////////////////////////////////////////////////////
    /// 
    private void EnterMovingState()
    {

    }
    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, ground);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, ground);

        ChechTouchDamage();

        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, rb.velocity.y);
            rb.velocity = movement;
        }
    }
    private void ExitMovingState()
    {

    }

    /// 
    /// DAMAGED ////////////////////////////////////////////////////
    /// 
    private void EnterDamagedState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        rb.velocity = movement;
        animator.SetBool("Damaged", true);
    }
    private void UpdateDamagedState()
    {
        if(Time.time >= knockbackStartTime * knockbackDuration)
        {
            SwitchState(State.Walking);
        }
    }
    private void ExitDamagedState()
    {
        animator.SetBool("Damaged", false);
    }
    /// 
    /// DEAD ////////////////////////////////////////////////////
    /// 
    private void EnterDeadState()
    {
        //Instantiate(deathChunkParticle, enemy.transform.position, deathChunkParticle.transform.rotation);
        //Instantiate(deathBloodParticle, enemy.transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }
    private void UpdateDeadState()
    {

    }
    private void ExitDeadState()
    {

    }

    /// 
    /// FUNCTIONS ////////////////////////////////////////////////////
    /// 
    public void Damage(float[] attackDetails)
    {
        currentHp -= attackDetails[0];

        Instantiate(hitParticle, enemy.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (attackDetails[1] > enemy.transform.position.x)
        {
            damageDirection = -1;
        }else damageDirection = 1;

        if(currentHp > 0.0f)
        {
            SwitchState(State.Damaged);
        }else if(currentHp <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }
    private void ChechTouchDamage()
    {
        if(Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if(hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = enemy.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }
    private void Flip()
    {
        facingDirection *= -1;
        enemy.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walking:
                ExitMovingState(); break;
            case State.Damaged:
                ExitDamagedState(); break;
            case State.Dead:
                ExitDeadState(); break;
        }

        switch (state)
        {
            case State.Walking:
                EnterMovingState(); break;
            case State.Damaged:
                EnterDamagedState(); break;
            case State.Dead:
                EnterDeadState(); break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 TopLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Vector2 TopRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, TopRight);
        Gizmos.DrawLine(TopRight, TopLeft);
        Gizmos.DrawLine(TopLeft, botLeft);
    }
}
