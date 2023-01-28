using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float lowFallForce;
    [SerializeField] private float fallForce;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] float coyoteTime = 0.25f;
    public float groundedArea = 0.1f;
    private float coyoteTimeCounter;

    
    
    [SerializeField] float jumpBufferTime = 0.25f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float doubleJumpMultiplier = 1.2f;
    private float jumpBufferCounter;
    private int jumpsLeft;
    private bool isFirstJump = true;

    private bool jumpRequest;
    private bool grounded;

    private Rigidbody2D rb;
    private Collider2D coll;
    AnimatorHandler animatorHandler;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        animatorHandler = GetComponent<AnimatorHandler>();
        jumpsLeft = maxJumps;
    }

    void Update()
    {
        var jumpInput = Input.GetButtonDown("Jump");

        if (grounded)
        {
            coyoteTimeCounter = coyoteTime;
            isFirstJump = true;
            if (rb.velocity.y <= 0) jumpsLeft = maxJumps;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (jumpInput)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        if (isFirstJump)
        {
            if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && jumpsLeft > 0)
            {
                isFirstJump = false;
                jumpRequest = true;
                coyoteTimeCounter = 0f;
            }
        }
        else if (jumpInput && jumpsLeft > 0)
        {
            jumpRequest = true;
            isFirstJump = false;
        }
    }

    private void FixedUpdate()
    {
        if (jumpRequest)
        {
            float m = isFirstJump? 1f : doubleJumpMultiplier;
            rb.AddForce(Vector2.up * jumpForce * m, ForceMode2D.Impulse);
            jumpRequest = false;
            grounded = false;
            jumpsLeft -= 1;
        }
        else
        {
            //Vector2 rayStart = (Vector2)transform.position + Vector2.down * collider.y * 0.5f;
            grounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, groundedArea, groundLayer);
        }


        if (rb.velocity.y <= 2f)
        {
            rb.gravityScale = fallForce;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowFallForce;
        }
        else rb.gravityScale = 1f;
    }

    public bool IsGrounded()
    {
        grounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, groundedArea, groundLayer);
        return grounded;
    }
}