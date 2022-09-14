using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    enum EnemyAttacks
    {
        attack,
        ability,
        ability2
    }
public class EnemyAttacking : BaseEnemyState
{
        EnemyAttacks Action = EnemyAttacks.attack;

        
public override void EnterState(BaseEnemy Enemy)
    {
        Enemy.Aggroed = true;
        Enemy.AIDestinationSetterScript.target = Enemy.Player.transform.position;
        Enemy.movement.maxSpeed = Enemy.Speed;

    }

public override void UpdateState(BaseEnemy Enemy)
    {
        switch(Action)
        {
            case EnemyAttacks.attack:
                if (Enemy.IsInAttackRange && Enemy.LineOfSight)
                {
                    Enemy.AttackAnimate();
                }
                if (Enemy.HasAbility && Enemy.Ability.IsInAbilityRange && Enemy.Ability.state == EnemyAbilityHolder.EnemyAbilityState.ready)
                {
                    Action = EnemyAttacks.ability;
                }
                if (Enemy.HasAbility2 && Enemy.Ability2.IsInAbilityRange && Enemy.Ability2.state == EnemyAbilityHolder.EnemyAbilityState.ready && Enemy.Ability.state == EnemyAbilityHolder.EnemyAbilityState.cooldown)
                {
                    Action = EnemyAttacks.ability2;
                }
                break;

            case EnemyAttacks.ability:
                if(Enemy.LineOfSight)
                {
                    Enemy.Animate("Ability");
                }
                else if (Enemy.LineOfSight != true)
                {
                    Enemy.AIDestinationSetterScript.target = Enemy.Player.transform.position;
                }
                
                break;

            case EnemyAttacks.ability2:
                if(Enemy.LineOfSight)
                {
                    Enemy.Animate("Ability2");                    
                }
                else if (Enemy.LineOfSight != true)
                {
                    Enemy.AIDestinationSetterScript.target = Enemy.Player.transform.position;
                }
                
                break;

        }
    }

}

        // switch (state)
        // {
        //     case EnemyAbilityState.ready:
        //        if (IsInAbilityRange && BaseEnemyScript.IsInAggroRange)
        //     {
        //         Ability.Activate(gameObject);
        //         state = EnemyAbilityState.active;
        //         _ActiveTime = Ability.activeTime;

        //     }

        //     break;



        // if (Enemy.IsInAttackRange && Enemy.HasAnAttack)
        // {
        //     Enemy.AttackAnimate();
        // }
        // else 
        // {
        //     Enemy.SwitchState(Enemy.ChasingState);
        // }
        // if (Enemy.CurrentHealth < 0)
        // {
        // Enemy.SwitchState(Enemy.DeathState);
        // }