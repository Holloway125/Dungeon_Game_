using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCameraDown : MonoBehaviour
{
    GameObject _camera;
    GameObject _miniMapCamera;
    CameraController cameraController;
    //GameObject Interactable;    
    bool playerInRange = false;
    private PlayerActions _playerActions;

    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;
    
    private void Awake()
    {  
        _playerActions = new PlayerActions();
        _camera = GameObject.FindGameObjectWithTag("Camera");
        _miniMapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera");
        cameraController = _camera.GetComponent<CameraController>();
        //Interactable = GameObject.Find("/PlayerUI/Interactable");
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
            cameraController.MoveDown(x,y,z);
            cameraController.CameraCoordinator();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            //Interactable.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            //Interactable.SetActive(false);
        }
    }
}