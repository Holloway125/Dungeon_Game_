using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeRemaining;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public bool bossAlive = true;

    PlayerResource PlayerResource;
    GameObject Player;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        //PlayerResource = Player.GetComponent<PlayerResource>(); 
    }

    private void Start()
    {
        timerIsRunning = true;
        bossAlive = true;
    }

    private void Update()
    {
        if (timerIsRunning == true && bossAlive == true)
        {
             if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }

            else if (timeRemaining <= 0)
            {
                GameOver();
            }
            
        }

        else if (timerIsRunning == true && bossAlive == false)
        {
            YouWin();
        }
        
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void PauseTimer()
    {
        timerIsRunning = false;
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }

    public void YouWin()
    {
        SceneManager.LoadScene("LaunchMenu");
    }

    public void GameOver()
    {
        PlayerResource.Death();
    }


}