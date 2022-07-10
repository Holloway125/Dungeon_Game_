using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{

// Make sure Player is on Player Layer in Inspector.
    [SerializeField]
    protected LayerMask WhatIsPlayer; 

    [Space]    

// Basic Enemy Stats Set in Inspector.
    [SerializeField]
    protected float EnemyHealth;   
    [SerializeField]
    protected float Speed;
    [SerializeField]
    protected float CheckRadius;
    [SerializeField]
    protected int ChallengeLevel;
// Defined on Awake/Start/Update/FixedUpdate Functions.
    protected Animator Anim;
    protected Vector2 Movement;
    protected GameObject Player;
    protected Damage DamageScript;
    protected Vector2 Dir;
    protected LevelSystem LevelSystem;
    protected Transform Target;
    protected Rigidbody2D Rb;

    [Space]
 
// Set in Inspector used for attacking Player.


    [SerializeField]
    protected float AttackRadius;

    [Space]

    // If Enemy has weapon or ability thats played through an Animator then mark true in the Inspector.
    [SerializeField]
    protected bool HasAnAttack;
    [SerializeField]
    protected int AttackDamaage;
    [SerializeField]
    protected float AttackCooldown;

    [Space]

    // If Enemy has a on collision damage then mark true in the inspector.
    [SerializeField]
    protected bool HasContactAttack;
    [SerializeField]
    protected int ContactDamage;
    [SerializeField]
    protected float ContactAttackCooldown;

// Used for attacking.
    protected bool IsInChaseRange;
    protected bool IsInAttackRange;
    protected bool IsCollided;





        protected virtual void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        LevelSystem = Player.GetComponent<LevelSystem>();
        DamageScript = Player.GetComponent<Damage>();
        Target = Player.transform;
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

        if(ChallengeLevel >= 0 )
        {
            ChallengeLevel = 1;
        }

    }

        protected virtual void Start()
    {

    }

        protected virtual void Update()
    {
        Anim.SetBool("isMoving", IsInChaseRange);
        IsInChaseRange = Physics2D.OverlapCircle(transform.position, CheckRadius, WhatIsPlayer);
        IsInAttackRange = Physics2D.OverlapCircle(transform.position, AttackRadius, WhatIsPlayer);

        Dir = Target.position - transform.position;
        float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        Dir.Normalize();
        Movement = Dir;

    }

        protected virtual void FixedUpdate()
    {
         if(IsInChaseRange && !IsInAttackRange)
        {           
             MoveEnemy(Movement);
             if (HasAnAttack)
             {
                StopCoroutine("Attack");
             }
        }
         if(IsInAttackRange)
        {
            Rb.velocity = Vector2.zero;
            if (HasAnAttack)
            {
                StartCoroutine("Attack");
            }
        }
    }
        protected void OnCollisionEnter2D(Collision2D collision)
     {
        if (collision.gameObject.tag == "Player" && HasContactAttack)
        {
            IsCollided = true;
            StartCoroutine("ContactAttack");
        }
     }

        protected void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player" && HasContactAttack)
            {
                IsCollided = false;
                StopCoroutine("ContactAttack");
            }
        }
// Coroutine that does damage over time when started.
        protected IEnumerator ContactAttack()
        {
            while(IsCollided)
            {
                 DamageScript.TakeDamage(ContactDamage); 
                yield return new WaitForSeconds(ContactAttackCooldown);
            }
            yield return null;
            
        }

// Coroutine used to play attack animation of Enemies that have an attack.
       protected virtual IEnumerator Attack()
     {
        
        if (HasAnAttack)
        {
            Anim.Play("Attack");
            yield return new WaitForSeconds(AttackCooldown);
        }

     }

        protected void MoveEnemy(Vector2 Dir)
    {
        Rb.MovePosition((Vector2)transform.position + (Dir * Speed * Time.deltaTime));
    }
// Called in Weapon Anim script to cause damage to Enemy needs.
// to be public can be overridden to change how much damage Enemy will take such as damaageAmount /= 2;.
        public virtual void TakeDamage(int damageAmount)
    {
        EnemyHealth -= damageAmount;
        if(EnemyHealth <= 0)
        {
            Anim.Play("Death");
        }
    }

        protected virtual void Death()
    {
        LevelSystem.GainExperience(ChallengeLevel+LevelSystem.playerLvl*100);
        Destroy(gameObject);
    }
}
