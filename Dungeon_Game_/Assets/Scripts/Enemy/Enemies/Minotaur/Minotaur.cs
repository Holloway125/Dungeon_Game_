using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : BaseEnemy
{
    public Timer timer;
    public override IEnumerator Attack()
    {
return null;
    }

    public override void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        if(CurrentHealth <= 0)
        {
            timer.bossAlive = false;
            Death();
        }
    }
}
