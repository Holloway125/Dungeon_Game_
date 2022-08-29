using UnityEngine;

public class EnemyRetreating : BaseEnemyState
{

public override void EnterState(BaseEnemy Enemy)
    
    {
        Enemy.movement.maxSpeed = Enemy.Speed;
    }

public override void UpdateState(BaseEnemy Enemy)
    {
                Debug.Log("RetreatingUpdate");
        if(Enemy.IsInSpawn)
        {
            Enemy.SwitchState(Enemy.DefaultState);
        }
        else
        {
            Enemy.InvokeRetreat();
            if (Enemy.CurrentHealth <= Enemy.EnemyMaxHealth)
            {
            Enemy.CurrentHealth += Mathf.Round(Enemy.EnemyMaxHealth * .05f);
            }
            
        }

    }


}

