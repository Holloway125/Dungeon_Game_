using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class BaseEnemy : MonoBehaviour
{

// Make sure Player is on Player Layer in Inspector.
    [Header ("Defines Player LayerMask")]
    public LayerMask WhatIsPlayer; 
    [Header ("Layers Ignored for PathFinding")]
    public LayerMask IgnoreTheseLayers;
    [Space]    
// Basic Enemy Stats Set in Inspector
    [Header("Enemy Stats")]
    public float EnemyMaxHealth;   
    public float CurrentHealth;
    public float Speed;
    public int ChallengeLevel;
// Defined on Awake/Start/Update/FixedUpdate Functions.
    [HideInInspector]
    public Animator Anim;
    [HideInInspector]
    public GameObject Player;
    [HideInInspector]
    public Damage DamageScript;
    [HideInInspector]
    public LevelSystem LevelSystem;
    [HideInInspector]
    public Rigidbody2D Rb;
    [HideInInspector]
    public AIPath movement;
    [HideInInspector]
    public AIDestinationSetter AIDestinationSetterScript;
    [Space]
// Set in Inspector used for attacking Player.
    [Header ("Range Settings")]
    public float AttackRadius;
    public float SuspiciousRadius;
    public float ChaseRadius;
    public float AggroRadius;
    public int Leash;
    [Space]
    [Header ("Attack Stats")]
    // If Enemy has weapon or ability thats played through an Animator then mark true in the Inspector.
    public bool HasAnAttack;
    public int AttackDamaage;
    public float AttackCooldown;
    [Space]
    [Header ("Contact Attack Stats")]
    // If Enemy has a on collision damage then mark true in the inspector
    public bool HasContactAttack;
    public int ContactDamage;
    public float ContactAttackCooldown;

    // Used for Attack.
    [HideInInspector]
    public bool IsCollided;
    [HideInInspector]
    public Collider2D PlayerCollider;
    // Used for Finite State Machine Logic.
    [HideInInspector]
    public bool IsInAttackRange;
    [HideInInspector]
    public bool IsInSuspiciousRange;
    [HideInInspector]
    public bool IsInAggroRange;
    [HideInInspector]
    public bool IsInChaseRange;
    [HideInInspector]
    public bool IsInSpawn;
    [HideInInspector]
    public bool LineOfSight;
    [HideInInspector]
    public bool Aggroed;
    [HideInInspector]
    public Vector3 Home;
    [HideInInspector]
    public float DistanceFromHome;


    BaseEnemyState currentState;
    [HideInInspector]
    public EnemyDefault DefaultState = new EnemyDefault();
    [HideInInspector]
    public EnemySuspicious SuspiciousState = new EnemySuspicious();
    [HideInInspector]
    public EnemyChasing ChasingState = new EnemyChasing();
    [HideInInspector]
    public EnemyAttacking AttackingState = new EnemyAttacking();
    [HideInInspector]
    public EnemyRetreating RetreatingState = new EnemyRetreating();
    [HideInInspector]
    public EnemyAbilityCast AbilityState = new EnemyAbilityCast();
    [HideInInspector]
    public EnemyDeathState DeathState = new EnemyDeathState();
    [HideInInspector]
    //For Animator
    private float angleDegree;
    public bool AttackAnimPlaying = false;
    public bool HasAbility;
    public bool HasAbility2;
    public EnemyAbilityHolder Ability;
    public EnemyAbilityHolder Ability2;
    


    public void SwitchState(BaseEnemyState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void EnterAbilityState()
    {
        currentState = AbilityState;
        AbilityState.EnterState(this);
    }
    public void ExitAbilityState()
    {
        currentState = ChasingState;
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
        CurrentHealth = EnemyMaxHealth;
    
        if(ChallengeLevel >= 0 )
        {
            ChallengeLevel = 1;        
        }

    }
        protected virtual void Start()
    {
        currentState.EnterState(this);
        EnemyDirection();
    }

        protected virtual void Update()
    {
        currentState.UpdateState(this);
        if (currentState != DefaultState)
        {
        EnemyDirection();
        }

    }

        protected virtual void FixedUpdate()
    {
    }
// Functions for determining which anim should play based on the direction the enemy is going

//                        90
//                        up
//                     300 - 240 
//                299               241
//  180  | left  -                 -  right | 360
//                 59               121
//                      60 - 120 
//                        down
//                         270    


        public void Animate(string action)
        {
            if (Anim.GetBool("up"))
            {
                Anim.Play($"{action}Up");
            }
            else if (Anim.GetBool("down"))
            {
                Anim.Play($"{action}Down");
            }
            else if (Anim.GetBool("left"))
            {
                Anim.Play($"{action}Left");    
            }
            else if (Anim.GetBool("right"))
            {
                Anim.Play($"{action}Right");   
            }

        }
        public void AttackAnimate()
        {
            if (!AttackAnimPlaying)
            {
                Animate("Attack");
            }
        }
        public void UpdateTarget()
        {
            AIDestinationSetterScript.target = Player.transform.position;
        }
        public void UpdateTarget(Vector3 target)
        {
            AIDestinationSetterScript.target = target;
        }
        public void EnemyDirection()
    {
        float angle;
        Vector3 Direction;
        Direction = AIDestinationSetterScript.target - transform.position;
        angle = Mathf.Atan2(Direction.y, Direction.x);
        angleDegree = angle * Mathf.Rad2Deg;

            if (angleDegree <= 0)
        {
            angleDegree += 360;
        }

            if(angleDegree <= 360)

        {       
            if (angleDegree <= 300 &&angleDegree >= 240)
                {
                    Anim.SetBool("up",false);
                    Anim.SetBool("down",true);
                    Anim.SetBool("left",false);
                    Anim.SetBool("right",false);
                }
                else if (angleDegree <= 120 &&angleDegree >= 60)
                {
                    Anim.SetBool("up",true);
                    Anim.SetBool("down",false);
                    Anim.SetBool("left",false);
                    Anim.SetBool("right",false);
                }
                else if (angleDegree <= 360 &&angleDegree >= 299 ||angleDegree <= 59 &&angleDegree >= 0)
                {
                    Anim.SetBool("up",false);
                    Anim.SetBool("down",false);
                    Anim.SetBool("left",false);
                    Anim.SetBool("right",true);
                }        
                else if (angleDegree <= 241 &&angleDegree >= 121)
                {
                    Anim.SetBool("up",false);
                    Anim.SetBool("down",false);
                    Anim.SetBool("left",true); 
                    Anim.SetBool("right",false);
                }
        }

    }    
        public void LOS()  
        {       
                Vector2 origin = new Vector2(transform.position.x, transform.position.y);
                Vector2 target = new Vector2(Player.transform.position.x + PlayerCollider.offset.x, Player.transform.position.y + PlayerCollider.offset.y);
                RaycastHit2D hit = Physics2D.Raycast(origin, target - origin, SuspiciousRadius, IgnoreTheseLayers);

                if (hit.collider == PlayerCollider)
                {
                    LineOfSight = true;
                }
                else
                {
                    LineOfSight = false;
                }


                if (CurrentHealth > EnemyMaxHealth)
                {
                    CurrentHealth = EnemyMaxHealth;
                }
        }
//Function for doing damage
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
        if (Vector3.Distance(AIDestinationSetterScript.target, Home) >= 5f)
        {
        Vector3 RandomPoint = Random.insideUnitCircle;
        AIDestinationSetterScript.target = Home + RandomPoint;
        Animate("Run");
        // Debug.Log(Vector3.Distance(AIDestinationSetterScript.target, Home));
        }
    }

// Called in Weapon Anim script to cause damage to Enemy
// Can be overridden to change how much damage Enemy will take such as damaageAmount /= 2;.
        public virtual void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        if(CurrentHealth <= 0)
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