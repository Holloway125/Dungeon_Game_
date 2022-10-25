using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraLeft : MonoBehaviour
{
    public GameObject _Camera;
    CameraController cameraController;
    void Awake()
    {
        cameraController = _Camera.GetComponent<CameraController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        cameraController.MoveLeft();
    }


}