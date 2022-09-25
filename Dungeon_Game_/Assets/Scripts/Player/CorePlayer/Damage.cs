using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour 
{
    PlayerResource playerResource;

    void Awake()
    {

        playerResource = GetComponent<PlayerResource>();
    }
        public void TakeDamage(float damage) // input the amount of damage you want the player to take on each monster
    {
        if(playerResource.currentHealth > damage)
        {
        playerResource.currentHealth -= damage;
        playerResource.healthText.text = ($"{Mathf.RoundToInt(playerResource.currentHealth).ToString()}");
        }
        else
        {
            Death();
        }
    }

        public void Death() //sets current health to 0 plays death animation puts up return to title canvas and stops all movement
    {
        playerResource.currentHealth = 0;
        playerResource.healthText.text = ($"{playerResource.currentHealth.ToString()}");
        //play death animation
        playerResource.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        playerResource._animator.SetTrigger("death");
        playerResource.youLose.SetActive(true);
    
    }

}
