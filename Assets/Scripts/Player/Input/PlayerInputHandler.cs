using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles events from Player Input component. 
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Camera _cam;

    public Vector2 rawMovementInput { get; private set; }
    public Vector2 rawDashDirectionInput { get; private set; }
    public Vector2Int dashDirectionInput { get; private set; }
    public int normInputX { get; private set; }
    public int normInputY { get; private set; }
    public bool jumpInput { get; private set; }
    public bool jumpInputStop { get; private set; }
    public bool dashInput { get; private set; }
    public bool dashInputStop { get; private set; }
    public bool primAtkInput { get; private set; }
    public bool primAtkInputStop { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float dashInputStartTime;
    private float primAtkInputStartTime;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _cam = Camera.main;
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();

        // Normalize input so player moves with same speed on different input types
        // TODO: Check with controller. Also possible to do with input system.
        if (Mathf.Abs(rawMovementInput.x) > 0.5f)
        {
            normInputX = (int)(rawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            normInputX = 0;
        }

        if (Mathf.Abs(rawMovementInput.y) > 0.5f)
        {
            normInputY = (int)(rawMovementInput * Vector2.up).normalized.y;
        }
        else
        {
            normInputY = 0;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpInput = true;
            jumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            jumpInputStop = true;
        }
    }
    public void UseJumpInput() => jumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            jumpInput = false;
        }
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            dashInput = true;
            dashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            dashInputStop = true;
        }
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        rawDashDirectionInput = context.ReadValue<Vector2>();

        if (_playerInput.currentControlScheme == "Keyboard")
        {
            // subtract player's transform to get vector pointing from player to world point
            rawDashDirectionInput = _cam.ScreenToWorldPoint(rawDashDirectionInput) - transform.position;
        }

        // normalize as magnitude does not matter, just direction
        dashDirectionInput = Vector2Int.RoundToInt(rawDashDirectionInput.normalized);
    }

    public void UseDashInput() => dashInput = false;

    private void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            dashInput = false;
        }
    }

    public void OnPrimAtkInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            primAtkInput = true;
            primAtkInputStop = false;
            primAtkInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            primAtkInputStop = true;
        }
        
    }

    public void UsePrimAtkInput() => primAtkInput = false;

    private void CheckPrimAtkInputHoldTime()
    {
        if(Time.time >= primAtkInputStartTime + inputHoldTime)
        {
            primAtkInput = false;
        }
    }

    public void EnableGameplayInput()
    {
        //enable gameplay input
        //playerInput.ActivateInput();
    }
}
