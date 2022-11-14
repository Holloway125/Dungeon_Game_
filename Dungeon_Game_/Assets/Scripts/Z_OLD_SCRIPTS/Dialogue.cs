using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Dialogue : MonoBehaviour
{
    public GameObject dialogBox;
    public TMP_Text dialogText;
    bool playerInRange = false;

    private PlayerActions _playerActions;

    private void Awake()
    {
        _playerActions = new PlayerActions();
    }
    private void Start()
    {
        _playerActions.Player_Map.Interact.performed += context => Interact();
    }

    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }

    //Can change code here to change Interaction
    private void Interact()
    {
        if(playerInRange == true)
        {
            if(dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
