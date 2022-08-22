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
    protected float AggroRadius;
    [SerializeField]
    protected float SuspiciousRadius;
    [SerializeField]
    protected float ChaseRadius;
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
    protected bool IsInAggroRange;
    protected bool IsInAttackRange;
    protected bool IsInSuspiciousRange;
    protected bool IsInChaseRange;
    protected bool IsCollided;
    protected bool LineOfSight;
    protected bool Aggroed;
    protected Vector3 StartPosition;
    protected Collider2D PlayerCollider;
    [SerializeField]
    protected LayerMask IgnoreTheseLayers;





        protected virtual void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        movement = GetComponent<AIPath>();
        movement.maxSpeed = Speed;
        LevelSystem = Player.GetComponent<LevelSystem>();
        DamageScript = Player.GetComponent<Damage>();
        PlayerCollider = Player.GetComponent<CircleCollider2D>();
        // Target = Player.transform;
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        AIDestinationSetterScript = GetComponent<AIDestinationSetter>();
        StartPosition = transform.position;
        

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
        Anim.SetBool("isMoving", IsInAggroRange);
        IsInAggroRange = Physics2D.OverlapCircle(transform.position, AggroRadius, WhatIsPlayer);
        IsInAttackRange = Physics2D.OverlapCircle(transform.position, AttackRadius, WhatIsPlayer);
        IsInSuspiciousRange = Physics2D.OverlapCircle(transform.position, SuspiciousRadius, WhatIsPlayer);
        IsInChaseRange = Physics2D.OverlapCircle(transform.position, ChaseRadius, WhatIsPlayer);
        
        // Dir = Target.position - transform.position;
        // float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        // Dir.Normalize();

        //If Enemy can see player and is in aggro range then enemy has been pulled

        if (IsInAggroRange && LineOfSight)
        {
            AIDestinationSetterScript.target = Player.transform;
            movement.maxSpeed = Speed;
            Aggroed = true;
        }
        //If Enemy has been pulled then it will keep chasing until out of chase range no matter LOS
        else if (IsInChaseRange && Aggroed)
        {
            AIDestinationSetterScript.target = Player.transform;
            movement.maxSpeed = Speed;
        }
        //IF player is in its aggro range but has no line of sight and hasn't been pulled then it will move towards player at 1/3 speed
        else if (IsInAggroRange && !LineOfSight && !Aggroed)
        {
            AIDestinationSetterScript.target = Player.transform;
            movement.maxSpeed = Speed/3;
            Aggroed = false;
        }
        //if Player is in enemies suspicious range then it will move towards player at 1/4 speed
        else if (IsInSuspiciousRange && !LineOfSight)
        {
            AIDestinationSetterScript.target = Player.transform;
            movement.maxSpeed = Speed/4;
            Aggroed = false;
        }
        //if Player is in enemies suspicious range and it has line of sight it will move towards player at 1/2 speed
        else if (IsInSuspiciousRange && LineOfSight)
        {
            AIDestinationSetterScript.target = Player.transform;
            movement.maxSpeed = Speed/2;
            Aggroed = false;
        }
        //if player is not in suspicious range and aggro and chase then enemy will reset
        else if (!IsInSuspiciousRange && !IsInAggroRange && !IsInChaseRange)
        {
          //  AIDestinationSetterScript.target = StartPosition;
            movement.maxSpeed = Speed;
            Aggroed = false;
        }

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

    //     protected void MoveEnemy(Vector2 Dir)
    // {
    //     Rb.MovePosition((Vector2)transform.position + (Dir * Speed * Time.deltaTime));
    // }
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
