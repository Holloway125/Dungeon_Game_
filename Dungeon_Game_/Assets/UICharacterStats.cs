using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

public class UICharacterStats : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CharacterStats playerStats;

    [SerializeField] private UIDocument _doc;

    [SerializeField] private Label Health;
    [SerializeField] private Label Attack;
    [SerializeField] private Label Defense;
    [SerializeField] private Label AttackSpeed;
    [SerializeField] private Label Crit;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<CharacterStats>();
        _doc = GetComponent<UIDocument>();
        Health = _doc.rootVisualElement.Q<Label>("HealthValue");
        Attack = _doc.rootVisualElement.Q<Label>("AttackValue");
        Defense = _doc.rootVisualElement.Q<Label>("DefenseValue");
        AttackSpeed = _doc.rootVisualElement.Q<Label>("AttackSpeedValue");
        Crit = _doc.rootVisualElement.Q<Label>("CritValue");
    }

    void Start()
    {
        UpdateValues();
    }

    public void UpdateValues()
    {

    Attack.text = playerStats.GetAttack().ToString();
    Health.text = playerStats.GetMaxHP().ToString();
    AttackSpeed.text = playerStats.GetAttackSpeed().ToString();
    Crit.text = playerStats.GetCrit().ToString();
    Defense.text = playerStats.GetDefense().ToString();

    }

}
