using UnityEngine.AI;
using UnityEngine;

// public class Enemy : MonoBehaviour
// {
//     public NavMeshAgent agent;
//     public Transform player;
//     public LayerMask whatIsGround, whatIsPlayer;

//     //patrolling 
//     public Vector2 walkPoint;
//     bool walkPointSet;
//     public float walkPointRange;

//     //Attacking
//     public float timeBetweenAttacks;
//     bool alreadyAttacked;

//     //States
//     public float sightRange, attackRange;
//     public bool playerInSightRange, playerInAttackRange;

//     private void Awake()
//     {
//         player = GameObject.Find("PlayerObj").transform;
//         agent = GetComponent<NavMeshAgent>();

//     }

//     private void Update()
//     {
//         //Check for sight and attack range 
//         playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
//         playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

//         if(!playerInSightRange && !playerInAttackRange) Patroling();
//         if (playerInSightRange && !playerInAttackRange) ChasePlayer();
//         if (playerInAttackRange && playerInAttackRange) AttackPlayer();
//     }

// //     private void Patroling()
// //     {
// //         if(!walkPointSet) SearchWalkPoint();

// //         if(walkPointSet)
// //             agent.SetDestination(walkPoint);

// //         Vector2 distanceToWalkPoint = transform.position - walkPoint;

// //         //Walkpoint reached
// //         if (distanceToWalkPoint.magnitude < 1f)
// //             walkPointSet = false;

// //     }

// //     private void SearchWalkPoint()
// //     {
// //         //Calculate random point in range
// //         float randomY = Random.Range(-walkPointRange, walkPointRange);
// //         float randomX = Random.Range(-walkPointRange, walkPointRange);
// //         walkPoint = new Vector2(transform.position.x + randomX, transform.position.y + randomY);

// //         if ( Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
// //             walkPointSet = true;
// //     }

// //     private void Chase ChasePlayer()
// //     {
// //            agent.SetDestination(player.position); 
// //     }

// //     private void AttackPlayer()
// //     //make sure enemydoesn't move
// //     agent.SetDestination(Transform.position);

// //     transform.LookAt(player);

// //     if (!alreadyAttacked)
// //     {
// //         alreadyAttacked = true;
// //         Invoke(nameof(ResetAttack), timeBetweenAttacks);
        
// //     }

// // }
