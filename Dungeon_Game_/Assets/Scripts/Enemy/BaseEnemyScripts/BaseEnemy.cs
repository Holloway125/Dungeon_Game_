using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class BaseEnemy : MonoBehaviour
{

// Make sure Player is on Player Layer in Inspector.
    [SerializeField]
    public LayerMask WhatIsPlayer; 

    [Space]    

// Basic Enemy Stats Set in Inspector.
    [SerializeField]
    public float EnemyHealth;   
    [SerializeField]
    public float Speed;
    [SerializeField]
    public int ChallengeLevel;
// Defined on Awake/Start/Update/FixedUpdate Functions.
    public Animator Anim;
    public GameObject Player;
    public Damage DamageScript;
    public Vector2 Dir;
    public LevelSystem LevelSystem;
    // public Transform Target;
    public Rigidbody2D Rb;
    public AIPath movement;
    public AIDestinationSetter AIDestinationSetterScript;
    

    [Space]
 
// Set in Inspector used for attacking Player.


    [SerializeField]
    public float AttackRadius;
    public float SuspiciousRadius;
    public float ChaseRadius;

    [Space]

    // If Enemy has weapon or ability thats played through an Animator then mark true in the Inspector.
    [SerializeField]
    public bool HasAnAttack;
    [SerializeField]
    public int AttackDamaage;
    [SerializeField]
    public float AttackCooldown;

    [Space]

    // If Enemy has a on collision damage then mark true in the inspector.
    [SerializeField]
    public bool HasContactAttack;
    [SerializeField]
    public int ContactDamage;
    [SerializeField]
    public float ContactAttackCooldown;

// Used for attacking.
    public bool IsCollided;
    public Collider2D PlayerCollider;
    [SerializeField]
    public LayerMask IgnoreTheseLayers;
    public bool IsInAttackRange;
    public bool IsInSuspiciousRange;
    public bool IsInAggroRange;
    public bool IsInChaseRange;
    public bool LineOfSight;
    BaseEnemyState currentState;

    public EnemyDefault DefaultState = new EnemyDefault();
    public EnemySuspicious SuspiciousState = new EnemySuspicious();
    public EnemyChasing ChasingState = new EnemyChasing();
    public EnemyAttacking AttackingState = new EnemyAttacking();
    public EnemyRetreating RetreatingState = new EnemyRetreating();



        public virtual void Awake()
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
        public virtual void Start()
    {
        Vector3 dir = new Vector3(-21, 18, 0);
    }

        public virtual void Update()
    {
        currentState.UpdateState(this);
        IsInAttackRange = Physics2D.OverlapCircle(transform.position, AttackRadius, WhatIsPlayer);
        IsInChaseRange = Physics2D.OverlapCircle(transform.position, ChaseRadius, WhatIsPlayer);

    }

        public virtual void FixedUpdate()
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
        public void OnCollisionEnter2D(Collision2D collision)
     {
        if (collision.gameObject.tag == "Player" && HasContactAttack)
        {
            IsCollided = true;
            StartCoroutine("ContactAttack");
        }
     }

        public void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player" && HasContactAttack)
            {
                IsCollided = false;
                StopCoroutine("ContactAttack");
            }
        }
    public void SwitchState(BaseEnemyState state)
    {
        currentState = state;
        state.EnterState(this);
    }
// Coroutine that does damage over time when started.
        public IEnumerator ContactAttack()
        {
            while(IsCollided)
            {
                 DamageScript.TakeDamage(ContactDamage); 
                yield return new WaitForSeconds(ContactAttackCooldown);
            }
            yield return null;
            
        }

// Coroutine used to play attack animation of Enemies that have an attack.
       public virtual IEnumerator Attack()
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

        public virtual void Death()
    {
        LevelSystem.GainExperience(ChallengeLevel+LevelSystem.playerLvl*100);
        Destroy(gameObject);
    }

}
