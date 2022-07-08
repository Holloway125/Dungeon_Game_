using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField]
    private int Health;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float checkRadius;
    [SerializeField]
    private float attackRadius;
    [SerializeField]
    private int challengeLevel;
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

        protected virtual void Update()
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

        protected virtual void FixedUpdate()
    {
         if(isInChaseRange && !isInAttackRange)
        {           
             MoveEnemy(movement);
        }
         if(isInAttackRange)
        {
            rb.velocity = Vector2.zero;
        }
    }

        private void MoveEnemy(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }

    protected virtual void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;
        if(Health <= 0)
        {
            anim.SetBool("Death", true);
        }
    }

    protected virtual void Death()
    {
        levelSystem.GainExperience(challengeLevel+levelSystem.playerLvl*100);
        Destroy(gameObject);
    }
}
