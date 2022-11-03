using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    private PlayerActions _playerActions;
    private Rigidbody2D _rBody;
    private Vector2 _moveInput;

    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject player;
    private Animator _anim;
    private bool isMoving;

    private Vector3 mousePosition;
    private Vector3 mouseWorldPosition;
    private float angle;
    private float angleDegree;

    private void Awake()
    {
        _playerActions = new PlayerActions();
        _rBody = GetComponent<Rigidbody2D>();
        if (_rBody is null)
            Debug.LogError("RigidBody2D is null!");
        _anim = GetComponent<Animator>();
        _playerActions.Player_Map.Attack.performed += context => Attack();
    }

    private void FixedUpdate() 
    {
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        _rBody.velocity = _moveInput * _speed;
        _anim.SetFloat("Horizontal",_rBody.velocity.x);
        _anim.SetFloat("Vertical",_rBody.velocity.y);

        if(_rBody.velocity.x != 0f)
        {
            isMoving = true;
        }
        else if (_rBody.velocity.y != 0f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        _anim.SetBool("isMoving", isMoving);
    }

    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }

    public void Attack()
    {
        _anim.SetBool("isAttacking", true);
        _speed = 0;
        MouseRotation();
        // Debug.Log(_anim.GetFloat("Direction"));
    }

    // private void StopAttacking()
    // {
    //     _anim.SetBool("isAttacking", false);
    //     _speed = 5;
    //     Debug.Log("Done Attacking!");
    // }

    private void MouseRotation()
    {
        mousePosition = _playerActions.Player_Map.MousePosition.ReadValue<Vector2>();
        mouseWorldPosition = _camera.ScreenToWorldPoint(mousePosition);
        Vector3 diffPos = mouseWorldPosition - player.transform.position;
        angle = Mathf.Atan2(diffPos.y, diffPos.x);
        angleDegree = angle * Mathf.Rad2Deg;
        //converts the range of (-180, 180) to a range of (0, 360) which then can be passed into the rotation z value of an object to set it rotation pointing to the cursors current position
        if(angleDegree < 0)
        {
            angleDegree+=360;           
        }   
        if (angleDegree >= 0  && angleDegree <= 22.5 || angleDegree <= 360 && angleDegree >=337.5)
        {
             _anim.Play("East");
        }
        else if (angleDegree > 22.5 && angleDegree <= 67.5)
        {
             _anim.Play("NorthEast");
        }
        else if (angleDegree > 67.5 && angleDegree <= 112.5)
        {
             _anim.Play("North");
        }
        else if (angleDegree > 112.5 && angleDegree <= 157.5)
        {
             _anim.Play("NorthWest");
        }
        else if (angleDegree > 157.5 && angleDegree <= 202.5)
        {
             _anim.Play("West");
        }     
        else if (angleDegree > 202.5 && angleDegree <= 247.5)
        {
             _anim.Play("SouthWest");
        }      
        else if (angleDegree > 247.5 && angleDegree <= 292.5)
        {
             _anim.Play("South");
        }    
        else if (angleDegree > 292.5 && angleDegree <= 337.5)
        {
             _anim.Play("SouthEast");
        }
    }

}
