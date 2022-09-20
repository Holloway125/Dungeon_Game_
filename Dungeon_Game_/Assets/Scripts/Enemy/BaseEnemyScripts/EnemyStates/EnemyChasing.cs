using UnityEngine;

public class EnemyChasing : BaseEnemyState
{
public override void EnterState(BaseEnemy Enemy)
    {
        Enemy.Aggroed = true;
        Enemy.AIDestinationSetterScript.target = Enemy.Player.transform.position;
        Enemy.movement.maxSpeed = Enemy.Speed;
        Enemy.Animate("Run");
    }

public override void UpdateState(BaseEnemy Enemy)
    {
        Enemy.LOS();
        Enemy.IsInChaseRange = Physics2D.OverlapCircle(Enemy.transform.position, Enemy.ChaseRadius, Enemy.WhatIsPlayer);
        Enemy.DistanceFromHome = Mathf.Round(Vector3.Distance(Enemy.Home, Enemy.transform.position));
        if(Enemy.HasAnAttack)
        {
        Enemy.IsInAttackRange = Physics2D.OverlapCircle(Enemy.transform.position, Enemy.AttackRadius, Enemy.WhatIsPlayer);
        }

        if (Enemy.DistanceFromHome > Enemy.Leash)
        {
            Enemy.SwitchState(Enemy.RetreatingState);
            Enemy.Animate("Run");
        }
        if (Enemy.IsInChaseRange)
        {
            Enemy.AIDestinationSetterScript.target = Enemy.Player.transform.position;
            Enemy.Animate("Run");
        }
        
        // if (Enemy.IsInAttackRange && Enemy.LineOfSight && Enemy.HasAnAttack)
        // {
        //     Enemy.SwitchState(Enemy.AttackingState);
        // }
        
        if (!Enemy.IsInChaseRange)
        {
            Enemy.Aggroed = false;
            Enemy.SwitchState(Enemy.RetreatingState);
            Enemy.Animate("Run");
        }
        // if (Enemy.CurrentHealth < 0)
        // {
        //     Enemy.SwitchState(Enemy.DeathState);
        // }
    }

}
