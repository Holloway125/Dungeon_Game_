using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;


    public Vector2 movement;
    public PlayerProperties playerproperties;


    void Update()
    {
        
        if (animator.GetBool("attacking") == true)
        {
        playerproperties.Speed = 0;
        }

        else if(animator.GetBool("attacking") == false)
        {       
            playerproperties.Speed = 5;
            float movementX = Input.GetAxisRaw("Horizontal");
            float movementY = Input.GetAxisRaw("Vertical");
            movement = new Vector2(movementX, movementY).normalized;
        }
    }

    void FixedUpdate() 
    {
            rb.velocity = new Vector2(movement.x * playerproperties.Speed, movement.y * playerproperties.Speed);        
    }

}
