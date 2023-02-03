using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set;}
    public Vector2 RawDashDirectionInput { get; private set;}
    public Vector2Int DashDirectionInput { get; private set;}

    private PlayerInput playerInput;
    [SerializeField] private Camera mainCamera;

    public int NormInputX { get; private set;}
    public int NormInputY { get; private set;}
    public bool JumpInput { get; private set;}
    public bool JumpInputStop { get; private set;}

    public bool DashInput { get; private set;}
    public bool DashInputStop { get; private set;}

    public bool attacking, parrying;

    [SerializeField] private float inputHoldTime = .2f;

    private float jumpInputStartTime, dashInputStartTime;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        int countAttacks = Enum.GetValues(typeof(CombatInputs)).Length;
        attacking = false;
        parrying = false;
    }
    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attacking = true;
        }
        if (context.canceled)
        {
            attacking = false;
        }
    }
    public void OnParryInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            parrying = true;
        }
        if (context.canceled)
        {
            parrying = false;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }
    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();

        if (playerInput.currentControlScheme == "Keyboard")
        {
            if (mainCamera != null)
            {
                RawDashDirectionInput = mainCamera.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;
            }
        }
        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);

    }
    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    private void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }

    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;
}

public enum CombatInputs
{
    primary,
    secondary
}
