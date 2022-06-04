using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerResource : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int maxStamina;
    public int currentStamina;
    public Slider Energy;
    public Slider Stamina;
    public TMP_Text staminaText;
    public TMP_Text healthText;
    public Slider health;
    public HealthBar healthBar;

    void Start()
    {
        currentStamina = maxStamina;
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
        currentStamina -= damage;
        healthBar.SetHealth(currentHealth);
        health.value -= damage;
    }

    void FixedUpdate()
    {
        Stamina.value += .2f;
        staminaText.SetText($"{currentStamina.ToString()} / {maxStamina.ToString()}");
    }

}
