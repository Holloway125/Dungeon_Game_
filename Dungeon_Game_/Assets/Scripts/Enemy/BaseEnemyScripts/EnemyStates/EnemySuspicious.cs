using UnityEngine;

public class EnemySuspicious : BaseEnemyState
{
    GameObject NewTarget;
public override void EnterState(BaseEnemy Enemy)
    {
        Enemy.Aggroed = false;
        NewTarget = new GameObject("Suspect");
        NewTarget.transform.position = Enemy.Player.transform.position;
        Enemy.AIDestinationSetterScript.target = NewTarget.transform;
        Enemy.movement.maxSpeed = Enemy.Speed / 3;
        Debug.Log("I am Suspicious");
    }

public override void UpdateState(BaseEnemy Enemy)
    {
        if (Enemy.transform.position == NewTarget.transform.position && Enemy.IsInSuspiciousRange)
        {
            Enemy.DestroySuspect(NewTarget);
            NewTarget = new GameObject("Suspect");
            NewTarget.transform.position = Enemy.Player.transform.position;
            Enemy.AIDestinationSetterScript.target = NewTarget.transform;
        }
        else if (Enemy.transform.position == NewTarget.transform.position && !Enemy.IsInSuspiciousRange)
        {
            Enemy.DestroySuspect(NewTarget);
            Enemy.SwitchState(Enemy.RetreatingState);
        }
        else if (Enemy.LineOfSight && Enemy.IsInAggroRange)
        {
            Enemy.DestroySuspect(NewTarget);            
            Enemy.SwitchState(Enemy.ChasingState);
        }
    }

}
