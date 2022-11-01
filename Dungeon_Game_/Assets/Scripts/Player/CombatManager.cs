using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    public bool canReceiveInput;
    public bool inputReceived;

    void Awake()
    {
        instance = this;
    }

    // public void Attack(InputAction.CallbackContext context)
    // {
    //     if(context.performed)
    //     {
    //         if(canReceiveInput)
    //         {
    //             inputReceived = true;
    //             canReceiveInput = false;
    //         }
    //         else
    //         {
    //             return;
    //         }
    //     }
    // }

    // public void Attack()
    // {
    //     if(Input.GetKeyDown(KeyCode.Tab))
    //     {
    //         if(canReceiveInput)
    //         {
    //             inputReceived = true;
    //             canReceiveInput = false;
    //         }

    //         else
    //         {
    //             return;
    //         }
    //     }
    // }

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
        Debug.Log(canReceiveInput);
    }
}
