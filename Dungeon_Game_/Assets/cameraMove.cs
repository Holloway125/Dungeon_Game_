using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    float xTransform;
    float yTransform;

    void Start()
    {
        xTransform = this.transform.position.x;
        yTransform = this.transform.position.y;
    }

    public void MoveCamera(float xMove, float yMove)
    {
        xTransform = xTransform + xMove;
        yTransform = yTransform + yMove;
    }

}
