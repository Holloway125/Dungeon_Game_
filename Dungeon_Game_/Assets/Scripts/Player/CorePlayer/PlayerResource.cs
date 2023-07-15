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

    //stamina properties
    [Header ("Stamina Settings")]
    [SerializeField]public float rollCost = .25f;
    [SerializeField]public float attackCost = .15f;
    [SerializeField]private float staminaRegenRate = 1;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<CharacterStats>();
        _UI = GameObject.FindGameObjectWithTag("UI");
        _youLose = GameObject.Find("/PlayerUI/GameSettings/YouLoseCanvas/YouLosePanel");
    }

    private void Start()
    { 
        _animator.SetBool("Death", false);   
    }

    private void FixedUpdate()
    {
        playerStats.SetCurrentStam(playerStats.GetCurrentStam() + staminaRegenRate);
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
        //play death animation
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _youLose.SetActive(true);
    }

}
