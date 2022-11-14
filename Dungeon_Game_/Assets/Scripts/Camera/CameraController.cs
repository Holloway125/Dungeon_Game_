using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject Player;

    void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }
    
    

    public void MoveUp(float x, float y, float z)
    {
        transform.position += new Vector3(x, y, z);
        Player.transform.position += new Vector3(0, 3, 0);
    }

    public void MoveRight(float x, float y, float z)
    {  
        transform.position += new Vector3(x, y, z);
        Player.transform.position += new Vector3(3, 0, 0);
    }

    public void MoveLeft(float x, float y, float z)
    {  
        transform.position += new Vector3(x, y, z);
        Player.transform.position += new Vector3(-3, 0, 0);
    }

    public void MoveDown(float x, float y, float z)
    {
        transform.position += new Vector3(x, y, z);
        Player.transform.position += new Vector3(0, -3, 0);
    }

}
