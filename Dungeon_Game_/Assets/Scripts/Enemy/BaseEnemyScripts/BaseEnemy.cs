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
    public int ExpGiven;
// Defined on Awake/Start/Update/FixedUpdate Functions.
    [HideInInspector]
    public Animator Anim;
    [HideInInspector]
    public GameObject Player;
    [HideInInspector]
    public PlayerController PlayerController;
    [HideInInspector]
    public LevelSystem LevelSystem;
    [HideInInspector]
    public Rigidbody2D Rb;
    [HideInInspector]
    public AIPath AIPath;
    [HideInInspector]
    public AIDestinationSetter AIDestinationSetter;

//     [Space]
// // Set in Inspector used for attacking Player.
//     [Header ("Range Settings")]
//     public float AttackRadius;
//     public float SuspiciousRadius;
//     public float ChaseRadius;
//     public float AggroRadius;
//     public int Leash;
    [Space]
    [Header ("Attack Stats")]
    // If Enemy has weapon or ability thats played through an Animator then mark true in the Inspector.
    public CircleCollider2D AttackTrigger;
    public bool HasAnAttack;
    public int AttackDamaage;
    public float AttackCooldown;
    [Space]
    public Collider2D PlayerCollider;
    public AttackTrigger attackTrigger;

    public bool Aggroed;
    public Transform Home;


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

    public bool AttackAnimPlaying = false;
    private float angle;
    private float MyAngleDegree;

    BaseEnemy instance;


    protected virtual void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        AIPath = GetComponent<AIPath>();
        AIPath.maxSpeed = Speed;
        LevelSystem = Player.GetComponent<LevelSystem>();
        PlayerController = Player.GetComponent<PlayerController>();
        PlayerCollider = Player.GetComponent<PolygonCollider2D>();
        attackTrigger = GetComponentInChildren<AttackTrigger>();
        //currentState = DefaultState;
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        AIDestinationSetter = GetComponent<AIDestinationSetter>();
        Vector3 dir = new Vector3(-21, 18, 0);
        Aggroed = false;
        Home = this.transform;
        AIDestinationSetter.target = Home;
        CurrentHealth = EnemyMaxHealth;
        instance = this;
    }
    
    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        EnemyDirection();

        // Vector3 diffPos = Player.transform.position - this.transform.position;
        // //returns a radian that can be used to convert to degrees 
        // angle = Mathf.Atan2(diffPos.y, diffPos.x);
        // //converts the radian to degrees in the range of (-180, 180)
        // MyAngleDegree = angle * Mathf.Rad2Deg;
        // //converts the range of (-180, 180) to a range of (0, 360) which then can be passed into the rotation z value of an object to set it rotation pointing to the cursors current position
        // if(MyAngleDegree < 0)
        // {
        //     MyAngleDegree+=360;
        // }   
        // //Debug.Log(MyAngleDegree);
    }

    protected virtual void FixedUpdate()
    {
        if(attackTrigger.InRange == false && AttackAnimPlaying)
        {
            StopCoroutine("Attack");
        }
        else if(attackTrigger.InRange == true && !AttackAnimPlaying)
        {
            StartCoroutine("Attack");
        }
    }

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

    public void EnemyDirection()
    {
        Vector3 Direction;
        Direction = AIDestinationSetter.target.position - this.transform.position;
        Anim.SetFloat("X", Direction.x);
        Anim.SetFloat("Y", Direction.y);
    }   

// Coroutine used to play attack animation of Enemies that have an attack.
    public virtual IEnumerator Attack()
    {
        while(attackTrigger.InRange == true)
        {
        Anim.Play("Attack");
        Debug.Log(" Attacking ");
        yield return new WaitForSeconds(AttackCooldown);
        }
        AttackAnimPlaying = false;
        Debug.Log("went to null yield");
        yield return null;
    }   

    public virtual void InvokeRetreat()
    {
        float seconds = Random.Range(2,5);
        Invoke("Retreat", seconds);
    }

    public virtual void Retreat()
    {
        if (Vector3.Distance(AIDestinationSetter.target.position, Home.position) >= 5f)
        {
        Vector3 RandomPoint = Random.insideUnitCircle;
        AIDestinationSetter.target.position = Home.position + RandomPoint;
        Animate("Run");
        // Debug.Log(Vector3.Distance(AIDestinationSetter.target, Home));
        }
    }

    public virtual void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        if(CurrentHealth <= 0)
        {
            Anim.Play("Death");
        }
    }
    
    public virtual void MonsterExp()
    {
        LevelSystem.GainExperience(ExpGiven);
    }

// Used to Give experience and destroy gameObject in Animator.
    public virtual void Death()
    {
        Destroy(gameObject);
    }
    // public void LOS()  
    // {       
    //     Vector2 origin = new Vector2(transform.position.x, transform.position.y);
    //     Vector2 target = new Vector2(Player.transform.position.x + PlayerCollider.offset.x, Player.transform.position.y + PlayerCollider.offset.y);
    //     RaycastHit2D hit = Physics2D.Raycast(origin, target - origin, SuspiciousRadius, IgnoreTheseLayers);

    //     if (hit.collider == PlayerCollider)
    //     {
    //         LineOfSight = true;
    //     }

    //     else
    //     {
    //         LineOfSight = false;
    //     }
    // }

// Coroutine that does damage over time when started.
    // public IEnumerator ContactAttack()
    // {
    //     while(IsCollided)
    //     {
    //         PlayerController.TakeDamage(ContactDamage); 
    //         yield return new WaitForSeconds(ContactAttackCooldown);
    //     }
    //     yield return null;
    // }
    // public virtual void DestroySuspect(GameObject Suspect)
    // {
    //     Destroy(Suspect);
    // }
        //float angle;
        // angle = Mathf.Atan2(Direction.y, Direction.x);
        // angleDegree = angle * Mathf.Rad2Deg;

        // if (angleDegree <= 0)
        // {
        //     angleDegree += 360;
        // }

        // if(angleDegree <= 360)
        // {       
        //     if (angleDegree <= 300 && angleDegree >= 240)
        //     {
        //         Anim.SetBool("up",false);
        //         Anim.SetBool("down",true);
        //         Anim.SetBool("left",false);
        //         Anim.SetBool("right",false);
        //     }

        //     else if (angleDegree <= 120 && angleDegree >= 60)
        //     {
        //         Anim.SetBool("up",true);
        //         Anim.SetBool("down",false);
        //         Anim.SetBool("left",false);
        //         Anim.SetBool("right",false);
        //     }

        //     else if (angleDegree <= 360 && angleDegree >= 299 ||angleDegree <= 59 &&angleDegree >= 0)
        //     {
        //         Anim.SetBool("up",false);
        //         Anim.SetBool("down",false);
        //         Anim.SetBool("left",false);
        //         Anim.SetBool("right",true);
        //     }  

        //     else if (angleDegree <= 241 && angleDegree >= 121)
        //     {
        //         Anim.SetBool("up",false);
        //         Anim.SetBool("down",false);
        //         Anim.SetBool("left",true); 
        //         Anim.SetBool("right",false);
        //     }
        // }
}