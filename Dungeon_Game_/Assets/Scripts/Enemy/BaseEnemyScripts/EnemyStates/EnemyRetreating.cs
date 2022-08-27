using UnityEngine;

public class EnemyRetreating : BaseEnemyState
{

public override void EnterState(BaseEnemy Enemy)
    
    {
        Enemy.Aggroed = false;
        Enemy.InvokeRetreat();
        Enemy.movement.maxSpeed = Enemy.Speed / 2;
        Debug.Log("I lost them");
    }

public override void UpdateState(BaseEnemy Enemy)
    {
        if (Enemy.IsInSuspiciousRange)
        {
            Enemy.SwitchState(Enemy.SuspiciousState);
        }
        else if(Enemy.transform.position == Enemy.Respawn.transform.position && !Enemy.IsInSuspiciousRange)
        {
            Enemy.SwitchState(Enemy.DefaultState);
        }
    }


}

