using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private PlayerActions _playerActions;
    private Rigidbody2D _rBody;
    private Vector2 _moveInput;

    private Animator _anim;
    private bool isMoving;

    private void Awake()
    {
        _playerActions = new PlayerActions();
        _rBody = GetComponent<Rigidbody2D>();
        if (_rBody is null)
            Debug.LogError("RigidBody2D is null!");
    }

    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }

    // void Attacking()
    // {
    //     _anim.SetBool("attacking", true);
    //     _speed = 0;
    // }

    // void StopAttacking()
    // {
    //     _anim.SetBool("attacking", false);
    //     _speed = 5;       
    // }

    void FixedUpdate() 
    {
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        _rBody.velocity = _moveInput * _speed;
        _anim.SetFloat("Horizontal",_rBody.velocity.x);
        _anim.SetFloat("Vertical",_rBody.velocity.y);
        if(_rBody.velocity.x < 0.1f || _rBody.velocity.x > -0.1f)
        {
            isMoving = true;
        }
        else if (_rBody.velocity.y < 0.1f || _rBody.velocity.y > -0.1f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        _anim.SetBool("isMoving", isMoving);
    }
}
