using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    public Slider healthSlider;
    public TMP_Text healthText;

    //energy properties
    public Slider Energy;
    public TMP_Text energyText;

    //stamina properties
    public Slider staminaSlider;
    public TMP_Text staminaText;

    //level system properties
    public int level;
    public float totalCurrentXP;
    public float currentXP;
    public float reqXP;
    public TMP_Text playerLevel;
    public Slider expBar;
    public int levelText;

    void Start()
    {
        SetMaxHealth(maxHealth);  
        currentHealth = maxHealth;
        healthText.SetText($"{currentHealth.ToString()} / {maxHealth.ToString()}");
        reqXP = 83;
        currentXP = 0;
        expBar.value = currentXP;
        expBar.maxValue = reqXP;
        levelText = 1;
        totalCurrentXP = 1;
        
        
    }

    //  void Update()
    //  {
    //      if (Input.GetKeyDown(KeyCode.Keypad1))
    //      {
    //          TakeDamage(20);
    //      }
    //      if(Input.GetKeyDown(KeyCode.Equals))
    //     GainExperienceFlatRate(20);
    //     if (currentXP > reqXP)
    //     {    
    //         LevelUP();
    //     }
    //  }
    
    void FixedUpdate()
    {
        staminaSlider.value += .2f;
        int currentStamina = Mathf.RoundToInt(staminaSlider.value);
        staminaText.SetText($"{currentStamina.ToString()} / {staminaSlider.maxValue.ToString()}");
    }


    public void GainExperienceFlatRate( float xpGained)
    {
        totalCurrentXP+=20;  
        expBar.value = currentXP += xpGained; 

    }
    public void LevelUP() // sets health to max health and levels the character rolls left over exp over to next level progression
    {
        levelText++;
        currentXP = Mathf.RoundToInt(currentXP - reqXP);
        playerLevel.SetText(levelText.ToString());
        reqXP = totalCurrentXP * 0.28571428571f;
        SetHealth(healthSlider.maxValue);
        currentHealth = maxHealth;
        healthText.SetText($"{currentHealth.ToString()} / {maxHealth.ToString()}");
        
    }

        public void SetMaxHealth(float Health) //input max health property or new maxhealth value
    {
        maxHealth = Health;
        healthSlider.maxValue = Health;
        healthSlider.value = Health;
    }

    public void SetHealth(float Health) // input current health property for setting new health value after taking damage or put in maxHealth to set current health to max;
    {
        if (Health > healthSlider.maxValue)
        {
            healthSlider.value = healthSlider.maxValue;
        }
        else
        {
        healthSlider.value = Health;
        }
    }
    
}
