using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UIElements;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set;}
    private UIDocument _doc;
    private VisualElement _pause;
    private VisualElement _stats;
    private VisualElement _map;
    private VisualElement _HUD;
    private GameObject scenemanager;
    private IMGUIContainer ExpFill;
    private IMGUIContainer HealthFill;
    private IMGUIContainer StaminaFill;
    private Label _healthValue;
    private Label _attackValue;
    private Label _defenseValue;
    private Label _attackSpeedValue;
    private Label _critValue;
    private Label _lvl;
    private Label _mapCoords;
    private VisualElement _playerChatBox;
    private VisualElement _npcChatBox;
    public Label _timer;
    private GameObject player;
    private GameObject _camera;
    private CameraController CameraController;
    private Menu Menu;

    private PlayerActions playerControls;
    private LevelSystem LevelSystem;
    private bool _statsOpen;
    private bool _pauseOpen;
    private bool _mapOpen = false;
    private bool _UIElementOpen;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        playerControls = new PlayerActions();
        player = GameObject.FindWithTag("Player");
        _camera = GameObject.FindWithTag("Camera");
        LevelSystem = player.GetComponent<LevelSystem>();
        CameraController = _camera.GetComponent<CameraController>();
        _doc = GetComponent<UIDocument>();
        _pause = _doc.rootVisualElement.Q("Pause");
        _stats = _doc.rootVisualElement.Q("PlayerStats");
        _map = _doc.rootVisualElement.Q("Map");
        _HUD = _doc.rootVisualElement.Q("HUD");
        scenemanager = GameObject.Find("SceneManager");
        Menu = scenemanager.GetComponent<Menu>();

        _healthValue = _doc.rootVisualElement.Q<Label>("HealthValue");
        HealthFill = _doc.rootVisualElement.Q<IMGUIContainer>("HealthFill");

        StaminaFill = _doc.rootVisualElement.Q<IMGUIContainer>("StamFill");

        ExpFill = _doc.rootVisualElement.Q<IMGUIContainer>("ExpFill");

        _mapCoords = _doc.rootVisualElement.Q<Label>("MapCoords");

        _timer = _doc.rootVisualElement.Q<Label>("TimeLeft");
        _playerChatBox = _doc.rootVisualElement.Q("PlayerChatBox");
        _npcChatBox = _doc.rootVisualElement.Q("NpcChatBox");

        _attackValue = _doc.rootVisualElement.Q<Label>("AttackValue");
        _defenseValue = _doc.rootVisualElement.Q<Label>("DefenseValue");
        _attackSpeedValue = _doc.rootVisualElement.Q<Label>("AttackSpeedValue");
        _critValue = _doc.rootVisualElement.Q<Label>("CritValue");
        _lvl = _doc.rootVisualElement.Q<Label>("Lvl");
    }

    private void Start()
    {
        UpdateValues();
        _statsOpen = false;
        _pauseOpen = false;
        _mapOpen = false;
        _UIElementOpen = false;
        _stats.style.display = DisplayStyle.None;
        _pause.style.display = DisplayStyle.None;
        _map.style.display = DisplayStyle.None;
    }
    private void OnEnable()
    {
        playerControls.UI.Enable();
        playerControls.UI.Map.performed += MapToggle;
        playerControls.UI.Stats.performed += StatsToggle;
        playerControls.UI.Pause.performed += PauseToggle;
    }
    private void OnDisable()
    {
        playerControls.UI.Disable();
        playerControls.UI.Map.performed -= MapToggle;
        playerControls.UI.Stats.performed -= StatsToggle;
        playerControls.UI.Pause.performed -= PauseToggle;
    }
    public void UpdateValues()
    {
        _lvl.text = LevelSystem.GetPlayerLvl().ToString();
        _healthValue.text = PlayerStats.GetMaxHP().ToString();
        _attackValue.text = PlayerStats.GetAttack().ToString();
        _defenseValue.text = PlayerStats.GetDefense().ToString();
        _attackSpeedValue.text = PlayerStats.GetAttackSpeed().ToString(PlayerStats.GetAttackSpeed()*100 + "%");
        _critValue.text = PlayerStats.GetCrit().ToString(PlayerStats.GetCrit()*100 + "%");
        ExpFill.style.width = Length.Percent((float)LevelSystem.GetTotalXp()/(float)LevelSystem.GetXpToNextLvl()*100);
    }
    public void NpcDialogue()
    {
        _npcChatBox.style.display = DisplayStyle.Flex;
    }

    public void UpdateStamBar()
    {
        StaminaFill.style.width = Length.Percent(PlayerStats.GetCurrentStam()/PlayerStats.GetMaxStam() * 100);
    }
    public void UpdateHealthBar()
    {
        HealthFill.style.width = Length.Percent(PlayerStats.GetCurrentHP()/PlayerStats.GetMaxHP() * 100);
    }
    public void UpdateExpBar()
    {
        ExpFill.style.width = Length.Percent((float)LevelSystem.GetTotalXp()/(float)LevelSystem.GetXpToNextLvl()*100);
    }
    public void UpdateMapCoords(double x, double y)
    {
        string x_coord;
        string y_coord;
        x_coord = Convert.ToInt32(x).ToString();
        y_coord = Convert.ToInt32(y).ToString();
        _mapCoords.text = x_coord+","+y_coord;
    }
    public void StatsToggle(InputAction.CallbackContext context)
    {

        if(_statsOpen == true && _UIElementOpen == true)
        {
            _stats.style.display = DisplayStyle.None;
            _statsOpen = false;
            _UIElementOpen = false;
            Menu.Resume();
            Debug.Log("Close Stats");
        }

        else if(_statsOpen == false && _UIElementOpen == false)
        {
            UpdateValues();
            _stats.style.display = DisplayStyle.Flex;
            _statsOpen = true;
            _UIElementOpen = true;
            Menu.Pause();
            Debug.Log("Open Stats");
        }

        else if(_statsOpen == true && _UIElementOpen == false)
        {
            return;
        }

        else if(_statsOpen == false && _UIElementOpen == true)
        {
            return;
        }
    }
    public void PauseToggle(InputAction.CallbackContext context)
    {
        if(_mapOpen == true && _UIElementOpen == true)
        {
            _map.style.display = DisplayStyle.None;
            _mapOpen = false;
            _UIElementOpen = false;
            Menu.Resume();
        }
        else if(_statsOpen == true && _UIElementOpen == true)
        {
            _stats.style.display = DisplayStyle.None;
            _statsOpen = false;
            _UIElementOpen = false;
            Menu.Resume();
        }
        else if(_pauseOpen == true && _UIElementOpen == true)
        {
            _pause.style.display = DisplayStyle.None;
            _pauseOpen = false;
            _UIElementOpen = false;
            Menu.Resume();
        }
        else if(_pauseOpen == false && _UIElementOpen == false)
        {
            _pause.style.display = DisplayStyle.Flex;
            _pauseOpen = true;
            _UIElementOpen = true;
            Menu.Pause();
        }

        else if(_pauseOpen == true && _UIElementOpen == false)
        {
            return;
        }

        else if(_pauseOpen == false && _UIElementOpen == true)
        {
            return;
        }
    }
    public void MapToggle(InputAction.CallbackContext context)
    {
        if(_mapOpen == true && _UIElementOpen == true)
        {
            _map.style.display = DisplayStyle.None;
            _mapOpen = false;
            _UIElementOpen = false;
            Menu.Resume();
        }

        else if(_mapOpen == false && _UIElementOpen == false)
        {
            _map.style.display = DisplayStyle.Flex;
            _mapOpen = true;
            _UIElementOpen = true;
            Menu.Pause();
        }

        else if (_mapOpen == true && _UIElementOpen == false)
        {
            return;
        }

        else if (_mapOpen == false && _UIElementOpen == true)
        {
            return;
        }
    }
}
