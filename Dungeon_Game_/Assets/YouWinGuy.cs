using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWinGuy : BaseNPC
{
    public float timerStop;
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
            timerStop = _timer.timeRemaining;
            SceneManager.LoadScene("YouWin");
            _timer.timeRemaining = timerStop;
        }
        else
        {
            return;
        }    
    }
}
