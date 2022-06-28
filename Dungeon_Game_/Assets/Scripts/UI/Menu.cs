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

public void Resume()
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

void OnMouseOver()
{
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
}

void PauseMenu()
{
    
    pauseMenu.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
}

public void PauseInv()
{
    
    invPanel.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
}

public void PauseChar()
{
    
    charPanel.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
}

public void PauseTalent()
{
    
    talentPanel.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
}

public void PauseAbility()
{
    
    abilitiesPanel.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
}

public void PauseLog()
{
    
    logPanel.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
}

}
