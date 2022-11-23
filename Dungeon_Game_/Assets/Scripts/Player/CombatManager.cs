using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    public bool canReceiveInput = true;
    public bool inputReceived;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        canReceiveInput = true;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(canReceiveInput)
            {
                inputReceived = true;
                canReceiveInput = false;
                Debug.Log("called");
            }
            else
            {
                return;
            }
        }
    }
    public void InputManager()
    {
        if(!canReceiveInput)
        {
            canReceiveInput = true;
        }
        else
        {
            canReceiveInput = false;
        }

    }
}
