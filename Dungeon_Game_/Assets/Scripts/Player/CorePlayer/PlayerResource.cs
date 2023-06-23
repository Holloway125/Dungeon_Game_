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
    [Space]
    [Header ("UI Elements")]
    public GameObject _youLose;
    public GameObject player;
    public CharacterStats playerStats;

    //health properties
    [Space]
    [Header ("Health Bar")]
    public Image _healthSlider;
    public Text _healthText;

    //stamina properties
    [Header ("Stamina Settings")]
    [SerializeField]public float rollCost = .25f;
    [SerializeField]public float attackCost = .15f;
    [SerializeField]private float staminaRegenRate =   .0015f;
    public Image staminaSlider;
    public Text staminaText;

    [SerializeField] private float m_MySliderValue;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<CharacterStats>();
        _UI = GameObject.FindGameObjectWithTag("UI");
        _healthSlider = GameObject.Find("/PlayerUI/HealthBar/Background/FillMask").GetComponent<Image>();
        _healthText = GameObject.Find("/PlayerUI/HealthBar/Background/HealthValue").GetComponent<Text>();
        staminaSlider = GameObject.Find("/PlayerUI/PlayerStamina/Background/FillMask").GetComponent<Image>();
        staminaText = GameObject.Find("/PlayerUI/PlayerStamina/Background/StaminaValue").GetComponent<Text>();
        _youLose = GameObject.Find("/PlayerUI/GameSettings/YouLoseCanvas/YouLosePanel");
    }

    private void Start()
    { 
        playerStats.SetCurrentHP(playerStats.GetMaxHP());
        _healthText.text = ($"{playerStats.GetMaxHP().ToString()}");   
        _animator.SetBool("Death", false);   
    }

    private void FixedUpdate()
    {
        staminaSlider.fillAmount += staminaRegenRate;
        int currentStamina = Mathf.RoundToInt(staminaSlider.fillAmount*100);
        staminaText.text = ($"{currentStamina.ToString()}");
        _healthSlider.fillAmount = playerStats.GetCurrentHP()/playerStats.GetMaxHP();
    }
    
    public void TimeStop() // Needed for death Animation
    {
        Time.timeScale = 0f;
    }

    public void TakeDamage(float damage) // input the amount of damage you want the player to take on each monster
    {
        if (_animator.GetBool("Death") == false)
        {
            if(playerStats.GetCurrentHP() >= damage)
            {
            playerStats.SetCurrentHP(playerStats.GetCurrentHP()-damage);
            _healthText.text = ($"{Mathf.RoundToInt(playerStats.GetCurrentHP()).ToString()}");
                if (playerStats.GetCurrentHP() <= 0)
                {
                    Death();
                }
            }
        }
        else if (_animator.GetBool("Death") == true)
        {
            return;
        }
    }

    public void Death() //sets current health to 0 plays death animation puts up return to title canvas and stops all movement
    {
        _animator.Play("death");
        _animator.SetBool("Death", true);
        //playerStats.GetCurrentHP() = 0;
        _healthText.text = ($"{playerStats.GetCurrentHP().ToString()}");
        //play death animation
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _youLose.SetActive(true);
    }

}
