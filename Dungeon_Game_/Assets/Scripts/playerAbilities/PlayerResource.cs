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
    public int maxHealth = 100;
    public int currentHealth;
    public Slider health;
    public TMP_Text healthText;

    //energy properties
    public Slider Energy;
    public TMP_Text energyText;

    //stamina properties
    public Slider Stamina;
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
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);  
        reqXP = 83;
        currentXP = 0;
        expBar.value = currentXP;
        expBar.maxValue = reqXP;
        levelText = 1;
        totalCurrentXP = 1;
        
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            TakeDamage(20);
        }
        if(Input.GetKeyDown(KeyCode.Equals))
        GainExperienceFlatRate(20);
        if (currentXP > reqXP)
        {    
            LevelUP();
        }
    }
    
    void FixedUpdate()
    {
        Stamina.value += .2f;
        int currentStamina = Mathf.RoundToInt(Stamina.value);
        staminaText.SetText($"{currentStamina.ToString()} / {Stamina.maxValue.ToString()}");
    }

    void TakeDamage(int damage)
    {
        if(currentHealth > damage)
        {
        currentHealth -= damage;
        SetHealth(currentHealth);
        healthText.SetText($"{currentHealth.ToString()} / {maxHealth.ToString()}");
        }
        else
        {
            Death();
        }
    }

    public void GainExperienceFlatRate( float xpGained)
    {
        totalCurrentXP+=20;  
        expBar.value = currentXP += xpGained; 

    }
    public void LevelUP()
    {
        levelText++;
        currentXP = Mathf.RoundToInt(currentXP - reqXP);
        playerLevel.SetText(levelText.ToString());
        reqXP = totalCurrentXP * 0.28571428571f;
        SetHealth(health.maxValue);
        currentHealth = maxHealth;
        healthText.SetText($"{currentHealth.ToString()} / {maxHealth.ToString()}");
        
    }

        public void SetMaxHealth(float Health)
    {
        health.maxValue = Health;
        health.value = Health;
    }

    public void SetHealth(float Health)
    {
        health.value = Health;
    }

    public void Death()
    {
        currentHealth = 0;
        SetHealth(currentHealth);
        healthText.SetText($"{currentHealth.ToString()} / {maxHealth.ToString()}");
        //play death animation
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _animator.SetTrigger("death");
        
        youLose.SetActive(true);



    }
    
    public void TimeStop()
    {
        Time.timeScale = 0;
        
    }
    
}
