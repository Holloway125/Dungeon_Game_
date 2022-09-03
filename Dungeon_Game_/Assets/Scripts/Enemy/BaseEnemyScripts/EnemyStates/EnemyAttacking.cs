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
    }

public override void UpdateState(BaseEnemy Enemy)
    {
                Debug.Log("AttackingUpdate");
        if (Enemy.IsInAttackRange && Enemy.HasAnAttack)
        {
            Enemy.Attack();
        }
        else 
        {
            Enemy.SwitchState(Enemy.ChasingState);
        }
        if (Enemy.CurrentHealth < 0)
        {
        Enemy.SwitchState(Enemy.DeathState);
        }

    }

}
