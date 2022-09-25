using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 90;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public bool bossAlive = true;

    public Damage damage;

    void Awake()
    {

    }
    void Start()
    {
        timerIsRunning = false; 
    }

    void Update()
    {
        if (timerIsRunning == true && bossAlive == true)
        {
            if (timeRemaining > 0 && bossAlive == false)
            {
                YouWin();
            }

            else if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }

            else if (timeRemaining <= 0)
            {
                GameOver();
            }
            
        }
        
    }

    void DisplayTime(float timeToDisplay)
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
        damage.Death();
    }


}