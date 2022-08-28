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
    public float AggroRange;

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
    public bool IsInSpawn;
    public bool LineOfSight;
    public bool Aggroed;
    public Vector3 Home;


    BaseEnemyState currentState;
    public EnemyDefault DefaultState = new EnemyDefault();
    public EnemySuspicious SuspiciousState = new EnemySuspicious();
    public EnemyChasing ChasingState = new EnemyChasing();
    public EnemyAttacking AttackingState = new EnemyAttacking();
    public EnemyRetreating RetreatingState = new EnemyRetreating();

    public void SwitchState(BaseEnemyState state)
    {
        currentState = state;
        state.EnterState(this);
    }


        protected virtual void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        movement = GetComponent<AIPath>();
        movement.maxSpeed = Speed;
        LevelSystem = Player.GetComponent<LevelSystem>();
        DamageScript = Player.GetComponent<Damage>();
        PlayerCollider = Player.GetComponent<CircleCollider2D>();
        currentState = DefaultState;
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        AIDestinationSetterScript = GetComponent<AIDestinationSetter>();
        Vector3 dir = new Vector3(-21, 18, 0);
        Aggroed = false;
        Home = this.transform.position;
        AIDestinationSetterScript.target = Home;
        
        
        if(ChallengeLevel >= 0 )
        {
            ChallengeLevel = 1;        
        }

    }
        protected virtual void Start()
    {
        currentState.EnterState(this);
    }

        protected virtual void Update()
    {
        currentState.UpdateState(this);

    }

        protected virtual void FixedUpdate()
    {
        IsInChaseRange = Physics2D.OverlapCircle(transform.position, ChaseRadius, WhatIsPlayer);
        IsInAttackRange = Physics2D.OverlapCircle(transform.position, AttackRadius, WhatIsPlayer);
        IsInSuspiciousRange = Physics2D.OverlapCircle(transform.position, SuspiciousRadius, WhatIsPlayer);
        IsInAggroRange = Physics2D.OverlapCircle(transform.position, AggroRange, WhatIsPlayer);
        IsInSpawn = Physics2D.OverlapCircle(Home, 5);
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
public virtual void InvokeRetreat()
{
    float seconds = Random.Range(2,5);
    Invoke("Retreat", seconds);
}
public virtual void Retreat()
{
    Vector3 RandomPoint = Random.insideUnitCircle;
    AIDestinationSetterScript.target = Home + RandomPoint;
}

// Called in Weapon Anim script to cause damage to Enemy
// Needs to be public 
// Can be overridden to change how much damage Enemy will take such as damaageAmount /= 2;.
        public virtual void TakeDamage(int damageAmount)
    {
        EnemyHealth -= damageAmount;
        if(EnemyHealth <= 0)
        {
            Anim.SetBool("Death", true);
        }
    }
// Used to Give experience and destroy gameObject in Animator.
        public virtual void Death()
    {
        LevelSystem.GainExperience(ChallengeLevel+LevelSystem.playerLvl*100);
        Destroy(gameObject);
    }

    public virtual void DestroySuspect(GameObject Suspect)
    {
        Destroy(Suspect);
    }
}
