using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public void MoveUp()
    {
        transform.position += new Vector3(0, 10, 0);
    }

    public void MoveRight()
    {  
        transform.position += new Vector3(20, 0, 0);
    }

    public void MoveLeft()
    {  
        transform.position += new Vector3(-20, 0, 0);
    }

    public void MoveDown()
    {
        transform.position += new Vector3(0, -10, 0);
    }


}
