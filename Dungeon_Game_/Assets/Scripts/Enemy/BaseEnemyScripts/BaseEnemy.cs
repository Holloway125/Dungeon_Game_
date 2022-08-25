using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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
    public float Speed;
    [SerializeField]
    protected int ChallengeLevel;
// Defined on Awake/Start/Update/FixedUpdate Functions.
    protected Animator Anim;
    protected GameObject Player;
    protected Damage DamageScript;
    protected Vector2 Dir;
    protected LevelSystem LevelSystem;
    // protected Transform Target;
    protected Rigidbody2D Rb;
    private AIPath movement;
    private AIDestinationSetter AIDestinationSetterScript;
    

    [Space]
 
// Set in Inspector used for attacking Player.


    [SerializeField]
    protected float AttackRadius;
    protected float SuspiciousRadius;
    protected float ChaseRadius;

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
    protected bool IsCollided;
    protected Collider2D PlayerCollider;
    [SerializeField]
    protected LayerMask IgnoreTheseLayers;
    protected bool IsInAttackRange;
    protected bool IsInSuspiciousRange;
    protected bool IsInAggroRange;
    protected bool IsInChaseRange;
    protected bool LineOfSight;
    BaseEnemyState currentState;

    public EnemyChasing ChasingState = new EnemyChasing();
    public EnemyDefault DefaultState = new EnemyDefault();
    public EnemySuspicious SuspiciousState = new EnemySuspicious();
    public EnemyAttacking AttackingState = new EnemyAttacking();
    public EnemyRetreating RetreatingState = new EnemyRetreating();



        protected virtual void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        movement = GetComponent<AIPath>();
        movement.maxSpeed = Speed;
        LevelSystem = Player.GetComponent<LevelSystem>();
        DamageScript = Player.GetComponent<Damage>();
        PlayerCollider = Player.GetComponent<CircleCollider2D>();
        currentState = DefaultState;
        currentState.EnterState(this);
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        AIDestinationSetterScript = GetComponent<AIDestinationSetter>();
        

        if(ChallengeLevel >= 0 )
        {
            ChallengeLevel = 1;
        }

    }

        protected virtual void Start()
    {
Vector3 dir = new Vector3(-21, 18, 0);
    }

        protected virtual void Update()
    {

        IsInAttackRange = Physics2D.OverlapCircle(transform.position, AttackRadius, WhatIsPlayer);
        IsInSuspiciousRange = Physics2D.OverlapCircle(transform.position, SuspiciousRadius, WhatIsPlayer);
        IsInChaseRange = Physics2D.OverlapCircle(transform.position, ChaseRadius, WhatIsPlayer);

    }

        protected virtual void FixedUpdate()
    {
        
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        Vector2 target = new Vector2(Player.transform.position.x + PlayerCollider.offset.x, Player.transform.position.y + PlayerCollider.offset.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, target - origin, SuspiciousRadius, IgnoreTheseLayers);
        Debug.DrawRay(origin, (target - origin), Color.blue);

        if (hit.collider == PlayerCollider)
        {
            LineOfSight = true;
        }
        else
        {
            LineOfSight = false;
        }
         if(IsInAggroRange && !IsInAttackRange)
        {           
            //  MoveEnemy(Dir);
             if (HasAnAttack)
             {
                StopCoroutine("Attack");
             }
        }
         if(IsInAttackRange && LineOfSight)
        {
            // Rb.velocity = Vector2.zero;
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

// Called in Weapon Anim script to cause damage to Enemy needs.
// to be public can be overridden to change how much damage Enemy will take such as damaageAmount /= 2;.
        public virtual void TakeDamage(int damageAmount)
    {
        EnemyHealth -= damageAmount;
        if(EnemyHealth <= 0)
        {
            Anim.SetBool("Death", true);
        }
    }

        protected virtual void Death()
    {
        LevelSystem.GainExperience(ChallengeLevel+LevelSystem.playerLvl*100);
        Destroy(gameObject);
    }
}
