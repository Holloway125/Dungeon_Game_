using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraDown : MonoBehaviour
{
    GameObject _Camera;
    CameraController cameraController;
    GameObject Interactable;
    bool playerInRange = false;

    public float x;
    public float y;
    public float z;
    
    void Awake()
    {  
        _Camera = GameObject.FindGameObjectWithTag("Camera");
        cameraController = _Camera.GetComponent<CameraController>();
        Interactable = GameObject.Find("/Player/PlayerUI/Interactable");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            cameraController.MoveDown(x,y,z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            Interactable.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Interactable.SetActive(false);
        }
    }
}