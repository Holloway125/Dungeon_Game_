using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    GameObject Player;
    double x_coord;
    double y_coord;
    
    [SerializeField] private UICharacterStats UICharacterStats;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }
    private void Start()
    {
        CameraCoordinator();
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

    public void CameraCoordinator()
    {
        if (transform.position.x < 0)
        {
            x_coord = transform.position.x * -1;
            x_coord = x_coord/20;

        }
        if (transform.position.x >= 0)
        {
            x_coord = transform.position.x;
            x_coord = x_coord/20;
        }
        if(transform.position.y < 0)
        {
            y_coord = transform.position.y * -1;
            y_coord = transform.position.y/11.25;
        }
        if(transform.position.y >= 0)
        {
            y_coord = transform.position.y;
            y_coord = transform.position.y/11.25;
        }
        UICharacterStats.UpdateMapCoords(x_coord, y_coord);
    }
}
