using UnityEngine;

public class EnemyChasing : BaseEnemyState
{
public override void EnterState(BaseEnemy Enemy)
    {
        Enemy.Aggroed = true;
        Enemy.AIDestinationSetterScript.target = Enemy.Player.transform.position;
        Enemy.movement.maxSpeed = Enemy.Speed;
        Enemy.Anim.Play("RunBlend");
    }

public override void UpdateState(BaseEnemy Enemy)
    {
        Debug.Log("ChasingUpdate");
        if (Enemy.DistanceFromHome > Enemy.Leash)
        {
            Enemy.SwitchState(Enemy.RetreatingState);
        }
        if (Enemy.IsInChaseRange)
        {
            Enemy.Animate();
            Enemy.AIDestinationSetterScript.target = Enemy.Player.transform.position;
        }
        
        if (Enemy.IsInAttackRange && Enemy.LineOfSight && Enemy.HasAnAttack)
        {
            Enemy.SwitchState(Enemy.AttackingState);
        }
        
        if (!Enemy.IsInChaseRange)
        {
            Enemy.Aggroed = false;
            Enemy.SwitchState(Enemy.RetreatingState);
        }
        if (Enemy.CurrentHealth < 0)
        {
            Enemy.SwitchState(Enemy.DeathState);
        }
    }

}
