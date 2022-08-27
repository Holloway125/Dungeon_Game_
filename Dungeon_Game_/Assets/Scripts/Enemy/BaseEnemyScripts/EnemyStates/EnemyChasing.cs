using UnityEngine;

public class EnemyChasing : BaseEnemyState
{
public override void EnterState(BaseEnemy Enemy)
    {
        Enemy.Aggroed = true;
        Enemy.AIDestinationSetterScript.target = Enemy.Player.transform;
        Enemy.movement.maxSpeed = Enemy.Speed;
        Debug.Log("I am Chasing");
    }

public override void UpdateState(BaseEnemy Enemy)
    {
        if (Enemy.IsInChaseRange)
        {
            Enemy.AIDestinationSetterScript.target = Enemy.Player.transform;
        }
        else 
        {
            Enemy.SwitchState(Enemy.RetreatingState);

        }
    }

}
