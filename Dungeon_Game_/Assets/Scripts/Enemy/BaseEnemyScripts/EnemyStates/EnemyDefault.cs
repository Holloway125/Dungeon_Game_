using UnityEngine;

public class EnemyDefault : BaseEnemyState
{
public override void EnterState(BaseEnemy Enemy)
    {
    Debug.Log("Hello im default");
    }

public override void UpdateState(BaseEnemy Enemy)
    {
         Enemy.IsInSuspiciousRange = Physics2D.OverlapCircle(Enemy.transform.position, Enemy.SuspiciousRadius, Enemy.WhatIsPlayer);
         if (Enemy.IsInSuspiciousRange)
         {
            Enemy.SwitchState(Enemy.SuspiciousState);
         }
         
    }

}

