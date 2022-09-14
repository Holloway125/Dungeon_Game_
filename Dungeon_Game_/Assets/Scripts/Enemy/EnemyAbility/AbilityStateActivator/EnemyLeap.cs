using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeap : Ability 
{

public float Force;

    public override void Activate(GameObject enemy)
     {
        var Enemy = enemy.GetComponent<BaseEnemy>();
        Enemy.Animate("Attack");

     }

    public override void BeginCooldown(GameObject enemy)
     {
         var Enemy = enemy.GetComponent<BaseEnemy>();

     }


}   