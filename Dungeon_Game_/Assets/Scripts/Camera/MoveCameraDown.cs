using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraDown : MonoBehaviour
{
    GameObject _Camera;
    CameraController cameraController;
    
    void Awake()
    {
        _Camera = GameObject.FindGameObjectWithTag("Camera");
        cameraController = _Camera.GetComponent<CameraController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        cameraController.MoveDown();
    }


}