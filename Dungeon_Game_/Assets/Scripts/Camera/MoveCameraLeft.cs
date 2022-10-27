using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraLeft : MonoBehaviour
{
    GameObject _Camera;
    CameraController cameraController;

    public float x;
    public float y;
    public float z;

    void Awake()
    {
        _Camera = GameObject.FindGameObjectWithTag("Camera");
        cameraController = _Camera.GetComponent<CameraController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        cameraController.MoveLeft(x,y,z);
    }


}