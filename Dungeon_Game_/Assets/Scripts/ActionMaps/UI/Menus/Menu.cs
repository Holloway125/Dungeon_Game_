using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    private PlayerActions playerControls;
    private PlayerInput playerInput;
    private InputAction characterStats;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject playerStats;
    //[SerializeField] private GameObject invPanel;
    [SerializeField] private GameObject _Map;
    private GameObject _player;

    public static bool GameIsPaused = false;
    private bool _MapIsOpen = false;
    private bool _statsOpen = false;

    private void Awake()
    {
        playerControls = new PlayerActions();
        playerInput = GetComponent<PlayerInput>();
        _player = GameObject.FindGameObjectWithTag("Player");
        playerStats = GameObject.Find("UI_Character Stats");
        characterStats = playerControls.UI.CharacterStatsToggle;
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(false);
        playerStats.SetActive(false);
        //invPanel.SetActive(false);
        _Map.SetActive(false);

    }

    private void OnEnable()
    {
        playerControls.UI.Enable();
        playerControls.UI.MenuToggle.performed += MenuToggle;
        playerControls.UI.MapToggle.performed += MapToggle;
        playerControls.UI.CharacterStatsToggle.performed += CharacterStatsToggle;
    }

    private void OnDisable()
    {
        playerControls.UI.Disable();
        playerControls.UI.MenuToggle.performed -= MenuToggle;
        playerControls.UI.MapToggle.performed -= MapToggle;
        playerControls.UI.CharacterStatsToggle.performed -= CharacterStatsToggle;
    }

    private void Resume()
    {
        settingsPanel.SetActive(false);
        pauseMenu.SetActive(false);
        // invPanel.SetActive(false);
        playerStats.SetActive(false);
        // logPanel.SetActive(false);
        Time.timeScale = 1f;
        _statsOpen = false;
        _MapIsOpen = false;
        GameIsPaused = false;
    }

    private void MenuToggle(InputAction.CallbackContext context)
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

    private void CharacterStatsToggle(InputAction.CallbackContext context)
    {
        Debug.Log("StatsToggleCalled");
        if(_statsOpen == true)
        {
            playerStats.SetActive(false);
            _statsOpen = false;
            Resume();
        }

        else if(_statsOpen == false)
        {
            playerStats.SetActive(true);
            _statsOpen = true;
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    }

    private void MapToggle(InputAction.CallbackContext context)
    {
        Debug.Log("MapToggleToggleCalled");
        if(_MapIsOpen == true)
        {
            _Map.SetActive(false);
            _MapIsOpen = false;
            Resume();
        }

        else if(_MapIsOpen == false)
        {
            _Map.SetActive(true);
            _MapIsOpen = true;
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
    //     playerStats.SetActive(true);
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
