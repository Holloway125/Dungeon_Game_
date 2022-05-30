using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Menu : MonoBehaviour
{

public static bool GameIsPaused = false;

public GameObject pauseMenu;
public GameObject settingsPanel;
public GameObject invPanel;
public GameObject charPanel;
public GameObject talentPanel;
public GameObject abilitiesPanel;
public GameObject logPanel;

void Update()
{
    if(Input.GetKeyDown(KeyCode.Escape))
    {
        if (GameIsPaused)
        {
            Resume();
        }else
        {
            PauseMenu();
        }
    }

        if(Input.GetKeyDown(KeyCode.Tab))
    {
        if (GameIsPaused)
        {

            Resume();
            
        }else
        {
            PauseInv();
        }
    }

            if(Input.GetKeyDown(KeyCode.C))
    {
        if (GameIsPaused)
        {

            Resume();
        }else
        {
            PauseChar();
        }
    }

            if(Input.GetKeyDown(KeyCode.N))
    {
        if (GameIsPaused)
        {

            Resume();
        }else
        {
            PauseTalent();
        }
    }

            if(Input.GetKeyDown(KeyCode.P))
    {
        if (GameIsPaused)
        {

            Resume();
        }else
        {
            PauseAbility();
        }
    }

            if(Input.GetKeyDown(KeyCode.L))
    {
        if (GameIsPaused)
        {

            Resume();
        }else
        {
            PauseLog();
        }
    }
}

void Resume()
{
    settingsPanel.SetActive(false);
    pauseMenu.SetActive(false);
    invPanel.SetActive(false);
    charPanel.SetActive(false);
    talentPanel.SetActive(false);
    abilitiesPanel.SetActive(false);
    logPanel.SetActive(false);
    Time.timeScale = 1f;
    GameIsPaused = false;

}

void PauseMenu()
{
    
    pauseMenu.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
}

void PauseInv()
{
    
    invPanel.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
}

void PauseChar()
{
    
    charPanel.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
}

void PauseTalent()
{
    
    talentPanel.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
}

void PauseAbility()
{
    
    abilitiesPanel.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
}

void PauseLog()
{
    
    logPanel.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
}

}
