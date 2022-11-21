using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Menu : MonoBehaviour
{

    private PlayerActions _playerActions;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsPanel;
    //[SerializeField] private GameObject charPanel;
    //[SerializeField] private GameObject invPanel;
    [SerializeField] private GameObject _map;
    private GameObject _player;

    public static bool GameIsPaused = false;
    private bool _mapIsOpen = false;

    private void Awake()
    {
        _playerActions = new PlayerActions();
        _player = GameObject.FindGameObjectWithTag("Player");
        _map = GameObject.Find("/PlayerUI/Map");
    }

    private void Start()
    {
        _playerActions.UI.Menu.performed += context => PauseMenu();
        _playerActions.UI.Map.performed += context => MapOpenandClose();
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(false);
        //charPanel.SetActive(false);
        //invPanel.SetActive(false);
        _map.SetActive(false);

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
        if(_mapIsOpen == true)
        {
            _map.SetActive(false);
            _mapIsOpen = false;
            Resume();
        }

        else if(_mapIsOpen == false)
        {
            _map.SetActive(true);
            _mapIsOpen = true;
            Time.timeScale = 0f;
            GameIsPaused = true;
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
