using UnityEngine;

public class EnemyDefault : BaseEnemyState
{
public override void EnterState(BaseEnemy Enemy)
    {
        Enemy.Aggroed = false;
        Enemy.movement.maxSpeed = Enemy.Speed;
        Enemy.Anim.Play("Idle");
    }

public override void UpdateState(BaseEnemy Enemy)
    {
        Enemy.IsInSuspiciousRange = Physics2D.OverlapCircle(Enemy.transform.position, Enemy.SuspiciousRadius, Enemy.WhatIsPlayer);
        if (Enemy.IsInSuspiciousRange)
        {
           Enemy.SwitchState(Enemy.SuspiciousState);
        }  
        else if (Enemy.IsInSpawn)
        {
            Enemy.AIDestinationSetterScript.target = Enemy.transform.position;
        }
        // if (Enemy.CurrentHealth < 0)
        // {
        // Enemy.SwitchState(Enemy.DeathState);
        // }

    }

}

