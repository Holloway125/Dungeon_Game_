using UnityEngine;

public class EnemyChasing : BaseEnemyState
{
public override void EnterState(BaseEnemy Enemy)
    {
        Enemy.Aggroed = true;
        Enemy.AIDestinationSetterScript.target = Enemy.Player.transform.position;
        Enemy.movement.maxSpeed = Enemy.Speed;
    }

public override void UpdateState(BaseEnemy Enemy)
    {
        Debug.Log("ChasingUpdate");
        if (Enemy.DistanceFromHome > Enemy.Leash)
        {
            Enemy.SwitchState(Enemy.RetreatingState);
        }
        if (Enemy.IsInChaseRange)
        {
            Enemy.AIDestinationSetterScript.target = Enemy.Player.transform.position;
        }
        
        if (Enemy.IsInAttackRange && Enemy.LineOfSight)
        {
            Enemy.SwitchState(Enemy.AttackingState);
        }
        
        if (!Enemy.IsInChaseRange)
        {
            Enemy.Aggroed = false;
            Enemy.SwitchState(Enemy.RetreatingState);
        }
    }

}