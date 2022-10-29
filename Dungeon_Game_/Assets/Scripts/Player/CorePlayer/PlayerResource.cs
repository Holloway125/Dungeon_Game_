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
        currentHealth = maxHealth;
        healthText.text = ($"{maxHealth.ToString()}");      
    }

    void FixedUpdate()
    {
        staminaSlider.fillAmount += .002f;
        int currentStamina = Mathf.RoundToInt(staminaSlider.fillAmount*100);
        staminaText.text = ($"{currentStamina.ToString()}");
    }
    
    public void TimeStop() // Needed for death Animation
    {
        Time.timeScale = 0f;
    }

    public void TakeDamage(float damage) // input the amount of damage you want the player to take on each monster
    {
        if(currentHealth > damage)
        {
        currentHealth -= damage;
        healthText.text = ($"{Mathf.RoundToInt(currentHealth).ToString()}");
        }
        else
        {
            Death();
        }
    }

    public void Death() //sets current health to 0 plays death animation puts up return to title canvas and stops all movement
    {
        currentHealth = 0;
        healthText.text = ($"{currentHealth.ToString()}");
        //play death animation
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _animator.SetTrigger("death");
        youLose.SetActive(true);
    
    }
 
}
