using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class PlayerResource : MonoBehaviour
{
    //for death animation
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator _animator;

    //UI_Elements
    private GameObject _UI;
    public GameObject _youLose;

    //health properties
    public float maxHealth = 100;
    public float currentHealth;
    public Image healthSlider;
    public Text healthText;

    //stamina properties
    public Image staminaSlider;
    public Text staminaText;

    private void Awake()
    {
        _UI = GameObject.FindGameObjectWithTag("UI");
        healthSlider = GameObject.Find("/PlayerUI/HealthBar/Background/FillMask").GetComponent<Image>();
        healthText = GameObject.Find("/PlayerUI/HealthBar/Background/HealthValue").GetComponent<Text>();
        staminaSlider = GameObject.Find("/PlayerUI/PlayerStamina/Background/FillMask").GetComponent<Image>();
        staminaText = GameObject.Find("/PlayerUI/PlayerStamina/Background/StaminaValue").GetComponent<Text>();
        _youLose = GameObject.Find("/PlayerUI/GameSettings/YouLoseCanvas/YouLosePanel");
    }

    private void Start()
    { 
        currentHealth = maxHealth;
        healthText.text = ($"{maxHealth.ToString()}");      
    }

    private void FixedUpdate()
    {
        staminaSlider.fillAmount += .002f;
        int currentStamina = Mathf.RoundToInt(staminaSlider.fillAmount*100);
        staminaText.text = ($"{currentStamina.ToString()}");
        healthSlider.fillAmount = currentHealth/maxHealth;
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
        _youLose.SetActive(true);
    }
}
