using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeap : Ability 
{
    

    public override void Activate(GameObject enemy)
     {
        var Enemy = enemy.GetComponent<BaseEnemy>();        
     }

    public override void BeginCooldown(GameObject enemy)
     {
         var Enemy = enemy.GetComponent<BaseEnemy>();
     }

}   