using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls playerControls;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;


    public Vector2 movement;
    public PlayerProperties playerproperties;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start()   
    {

    }

    void Update()
    {
        Vector2 move = playerControls.Player.Move.ReadValue<Vector2>();
        Debug.Log(move);
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void Disable()
    {
        playerControls.Disable();
    }

    void Attacking()
    {
        animator.SetBool("attacking", true);
        playerproperties.Speed = 0;
    }

    void StopAttacking()
    {
        animator.SetBool("attacking", false);
        playerproperties.Speed = 5;       
    }

    // void Update()
    // {
    //     float movementX = Input.GetAxisRaw("Horizontal");
    //     float movementY = Input.GetAxisRaw("Vertical");
    //     movement = new Vector2(movementX, movementY).normalized;
    //     animator.SetFloat("Horizontal", movementX);
    //     animator.SetFloat("Vertical", movementY);
    //     animator.SetFloat("Speed", movement.sqrMagnitude); 
    // }

    // void FixedUpdate() 
    // {
    //     rb.velocity = new Vector2(movement.x * playerproperties.Speed, movement.y * playerproperties.Speed);        
    // }

    public void Movement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        rb.velocity = new Vector2(movement.x,movement.y).normalized;
    }

}
