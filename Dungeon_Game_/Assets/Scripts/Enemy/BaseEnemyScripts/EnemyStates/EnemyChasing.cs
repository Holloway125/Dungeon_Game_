using UnityEngine;

public class EnemyChasing : BaseEnemyState
{
public override void EnterState(BaseEnemy Enemy)
    {
        Enemy.Aggroed = true;
        Enemy.AIDestinationSetterScript.target = Enemy.Player.transform.position;
        Enemy.movement.maxSpeed = Enemy.Speed;
        Debug.Log("I am Chasing");
    }

public override void UpdateState(BaseEnemy Enemy)
    {
                Debug.Log("ChasingUpdate");
        if (Enemy.IsInChaseRange)
        {
            Enemy.AIDestinationSetterScript.target = Enemy.Player.transform.position;
        }
        else if (Enemy.IsInAttackRange && Enemy.LineOfSight)
        {
            Enemy.SwitchState(Enemy.AttackingState);
        }
        else if (!Enemy.IsInChaseRange)
        {
            Enemy.Aggroed = false;
            Enemy.SwitchState(Enemy.RetreatingState);

        }
    }

}
