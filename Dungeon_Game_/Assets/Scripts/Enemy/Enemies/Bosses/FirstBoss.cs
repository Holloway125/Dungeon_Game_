using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/* 
    What this script needs to do
- Conditional function
- Looping Attack function
- Event Triggers

    What this script needs to store
- Boss Stats
- parameters for trigger events

*/
public class RivalBoss : MonoBehaviour
{
public float BossHealthMax;
public float BossHealth;
public float Damage;

    private void Start()
    {
        WhatToDo();
    }
    public void WhatToDo()
    {
        if(BossHealth >= BossHealthMax * 0.7)
        {
            Event(1);
        }
        else if (BossHealth < BossHealthMax * 0.7 && BossHealth >= BossHealthMax * 0.4)
        {
            Event(whichEvent: 2);
        }
        else if (BossHealth < BossHealthMax * 0.4 && BossHealth > 0)
        {
            Event(3);
        }
        else 
        {
            Event(4);
        }
        return;
    }
    public void BossAttack()
    {
        return;
    }

    public void Event(int whichEvent)
    {
        
    }


}

