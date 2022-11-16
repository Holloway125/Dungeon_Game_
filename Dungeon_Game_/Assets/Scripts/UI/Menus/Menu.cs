using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Menu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    private GameObject _player;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject charPanel;
    [SerializeField] private GameObject invPanel;
    [SerializeField] private GameObject _map;

    private PlayerActions _playerActions;

    private void Awake()
    {
        _playerActions = new PlayerActions();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        _playerActions.UI.Menu.performed += context => PauseMenu();
        _playerActions.UI.Map.performed += context => MapOpenandClose();
    }

    private void OnEnable()
    {
        _playerActions.UI.Enable();
    }

    private void OnDisable()
    {
        _playerActions.UI.Disable();
    }

    private void Resume()
    {
        settingsPanel.SetActive(false);
        pauseMenu.SetActive(false);
        // invPanel.SetActive(false);
        // charPanel.SetActive(false);
        // logPanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void PauseMenu()
    {  
        if(GameIsPaused == false)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        else if(GameIsPaused == true)
        {
            Resume();
        }
    }

    private void MapOpenandClose()
    {
        if(_map == false)
        {
            _map.SetActive(true);
        }

        else if(_map == true)
        {
            _map.SetActive(false);
        }
    }

    //LEGACY CODE FOR REFERENCE WHEN NEEDED

    // public void PauseInv()
    // {
    //     invPanel.SetActive(true);
    //     Time.timeScale = 0f;
    //     GameIsPaused = true;
    // }

    // public void PauseChar()
    // {
    //     charPanel.SetActive(true);
    //     Time.timeScale = 0f;
    //     GameIsPaused = true;
    // }


    // public void PauseLog()
    // {
    //     logPanel.SetActive(true);
    //     Time.timeScale = 0f;
    //     GameIsPaused = true;
    // }

    // void Update()
    // {

    //     if(Input.GetKeyDown(KeyCode.Tab))
    //     {
    //         if (GameIsPaused)
    //         {
    //             Resume();  
    //         }

    //         else
    //         {
    //             PauseInv();
    //         }
    //     }

    //     if(Input.GetKeyDown(KeyCode.C))
    //     {
    //         if(GameIsPaused)
    //         {
    //             Resume();
    //         }

    //         else
    //         {
    //             PauseChar();
    //         }
    //     }

    //     if(Input.GetKeyDown(KeyCode.L))
    //     {
    //         if (GameIsPaused)
    //         {
    //             Resume();
    //         }

    //         else
    //         {
    //             PauseLog();
    //         }
    //     }

    //     if(Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         if (GameIsPaused)
    //         {
    //             Resume();  
    //         }
            
    //         else
    //         {
    //             PauseMenu();
    //         }
    //     }
    // }

}
