using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDash : Ability 
{
    public float dashVelocity;

    public override void Activate(GameObject enemy)
     {
         Minotaur Enemy = enemy.GetComponent<Minotaur>();
         Enemy.movement.maxSpeed = Enemy.Speed + dashVelocity;
     }

    public override void BeginCooldown(GameObject enemy)
     {
         Minotaur Enemy = enemy.GetComponent<Minotaur>();
         Enemy.movement.maxSpeed = Enemy.Speed;
     }

}    