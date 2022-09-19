using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;


    public Vector2 movement;
    public float speed = 5f;

    void Update()
    {
            if(!animator.GetBool("attacking"))
        {       
            float movementX = Input.GetAxisRaw("Horizontal");
            float movementY = Input.GetAxisRaw("Vertical");
            movement = new Vector2(movementX, movementY).normalized;
            animator.SetFloat("Horizontal", movementX);
            animator.SetFloat("Vertical", movementY);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    void FixedUpdate() 
    {
        if(!animator.GetBool("attacking"))
        {
            rb.velocity = new Vector2(movement.x * speed, movement.y * speed); 
        }
    }

}
