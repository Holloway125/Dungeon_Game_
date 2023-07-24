using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script acts as a single point for all other scripts to get
// the current input from. It uses Unity's new Input System and
// functions should be mapped to their corresponding controls
// using a PlayerInput component with Unity Events.

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private Vector2 moveDirection;
    private bool jumpPressed = false;
    private bool interactPressed = false;
    [SerializeField] private bool submitPressed = false;
    private bool attackPressed = false;
    private bool rollPressed = false;

    private static InputManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;
    }

    public static InputManager GetInstance() 
    {
        return instance;
    }

    public void RollPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rollPressed = true;
        }
        else if (context.canceled)
        {
            rollPressed = false;
        }
    }
    public void JumpPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }
        else if (context.canceled)
        {
            jumpPressed = false;
        }
    }

    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactPressed = true;
            Debug.Log("Interact Pressed!");
        }
        else if (context.canceled)
        {
            interactPressed = false;
        } 
    }

    public void AttackPressed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            attackPressed = true;
        }
        else if (context.canceled)
        {
            attackPressed = false;
        }
    }

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
            Debug.Log("Submit Pressed!");
        }
        else if (context.canceled)
        {
            submitPressed = false;
        } 
    }

    // for any of the below 'Get' methods, if we're getting it then we're also using it,
    // which means we should set it to false so that it can't be used again until actually
    // pressed again.
    public bool GetJumpPressed() 
    {
        bool result = jumpPressed;
        jumpPressed = false;
        return result;
    }
    public bool GetAttackPressed() 
    {
        bool result = attackPressed;
        attackPressed = false;
        return result;
    }
    public bool GetRollPressed() 
    {
        bool result = rollPressed;
        rollPressed = false;
        return result;
    }

    public bool GetInteractPressed() 
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }

    public bool GetSubmitPressed() 
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }

    public void RegisterSubmitPressed() 
    {
        submitPressed = false;
    }

}
