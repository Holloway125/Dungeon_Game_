using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{     
    public float speed; 
    public float checkRadius;
    public float attackRadius; 
    public int health;
    public int challengeLevel; 
    public GameObject gameObject;
    private LevelSystem levelSystem;
    public bool shouldRotate;

    //Make sure Player is on Player Layer in Inspector
    public LayerMask whatIsPlayer; 
    private Transform target;
    private Rigidbody2D rb;

    //Defines properties on start
    private Animator anim;
    private Vector2 movement;
    private Vector2 dir;
    private bool isInChaseRange;
    private bool isInAttackRange;


    private void Start()
    {
        levelSystem = GameObject.FindWithTag("Player").GetComponent<LevelSystem>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        if(challengeLevel >= 0 )
        {
            challengeLevel = 1;
        }
    }

    private void Update()
    {
        anim.SetBool("isMoving", isInChaseRange);
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        // if shouldRotate is checked in inspector this will run if unchecked this will not run
        if(shouldRotate)
        {
            anim.SetFloat("X", dir.x);
            anim.SetFloat("Y", dir.y);
        }

    }

    private void FixedUpdate()
    {
         if(isInChaseRange && !isInAttackRange)
        {           
             MoveCharacter(movement);
        }
         if(isInAttackRange)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {
            anim.SetBool("Death", true);
        }
    }

    public void Death()
    {
        
        levelSystem.GainExperience(challengeLevel+levelSystem.playerLvl*100);
        Destroy(gameObject);
    }
}
