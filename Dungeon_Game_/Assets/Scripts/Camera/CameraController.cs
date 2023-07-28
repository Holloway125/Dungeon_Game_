using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    GameObject Player;
    double x_coord;
    double y_coord;
    [SerializeField] private Camera _miniMapCamera;
    [SerializeField] private Vector3 _newlocation;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }
    private void Start()
    {
        CameraCoordinator();
        _newlocation = _miniMapCamera.transform.localPosition;
    }
    private void FixedUpdate()
    {
        if(_newlocation == _miniMapCamera.transform.localPosition)
        {
            return;
        }
        else if(_newlocation != _miniMapCamera.transform.localPosition)
        {
        _miniMapCamera.transform.Translate(Vector3.up * Time.deltaTime);
            if (_miniMapCamera.transform.localPosition.y > _newlocation.y + .001f)
            {
            _miniMapCamera.transform.localPosition = _newlocation;
            Debug.Log("Please do this!");
            }
        }
    }

    public void MoveUp(float x, float y, float z)
    {
        transform.position += new Vector3(x, y, z);
        _newlocation += new Vector3(0,1,0);
        Player.transform.position += new Vector3(0, 3, 0);
    }

    public void MoveDown(float x, float y, float z)
    {
        transform.position += new Vector3(x, y, z);
        _miniMapCamera.transform.localPosition += new Vector3(0,-1,0);
        Player.transform.position += new Vector3(0, -3, 0);
    }
    
    public void MoveRight(float x, float y, float z)
    {  
        transform.position += new Vector3(x, y, z);
        _miniMapCamera.transform.localPosition += new Vector3(1,0,0);
        Player.transform.position += new Vector3(3, 0, 0);
    }

    public void MoveLeft(float x, float y, float z)
    {  
        transform.position += new Vector3(x, y, z);
        //_miniMapCamera.transform.position += new Vector3(-1,0,0);
        _newlocation = _miniMapCamera.transform.localPosition + new Vector3(-1,0,0);
        Player.transform.position += new Vector3(-3, 0, 0);
        Debug.Log(_newlocation);
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
        UIManager.Instance.UpdateMapCoords(x_coord, y_coord);
    }
}
