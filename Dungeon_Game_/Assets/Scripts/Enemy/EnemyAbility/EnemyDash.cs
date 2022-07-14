using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDash : Ability 
{
    public float dashVelocity;

    public override void Activate(GameObject enemy)
     {
        Minotaur movement = enemy.GetComponent<Minotaur>();
        movement.Speed = movement.Speed * dashVelocity;
     }

    public override void BeginCooldown(GameObject enemy)
     {
        Minotaur movement = enemy.GetComponent<Minotaur>();
        movement.Speed = 3f;
     }

}    