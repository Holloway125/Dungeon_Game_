using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMob : BaseEnemy
{

    void BossDeath()
    {
        if(CurrentHealth <= 0)
        {
            timer.bossAlive = false;
        }
    }

}
