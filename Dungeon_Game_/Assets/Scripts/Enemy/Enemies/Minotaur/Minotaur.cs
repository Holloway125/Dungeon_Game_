using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : BaseEnemy
{

    public override IEnumerator Attack()
    {
return null;
    }

    public override void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        if(CurrentHealth <= 0)
        {
            Death();
        }
    }
}
