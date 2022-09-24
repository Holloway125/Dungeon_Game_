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
}
