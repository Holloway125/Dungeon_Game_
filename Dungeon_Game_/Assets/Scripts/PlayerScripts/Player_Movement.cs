using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    public float baseSpeed = 12f;
    public float speed = 12f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;



    void Update() //Called every frame
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);



    }


    void FixedUpdate() //Called 50 times a sec
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

    }
}
