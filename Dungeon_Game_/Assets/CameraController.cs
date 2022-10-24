using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float xTransform;
    float yTransform;

    public void MoveCamera(float xMove, float yMove)
    {
        xTransform = transform.position.x + xMove;
        yTransform = transform.position.y + yMove;
    }

}
