using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelSystem : MonoBehaviour
{
    //level properties
    public int playerXp;//current running total
    public int totalXp;//total xp needed to lvl
    public int playerLvl;
    public TMP_Text lvlText;
    public Slider expBar;

    //Health Refrence
    public GameObject player;

     void Start()
     { 
        totalXp = 100; //exp needed to get to lvl 2  
        playerXp = 0;
        expBar.value = playerXp;
        expBar.maxValue = totalXp;
        playerLvl = 1;   
     }

     void Update()
    {
    if(Input.GetKeyDown(KeyCode.Equals))
        {
            GainExperienceFlatRate(250);
                if (playerXp >= totalXp)
                {    
                LevelUP();
                }
        }
    }

    public void GainExperienceFlatRate(int xpGained)
    {
        playerXp += xpGained;
        expBar.value += xpGained;
    }
     
    public void LevelUP() // sets health to max health and levels the character rolls left over exp over to next level progression
    {
        PlayerResource playerResource = player.GetComponent<PlayerResource>();//gets PlayerResource script by referencing player GameObject
        playerLvl++;
        lvlText.SetText(playerLvl.ToString());
        int oldTotal = totalXp; // holds previous lvls totalxp value
        totalXp = totalXp * 2; //exp curve... change later

        if(playerXp>=totalXp)//levels up until playerXp is less than totalxp
        {
            LevelUP();
        }
        else
        {
        expBar.maxValue = totalXp - oldTotal;
        expBar.value = playerXp - oldTotal;
        }
        //must use playerResource to get health properties
        playerResource.SetHealth(playerResource.healthSlider.maxValue); //Sets health to full after lvl up
        playerResource.currentHealth = playerResource.maxHealth;
        playerResource.healthText.SetText($"{playerResource.currentHealth.ToString()} / {playerResource.maxHealth.ToString()}");          
    }

}
