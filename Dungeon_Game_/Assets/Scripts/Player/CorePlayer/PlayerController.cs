using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;


    public Vector2 movement;
    public PlayerProperties playerproperties;

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

    void Update()
    {
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(movementX, movementY).normalized;
        animator.SetFloat("Horizontal", movementX);
        animator.SetFloat("Vertical", movementY);
        animator.SetFloat("Speed", movement.sqrMagnitude); 
    }

    void FixedUpdate() 
    {
        rb.velocity = new Vector2(movement.x * playerproperties.Speed, movement.y * playerproperties.Speed);        
    }

}
