using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public void MoveUp()
    {
        transform.position += new Vector3(0f, 11.25f, 0f);
    }

    public void MoveRight()
    {  
        transform.position += new Vector3(20f, 0f, 0f);
    }

    public void MoveLeft()
    {  
        transform.position += new Vector3(-20f, 0f, 0f);
    }

    public void MoveDown()
    {
        transform.position += new Vector3(0f, -11.25f, 0f);
    }


}
