using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //References
    private Rigidbody2D rb;
    private Animator anim;
    public Transform groundCheck;
    //public Transform wallCheck;
    //public Vector2 wallJumpDirection;


    //player variables
    public float movementSpeed = 10f;
    public float jumpForce = 16f;
    public float groundCheckRadius;
    public LayerMask WhatIsGround;
    public int amountOfJumps = 1;
    //public float wallCheckDistance;
    //public float wallSlidingSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMultiplier = 0.5f;
    //public float wallJumpForce;
    public float jumpBufferSet = 0.15f;
    //public float turnTimerSet = 0.1f;
    //public float wallJumpTimerSet = 0.5f;

    //input
    private float movementInputDirection;
    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool canNormalJump;
    //public bool canWallJump;
    private int amountOfJumpsLeft;
    //private bool isTouchingWall;
    //private bool isWallSliding;
    private bool isAttemptingToJump;
    private float jumpBuffer;
    private int facingDirection = 1;
    private bool checkJumpMultiplier;
    private bool canMove;
    private bool canFlip;
    private float turnTimer;
    //private float wallJumpTimer;
    //private bool hasWallJumped;
    private int lastWallJumpDirection;

    //Damage
    private bool knockback;
    private float knockbackStartTime;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private Vector2 knockbackSpeed;
    

    private bool isMoving;
    [SerializeField] private float standingTime;

    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        //wallJumpDirection.Normalize();

        isMoving = true;
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CanJump();
        //CheckIfWallSliding();
        CheckJump();
        CheckKnockback();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    #region input
    //input
    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || amountOfJumpsLeft > 0) {
                NormalJump();
            } else
            {
                jumpBuffer = jumpBufferSet;
                isAttemptingToJump = true;
            }
        }
        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            if (!isGrounded && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                //turnTimer = turnTimerSet;
            }
        }

        if (!canMove)
        {
            turnTimer -= Time.deltaTime;
            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }
    }
    #endregion

    #region movement
    //movement
    private void ApplyMovement()
    {
        if (!isGrounded && movementInputDirection == 0 && !knockback)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if (canMove && !knockback)
        {
            rb.velocity = new Vector2(movementInputDirection * movementSpeed, rb.velocity.y);
        }
    }

    private void CheckMovementDirection()
    {
        if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        } else if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isWalking = true;
        } else
        {
            isWalking = false;
        }
    }

    private void DisableFlip(){
        canFlip = false;
    }
    private void EnableFlip()
    {
        canFlip = true;
    }
    private void Flip()
    {
        if (canFlip && !knockback)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    #endregion

    #region Jump
    //jumping

    private void NormalJump()
    {
        if (canNormalJump)
        {
            //can only jump using basic jump force when conditions are met and not wall sliding
            amountOfJumpsLeft--;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBuffer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
        }
    }

    /*private void WallJump()
    {
        //wall jumping can be done when the player touches the wall or on the wall
        if (canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            //resets velocity on y axis before wall jump so wall jump always has same increase no matter previous velocity
            isWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;
            //adding a force with the x equal to the force off the wall towards the movement, with the y axis having the same force upwards in the upwards direction
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpBuffer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;
        }
    }*/

    private void CanJump()
    {
        //can only jump when grounded and not in the air or wall sliding
        if (isGrounded && rb.velocity.y <= 0.01f)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        /*if (isTouchingWall)
        {
            canWallJump = true;
        }*/

        if (amountOfJumpsLeft == 0) {
            canNormalJump = false;
        } else
        {
            canNormalJump = true;
        }
    }

    private void CheckJump()
    {
        if (jumpBuffer > 0)
        {
            //walljump
            /*if (!isGrounded && isTouchingWall && movementInputDirection != 0 && movementInputDirection != facingDirection)
            {
                WallJump();
            } else */
            if (isGrounded)
            {
                NormalJump();
            }
        }
        //when space bar hit we lower timer to 0 allowing threshold for player to jump
        /*if(isAttemptingToJump)
        {
            jumpBuffer -= Time.deltaTime;
        }

        if (wallJumpTimer > 0)
        {
            if (hasWallJumped && movementInputDirection == -lastWallJumpDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, -0.5f);
                hasWallJumped= false;
            } else if (wallJumpTimer <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }*/
    }

    #endregion

    #region WallSliding
    /*private void CheckIfWallSliding()
    {
        //if player is touching a wall and purposefully looking at wall then wallsliding is true
        if (isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y < 0)
        {
            isWallSliding = true;
        } else
        {
            isWallSliding = false;
        }
    }*/

    #endregion

    #region animations

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        //anim.SetBool("isWallSliding", isWallSliding);
    }

    #endregion

    #region CheckSurroundings

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);

        //isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, WhatIsGround);
    }

    #endregion

    #region Knockback
    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }
    private void CheckKnockback()
    {
        if(Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }
    public bool GetDashStatus()
    {
        //return isDashing;
        return false;
    }
    #endregion

    #region gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        //Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
    #endregion
}