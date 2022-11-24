using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWinGuy : BaseNPC
{
    //GameObject _youWin;

    // protected override void Start()
    // {
    //     base.Start();
    // }

    protected override void Interact()
    {
        if(playerInRange == true)
        {
            _timer.PauseTimer();
            SceneManager.LoadScene("YouWin");
        }
        else
        {
            return;
        }    
    }
}
