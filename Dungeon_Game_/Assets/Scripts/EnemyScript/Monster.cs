using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{     
    public float speed; 
    public float checkRadius; //how far an enemy can detect the player
    public float attackRadius; 
    public int health;
    public GameObject gameObject;
    public int challengeLevel; 
    private LevelSystem levelSystem;

    public bool shouldRotate; //this is for enemies with animations that face both left and right

    public LayerMask whatIsPlayer; //Player should always be set to Player under the Layer section in the inspector
    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private Vector2 dir;
    private bool isInChaseRange;
    private bool isInAttackRange;


    private void Start()
    {
        //Defines properties on start
        levelSystem = GameObject.FindWithTag("Player").GetComponent<LevelSystem>();
        health = 5;
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
        anim.SetBool("isMoving", isInChaseRange); // set boolean in animator to true if enemy is in chase range

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer); // sets boolean to true when in chase range of player
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);// sets boolean to true when in attack range of player

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        if(shouldRotate)// if shouldRotate is checked in inspector this will run if unchecked this will not run
        {
            anim.SetFloat("X", dir.x);
            anim.SetFloat("Y", dir.y);
        }

    }

    private void FixedUpdate()
    {
         if(isInChaseRange && !isInAttackRange)//unlocks enemy movement when in range of player and is not attacking
        {
            MoveCharacter(movement);
        }
         if(isInAttackRange)//locks enemy movement to zero when attacking
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void MoveCharacter(Vector2 dir)//method that moves enemy
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }

    public void Death()
    {
        if(health <= 0)
        {
        Destroy(gameObject);
        levelSystem.GainExperience(challengeLevel+levelSystem.playerLvl*100);
        }
    }
}
