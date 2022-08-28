using UnityEngine;

public class EnemyRetreating : BaseEnemyState
{

public override void EnterState(BaseEnemy Enemy)
    
    {
        Enemy.movement.maxSpeed = Enemy.Speed;
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
    }


}

