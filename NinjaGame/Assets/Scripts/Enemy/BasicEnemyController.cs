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
        wallCheck;

    [SerializeField]
    private Vector2 knockbackSpeed;
    [SerializeField]
    private LayerMask ground;

    [SerializeField]
    private float
        movementSpeed,
        maxHp,
        knockbackDuration,
        groundCheckDistance, 
        wallCheckDistance;

    private bool
        groundDetected,
        wallDetected;

    private int 
        facingDirection,
        damageDirection;

    private float 
        knockbackStartTime,
        currentHp;

    private Vector2 movement;
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
        //Dead enemy feedback
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
    }
}
