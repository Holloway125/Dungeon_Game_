using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using MyUILibrary;

public class UICharacterStats : MonoBehaviour
{
    [SerializeField] private UIDocument _doc;
    private VisualElement _pause;
    private VisualElement _stats;
    private VisualElement _map;
    private VisualElement _HUD;
    private GameObject scenemanager;
    [SerializeField] private Label HealthValue;
    [SerializeField] private Label AttackValue;
    [SerializeField] private Label DefenseValue;
    [SerializeField] private Label AttackSpeedValue;
    [SerializeField] private Label CritValue;
    [SerializeField] private Label Lvl;
    [SerializeField] private RadialProgress ExpBar;

    [SerializeField] private IMGUIContainer HealthFill;
    [SerializeField] private IMGUIContainer StaminaFill;
    [SerializeField] private GameObject player;
    [SerializeField] private CharacterStats playerStats;
    [SerializeField] private Menu Menu;

    private PlayerActions playerControls;
    private LevelSystem LevelSystem;
    private bool _statsOpen;
    private bool _pauseOpen;
    private bool _mapOpen = false;
    private bool _UIElementOpen;

    // private PlayerInput playerInput;
    // private InputAction stats;
    // private InputAction pause;
    // private InputAction map;
    
    private void OnEnable()
    {
        playerControls.UI.Enable();
        // playerControls.UI.MenuToggle.performed += MenuToggle;
        playerControls.UI.Map.performed += MapToggle;
        playerControls.UI.Stats.performed += StatsToggle;
        playerControls.UI.Pause.performed += PauseToggle;
    }

    private void OnDisable()
    {
        playerControls.UI.Disable();
        // playerControls.UI.MenuToggle.performed -= MenuToggle;
        playerControls.UI.Map.performed -= MapToggle;
        playerControls.UI.Stats.performed -= StatsToggle;
        playerControls.UI.Pause.performed -= PauseToggle;
    }

    private void Awake()
    {
        playerControls = new PlayerActions();
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<CharacterStats>();
        LevelSystem = player.GetComponent<LevelSystem>();
        _doc = GetComponent<UIDocument>();
        _pause = _doc.rootVisualElement.Q("Pause");
        _stats = _doc.rootVisualElement.Q("CharacterStats");
        _map = _doc.rootVisualElement.Q("Map");
        _HUD = _doc.rootVisualElement.Q("HUD");
        scenemanager = GameObject.Find("SceneManager");
        Menu = scenemanager.GetComponent<Menu>();

        HealthValue = _doc.rootVisualElement.Q<Label>("HealthValue");
        HealthFill = _doc.rootVisualElement.Q<IMGUIContainer>("HealthFill");

        StaminaFill = _doc.rootVisualElement.Q<IMGUIContainer>("StamFill");

        AttackValue = _doc.rootVisualElement.Q<Label>("AttackValue");
        DefenseValue = _doc.rootVisualElement.Q<Label>("DefenseValue");
        AttackSpeedValue = _doc.rootVisualElement.Q<Label>("AttackSpeedValue");
        CritValue = _doc.rootVisualElement.Q<Label>("CritValue");
        Lvl = _doc.rootVisualElement.Q<Label>("Lvl");
    }

        // playerInput = player.GetComponent<PlayerInput>();
        // pause = playerInput.actions["Pause"];
        // map = playerInput.actions["Map"];
        // stats = playerInput.actions["Stats"];

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

    private void Update()
    {
    HealthFill.style.width = Length.Percent(playerStats.GetCurrentHP()/playerStats.GetMaxHP() * 100);
    StaminaFill.style.width = Length.Percent(playerStats.GetCurrentStam()/playerStats.GetMaxStam() * 100);
    }

    private void StatsToggle(InputAction.CallbackContext context)
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

    public void UpdateValues()
    {
    ExpBar.progress = (float)LevelSystem.GetXpToNextLvl() / (float)LevelSystem.GetTotalXp();
    Lvl.text = LevelSystem.GetPlayerLvl().ToString();
    HealthValue.text = playerStats.GetMaxHP().ToString();
    AttackValue.text = playerStats.GetAttack().ToString();
    DefenseValue.text = playerStats.GetDefense().ToString();
    AttackSpeedValue.text = playerStats.GetAttackSpeed().ToString(playerStats.GetAttackSpeed()*100 + "%");
    CritValue.text = playerStats.GetCrit().ToString(playerStats.GetCrit()*100 + "%");
    }

    private void PauseToggle(InputAction.CallbackContext context)
    {
        if(_pauseOpen == true && _UIElementOpen == true)
        {
            _pause.style.display = DisplayStyle.None;
            _pauseOpen = false;
            _UIElementOpen = false;
            Menu.Resume();
            Debug.Log("Close Stats");
        }

        else if(_pauseOpen == false && _UIElementOpen == false)
        {
            _pause.style.display = DisplayStyle.Flex;
            _pauseOpen = true;
            _UIElementOpen = true;
            Menu.Pause();
            Debug.Log("Open Stats");
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

        private void MapToggle(InputAction.CallbackContext context)
    {
        if(_mapOpen == true && _UIElementOpen == true)
        {
            _map.style.display = DisplayStyle.None;
            _mapOpen = false;
            _UIElementOpen = false;
            Menu.Resume();
            Debug.Log("Close Map");
        }

        else if(_mapOpen == false && _UIElementOpen == false)
        {
            _map.style.display = DisplayStyle.Flex;
            _mapOpen = true;
            _UIElementOpen = true;
            Menu.Pause();
            Debug.Log("Open Map");
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
