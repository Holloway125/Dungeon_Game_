using UnityEngine;

public class EnemyDefault : BaseEnemyState
{
public override void EnterState(BaseEnemy Enemy)
    {
        Enemy.Animate("Idle");
        Enemy.Aggroed = false;
        Enemy.movement.maxSpeed = Enemy.Speed;
    }

public override void UpdateState(BaseEnemy Enemy)
    {
        Debug.Log("Default");
        if (Enemy.IsInSuspiciousRange)
        {
           Enemy.SwitchState(Enemy.SuspiciousState);
        }  
        else if (!Enemy.IsInSpawn)
        {
            Enemy.AIDestinationSetterScript.target = Enemy.Home;
        }
        if (Enemy.CurrentHealth < 0)
        {
        Enemy.SwitchState(Enemy.DeathState);
        }

    }

}

