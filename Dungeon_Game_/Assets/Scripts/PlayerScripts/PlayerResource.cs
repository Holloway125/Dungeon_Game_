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

    //level properties
    public int playerXp;//current running total
    public int totalXp;//total xp needed to lvl
    public int playerLvl;
    public TMP_Text lvlText;
    public Slider expBar;

    void Start()
    {
        SetMaxHealth(maxHealth);  
        currentHealth = maxHealth;
        healthText.SetText($"{currentHealth.ToString()} / {maxHealth.ToString()}");
        totalXp = 100; //exp needed to get to lvl 2
        playerXp = 0;
        expBar.value = playerXp;
        expBar.maxValue = totalXp;
        playerLvl = 1;               
    }

    void Update()
      {     
        /* 
            if (Input.GetKeyDown(KeyCode.Keypad1))
          {
              TakeDamage(20);
          }
        */
            if(Input.GetKeyDown(KeyCode.Equals))
            {
            GainExperienceFlatRate(250);
                if (playerXp >= totalXp)
                {    
                LevelUP();
                }
            }
      }

    void FixedUpdate()
    {
        staminaSlider.value += .2f;
        int currentStamina = Mathf.RoundToInt(staminaSlider.value);
        staminaText.SetText($"{currentStamina.ToString()} / {staminaSlider.maxValue.ToString()}");
    }

    public void GainExperienceFlatRate(int xpGained)
    {
        playerXp += xpGained;
        expBar.value += xpGained;
    }

    public void LevelUP() // sets health to max health and levels the character rolls left over exp over to next level progression
    {
        playerLvl++;
        lvlText.SetText(playerLvl.ToString());
        int oldTotal = totalXp; // holds previous lvls totalxp value
        totalXp = totalXp * 2; //exp curve change later

        if(playerXp>=totalXp)//levels up until playerXp is less than totalxp
        {
            LevelUP();
        }
        else
        {
        expBar.maxValue = totalXp - oldTotal;
        expBar.value = playerXp - oldTotal;
        }

        SetHealth(healthSlider.maxValue); //Sets health to full after lvl up
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
    
    public void TimeStop() // Needed for death Animation
    {
        Time.timeScale = 0f;
    }
    

    
}
