using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class PlayerResource : MonoBehaviour
{
    //for death animation
    public Rigidbody2D rb;
    public Animator _animator;
    public GameObject youLose;

    //health properties
    public float maxHealth = 100;
    public float currentHealth;
    public Image healthSlider;
    public Text healthText;

    //stamina properties
    public Image staminaSlider;
    public Text staminaText;

    void Start()
    {
        SetMaxHealth(maxHealth);  
        currentHealth = maxHealth;
        healthText.text = ($"{maxHealth.ToString()}");      
    }

    void FixedUpdate()
    {
        staminaSlider.fillAmount += .002f;
        int currentStamina = Mathf.RoundToInt(staminaSlider.fillAmount*100);
        staminaText.text = ($"{currentStamina.ToString()}");
    }

    public void SetMaxHealth(float Health) //input max health property or new maxhealth fillAmount
    {
        maxHealth = Health;
        healthSlider.fillAmount = 1;
    }

    public void SetHealth(float Health) // input current health property for setting new health fillAmount after taking damage or put in maxHealth to set current health to max;
    {
        if (Health/maxHealth*100 > healthSlider.fillAmount)
        {
            healthSlider.fillAmount = healthSlider.fillAmount;
        }
        else
        {
        healthSlider.fillAmount = Health;
        }
    }
    
    public void TimeStop() // Needed for death Animation
    {
        Time.timeScale = 0f;
    } 
}
