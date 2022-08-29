using UnityEngine;

public class EnemySuspicious : BaseEnemyState
{
Vector3 NewTarget;
public override void EnterState(BaseEnemy Enemy)
    {
            Enemy.movement.maxSpeed = Enemy.Speed / 3;
        if (Enemy.IsInAggroRange && Enemy.LineOfSight)
        {
            Enemy.SwitchState(Enemy.ChasingState);
        }

    }

public override void UpdateState(BaseEnemy Enemy)
    {
        Debug.Log("SuspiciousUpdate");
        if (Vector3.Distance(Enemy.Home, Enemy.transform.position)> Enemy.Leash)
        {
            Enemy.SwitchState(Enemy.RetreatingState);
        }
        if (Vector3.Distance(Enemy.transform.position, Enemy.AIDestinationSetterScript.target) <= 0.01 && Enemy.IsInSuspiciousRange)
            {           
            Vector3 RandomPoint = Random.insideUnitCircle * 5;
            RandomPoint.z = 0;
            NewTarget = Enemy.Player.transform.position + RandomPoint;
            Enemy.AIDestinationSetterScript.target = NewTarget;
            Debug.Log("I am still Suspicious");
            }
        
        else if (Enemy.IsInAggroRange && Enemy.LineOfSight)
        {
            Enemy.SwitchState(Enemy.ChasingState);
        }
        else if (!Enemy.IsInSuspiciousRange && Vector3.Distance(Enemy.transform.position, Enemy.AIDestinationSetterScript.target) <= 0.01)
        {
            Enemy.SwitchState(Enemy.RetreatingState);
        }
    }

}
