using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Menu : MonoBehaviour
{

public static bool GameIsPaused = false;

public GameObject pauseMenu;
public GameObject settingsPanel;

void Update()
{
    if(Input.GetKeyDown(KeyCode.Escape))
    {
        if (GameIsPaused)
        {
            Resume();
        }else
        {
            Pause();
        }
    }
}

void Resume()
{
    settingsPanel.SetActive(false);
    pauseMenu.SetActive(false);
    Time.timeScale = 1f;
    GameIsPaused = false;

}

void Pause()
{
    
    pauseMenu.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
}
}
