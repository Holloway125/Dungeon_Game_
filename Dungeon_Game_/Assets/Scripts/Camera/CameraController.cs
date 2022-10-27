using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public void MoveUp(float x, float y, float z)
    {
        transform.position += new Vector3(x, y, z);
    }

    public void MoveRight(float x, float y, float z)
    {  
        transform.position += new Vector3(x, y, z);
    }

    public void MoveLeft(float x, float y, float z)
    {  
        transform.position += new Vector3(x, y, z);
    }

    public void MoveDown(float x, float y, float z)
    {
        transform.position += new Vector3(x, y, z);
    }

}
