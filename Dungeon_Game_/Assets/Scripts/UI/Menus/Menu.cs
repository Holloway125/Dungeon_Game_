using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    private PlayerActions playerControls;
    private GameObject _player;

    public static bool GameIsPaused = false;

    private void Awake()
    {
        playerControls = new PlayerActions();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
