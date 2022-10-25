using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraUp : MonoBehaviour
{
    public GameObject _Camera;
    CameraController cameraController;
    void Awake()
    {
        cameraController = _Camera.GetComponent<CameraController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        cameraController.MoveUp();
    }


}