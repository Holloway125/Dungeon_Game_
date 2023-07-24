// using UnityEngine;

// public class EnemyRetreating : BaseEnemyState
// {

// public override void EnterState(BaseEnemy Enemy)
    
//     {
//         Enemy.movement.maxSpeed = Enemy.Speed;
//         Enemy.Animate("Run");
//     }

// public override void UpdateState(BaseEnemy Enemy)
//     {
//         Enemy.IsInSpawn = Physics2D.OverlapCircle(Enemy.Home, 5);
//         Enemy.Animate("Run");
//         if(Enemy.IsInSpawn)
//         {
//             Enemy.SwitchState(Enemy.DefaultState);
//         }
//         else
//         {
//             Enemy.InvokeRetreat();
//             if (Enemy.CurrentHealth <= Enemy.EnemyMaxHealth)
//             {
//             Enemy.CurrentHealth += Mathf.Round(Enemy.EnemyMaxHealth * .05f);
//             }
            
//         }
//         // if (Enemy.CurrentHealth < 0)
//         // {
//         // Enemy.SwitchState(Enemy.DeathState);
//         // }

//     }


// }

