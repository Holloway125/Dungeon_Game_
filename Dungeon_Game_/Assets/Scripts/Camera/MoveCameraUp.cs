using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCameraUp : MonoBehaviour
{
    private CameraController cameraController;
    private PlayerActions _playerActions;
    private GameObject _Camera;
    [SerializeField] private GameObject Interactable;    
    private bool playerInRange = false;

    public float x;
    public float y;
    public float z;
    
    private void Awake()
    {  
        _playerActions = new PlayerActions();
        _Camera = GameObject.FindGameObjectWithTag("Camera");
        cameraController = _Camera.GetComponent<CameraController>();
        //A GameObject has to be Active in Hierarchy for GameObject.Find() to work so interactable is active by default and is set to false in Start()
        Interactable = GameObject.Find("/PlayerUI/Interactable");
    }

    private void Start()
    {
        _playerActions.Player_Map.Interact.performed += context => Interact();
        Interactable.SetActive(false);
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
            cameraController.MoveUp(x,y,z);
            cameraController.CameraCoordinator();
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