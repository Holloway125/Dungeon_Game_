using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerResource : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public Slider health;
    public TMP_Text healthText;
    public Slider Energy;
    public TMP_Text energyText;
    public Slider Stamina;
    public TMP_Text staminaText;
    
    
    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            TakeDamage(20);
        }
    }


    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        healthText.SetText($"{currentHealth.ToString()} / {maxHealth.ToString()}");
    }

    void FixedUpdate()
    {
        Stamina.value += .2f;
        int currentStamina = Mathf.RoundToInt(Stamina.value);
        staminaText.SetText($"{currentStamina.ToString()} / {Stamina.maxValue.ToString()}");
    }

}
