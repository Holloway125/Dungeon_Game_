using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    private PlayerSkills playerSkills;

    public Vector2 movement;
    public float speed = 12f;

    private void Awake() {
        playerSkills = new PlayerSkills();
    }

    private void Start() {
    }

    void Update() {
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(movementX, movementY).normalized;

        animator.SetFloat("Horizontal", movementX);
        animator.SetFloat("Vertical", movementY);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(movement.x * speed, movement.y * speed); 
    }

    //Place all ability bool funtions here name should be CanUse[AbilityName]

    public bool CanUseBackStab(){
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.BackStab);
    }
}
