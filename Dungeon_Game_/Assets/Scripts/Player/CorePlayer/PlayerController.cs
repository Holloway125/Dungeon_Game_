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

    private PlayerInput _playerInput;
    private InputAction _movement;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _movement = _playerInput.actions["Movement"];
        // _playerActions = new PlayerActions();
        // _rBody = GetComponent<Rigidbody2D>();
        // _anim = GetComponent<Animator>();
        // if (_rBody is null)
        //     Debug.LogError("Rigidbody is NULL!");
        
    }

    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }

    void Attacking()
    {
        _anim.SetBool("attacking", true);
        _speed = 0;
    }

    void StopAttacking()
    {
        _anim.SetBool("attacking", false);
        _speed = 5;       
    }

    void FixedUpdate() 
    {
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        _rBody.velocity = _moveInput * _speed;
    }
}
