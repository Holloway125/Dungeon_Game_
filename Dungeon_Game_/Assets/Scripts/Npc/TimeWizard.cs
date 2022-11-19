using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWizard : BaseNPC
{
    protected override void Interact()
    {
        if(playerInRange == true)
        {
            dialogBox.SetActive(true);
            DataStorage._TimeLeft = 120f;
            _timer.timerIsRunning = true;
        }
        else
        {
            return;
        }
    }
}
