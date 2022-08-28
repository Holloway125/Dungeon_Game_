using UnityEngine;

public class EnemyDefault : BaseEnemyState
{
public override void EnterState(BaseEnemy Enemy)
    {
        Enemy.Aggroed = false;
        Enemy.movement.maxSpeed = Enemy.Speed;
        Debug.Log("Hello I am in DefaultState");
    }

public override void UpdateState(BaseEnemy Enemy)
    {
        Debug.Log("DefaultUpdate");
        if (Enemy.IsInSuspiciousRange)
        {
           Enemy.SwitchState(Enemy.SuspiciousState);
        }   

    }

}

