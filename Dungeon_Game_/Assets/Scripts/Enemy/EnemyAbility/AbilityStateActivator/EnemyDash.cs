// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyDash : Ability 
// {
//     public float dashVelocity;

//     public override void Activate(GameObject enemy)
//      {
//          var Enemy = enemy.GetComponent<BaseEnemy>();
//          Enemy.movement.maxSpeed = Enemy.Speed + dashVelocity;
//      }

//     public override void BeginCooldown(GameObject enemy)
//      {
//          var Enemy = enemy.GetComponent<BaseEnemy>();
//          Enemy.movement.maxSpeed = Enemy.Speed;
//      }

// }    