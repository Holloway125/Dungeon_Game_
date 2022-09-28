using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstBoss : BaseEnemy
{
    public override void Death()
    {

        timer.YouWin();

    }        
}
