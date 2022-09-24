using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelSystem : MonoBehaviour
{
    
    public int playerXp;//current running total
    public int totalXp;//total xp needed to lvl
    public int playerLvl;
    public Text lvlText;
    public Image expBar;

    private GameObject player; 

    void Awake()
    {
        player = GameObject.Find("Player");
        expBar.fillAmount = playerXp;
        expBar.fillAmount = totalXp; 
        totalXp = 100;  
        playerXp = 0;
        playerLvl = 1;
    }

     
    public void LevelUP()
    {
        PlayerResource playerResource = player.GetComponent<PlayerResource>();
        playerLvl++;
        playerResource.SetMaxHealth(50+(playerLvl*5)); //increases health by 5 everytime playerlvls
        lvlText.text = (playerLvl.ToString());
        int oldTotal = totalXp; // holds previous lvls totalxp fillAmount
        totalXp = totalXp * 2; //exp curve... change later

        if(playerXp>=totalXp) //levels up until playerXp is less than totalxp
        {
            LevelUP();
        }
        else
        {
        expBar.fillAmount = totalXp - oldTotal;
        expBar.fillAmount = playerXp - oldTotal;
        }
        //must use playerResource to get health properties
        playerResource.SetHealth(playerResource.healthSlider.fillAmount);
        playerResource.currentHealth = playerResource.maxHealth;
        playerResource.healthText.text = ($"{playerResource.currentHealth.ToString()} / {playerResource.maxHealth.ToString()}");          
    }
    public void GainExperience(int exp)
        {
        playerXp += exp;
        expBar.fillAmount += exp;
            if (playerXp >= totalXp)
                {    
                    LevelUP();
                }
        }
}
