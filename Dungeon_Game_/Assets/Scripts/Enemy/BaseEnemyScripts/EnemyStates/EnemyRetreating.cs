using UnityEngine;

public class EnemyRetreating : BaseEnemyState
{

public override void EnterState(BaseEnemy Enemy)
    
    {
        Enemy.movement.maxSpeed = Enemy.Speed;
        
        if (!Enemy.IsInAggroRange)
        Enemy.InvokeRetreat();
        Debug.Log("I am Retreating");
        
    }

public override void UpdateState(BaseEnemy Enemy)
    {
                Debug.Log("RetreatingUpdate");
        if(Enemy.IsInSpawn)
        {
            Enemy.SwitchState(Enemy.DefaultState);
        }
        else if (Enemy.IsInAggroRange && Enemy.LineOfSight)
        {
            Enemy.SwitchState(Enemy.ChasingState);
        }

    }


}

