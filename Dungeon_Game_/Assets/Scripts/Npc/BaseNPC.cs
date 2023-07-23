using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BaseNPC : MonoBehaviour
{
    private PlayerActions _playerActions;
    
    public bool playerInRange = false;

    public GameObject dialogBox;
    // public TMP_Text dialogText;


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

    //Change Interaction by overriding
    protected virtual void Interact()
    {
        if(playerInRange == true)
        {
            UIManager.Instance.NpcDialogue();
        }
        else
        {
            return;
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
