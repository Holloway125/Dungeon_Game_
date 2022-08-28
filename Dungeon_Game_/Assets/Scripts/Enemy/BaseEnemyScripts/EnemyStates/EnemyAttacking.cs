using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : BaseEnemyState
{
public override void EnterState(BaseEnemy Enemy)
    {
        Enemy.Aggroed = true;
        Enemy.AIDestinationSetterScript.target = Enemy.Player.transform.position;
        Enemy.movement.maxSpeed = Enemy.Speed;
        Debug.Log("I am Attacking");
    }

public override void UpdateState(BaseEnemy Enemy)
    {
                Debug.Log("AttackingUpdate");
        if (Enemy.IsInAttackRange)
        {
            Enemy.Attack();
        }

    }

}
