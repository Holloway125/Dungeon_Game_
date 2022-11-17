using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCameraRight : MonoBehaviour
{
    GameObject _Camera;
    CameraController cameraController;
    GameObject Interactable;    
    bool playerInRange = false;
    private PlayerActions _playerActions;

    public float x;
    public float y;
    public float z;
    
    private void Awake()
    {  
        _playerActions = new PlayerActions();
        _Camera = GameObject.FindGameObjectWithTag("Camera");
        cameraController = _Camera.GetComponent<CameraController>();
        Interactable = GameObject.Find("/PlayerUI/Interactable");
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

    private void Interact()
    {
        if(playerInRange == true)
        {
            cameraController.MoveRight(x,y,z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            Interactable.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Interactable.SetActive(false);
        }
    }
}