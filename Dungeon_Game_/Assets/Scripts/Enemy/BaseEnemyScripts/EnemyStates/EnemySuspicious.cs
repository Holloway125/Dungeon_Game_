using UnityEngine;

public class EnemySuspicious : BaseEnemyState
{
Vector3 NewTarget;
public override void EnterState(BaseEnemy Enemy)
    {
        Enemy.Anim.Play("Run_Slime");
        Enemy.movement.maxSpeed = Enemy.Speed / 3;
        if (Enemy.IsInAggroRange && Enemy.LineOfSight)
        {
            Enemy.SwitchState(Enemy.ChasingState);
        }

    }

public override void UpdateState(BaseEnemy Enemy)
    {
        Debug.Log("SuspiciousUpdate");
        if (Enemy.DistanceFromHome > Enemy.Leash)
        {
            Enemy.SwitchState(Enemy.RetreatingState);
        }
        if (Vector3.Distance(Enemy.transform.position, Enemy.AIDestinationSetterScript.target) <= .01f && Enemy.IsInSuspiciousRange)
            {           
            Vector3 RandomPoint = Random.insideUnitCircle * 5;
            RandomPoint.z = 0;
            NewTarget = Enemy.Player.transform.position + RandomPoint;
            Enemy.AIDestinationSetterScript.target = NewTarget;
            Enemy.Anim.Play("Run_Slime");
            }
        
        else if (Enemy.IsInAggroRange && Enemy.LineOfSight)
        {
            Enemy.SwitchState(Enemy.ChasingState);
        }
        else if (!Enemy.IsInSuspiciousRange && Vector3.Distance(Enemy.transform.position, Enemy.AIDestinationSetterScript.target) <= .01f)
        {
            Enemy.SwitchState(Enemy.RetreatingState);
        }
    }

}
