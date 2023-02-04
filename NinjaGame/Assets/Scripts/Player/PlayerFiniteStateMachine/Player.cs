using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables

    [SerializeField] private PlayerData playerData;
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerAttackState ParryState { get; private set; }
    #endregion

    #region Components
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }

    public Rigidbody2D rb { get; private set; }

    public Transform DashDirectionIndicator { get; private set; }

    [NonSerialized] public PlayerStats Stats;
    #endregion

    #region Check Variables

    [SerializeField] private Transform groundCheck;
    //[SerializeField] public Transform attackHitBoxPos;

    #endregion

    #region Other Variables

    private Vector2 workSpace;
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    public bool IsAttackFinished { get; private set; }

    private bool canFlip;

    //Damage
    private bool knockback;
    private float knockbackStartTime;

    private AttackDetails attackDetails;

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");

        AttackState = new PlayerAttackState(this, StateMachine, playerData, "attack1");
        ParryState = new PlayerAttackState(this, StateMachine, playerData, "parry");
    }
    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        Stats = GetComponent<PlayerStats>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        FacingDirection = 1;

        canFlip = true;
        //IsAttackFinished = true;
        StateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        CurrentVelocity = rb.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Sets
    public void SetVelocityX(float velocity)
    {
        workSpace.Set(velocity, CurrentVelocity.y);
        rb.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workSpace = direction * velocity;
        rb.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    public void SetVelocityY(float velocity)
    {
        workSpace.Set(CurrentVelocity.x, velocity);
        rb.velocity = workSpace;
        CurrentVelocity = workSpace;
    }
    #endregion

    #region Checks

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }
    private void CheckKnockback()
    {
        if (Time.time >= knockbackStartTime + playerData.knockbackDuration && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    #endregion

    #region Damage

    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(playerData.knockbackSpeed.x * direction, playerData.knockbackSpeed.y);
    }
    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTriggerFunction() => StateMachine.CurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        if (canFlip)
        {
            FacingDirection *= -1;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void DisableFlip() => canFlip = false;
    private void EnableFlip() => canFlip = true;

    public bool GetDashStatus() => false;

    #endregion
}
