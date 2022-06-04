using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{


    public float speed = 12f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;




    void Update() //Called every frame
    {

        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(movementX, movementY).normalized;

        animator.SetFloat("Horizontal", movementX);
        animator.SetFloat("Vertical", movementY);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        


    }


    void FixedUpdate() //Called 50 times a sec
    {
        //rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        rb.velocity = new Vector2(movement.x * speed, movement.y * speed); 
    }
}
