using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : GameBehaviour<InputManager>
{
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;

    public Vector2 movementInput, cameraInput;

    public float cameraInputX, cameraInputY;
    public float moveAmount;
    public float verticalInput, horizontalInput;


    public bool sprint_Input;
    public bool jump_Input;
    public bool interact_Input;

    private void Awake()
    {
        playerLocomotion = GetComponent<PlayerLocomotion>();
        animatorManager = GetComponent<AnimatorManager>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Sprint.performed += i => sprint_Input = true;
            playerControls.PlayerActions.Sprint.canceled += i => sprint_Input = false;

            playerControls.PlayerActions.Jump.performed += i => jump_Input = true;

            playerControls.PlayerActions.Interact.performed += i => interact_Input = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpingInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSprintingInput()
    {
        if (sprint_Input && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }

    private void HandleJumpingInput()
    {
        if (jump_Input)
        {
            jump_Input = false;
            playerLocomotion.HandleJumping();
        }
    }

}
