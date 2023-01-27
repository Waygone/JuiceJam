using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    //[SerializeField] private float jumpTime;
    [SerializeField] private float jumpForce;
    [SerializeField] private float lowFallForce;
    [SerializeField] private float fallForce;


    //[SerializeField] private Transform groundCheck;
    //[SerializeField] private float groundCheckRadius;


    public float groundedArea = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] float coyoteTime = 0.25f;
    private float coyoteTimeCounter;

    [SerializeField] float jumpBufferTime = 0.25f;
    private float jumpBufferCounter;

    Vector2 playerSize;

    private void Awake()
    {
        playerSize = GetComponent<BoxCollider2D>().size;
    }

    private bool jumpRequest;
    private bool isGrounded;

    private Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var jumpInput = Input.GetButtonDown("Jump");

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
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
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            jumpRequest = true;
            coyoteTimeCounter = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (jumpRequest)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequest = false;
            isGrounded = false;
        }
        else
        {
            Vector2 rayStart = (Vector2)transform.position + Vector2.down * playerSize.y * 0.5f;
            isGrounded = Physics2D.Raycast(rayStart, Vector2.down, groundedArea, groundLayer);
        }


        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallForce;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowFallForce;
        }
        else rb.gravityScale = 1f;
    }
}