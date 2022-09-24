using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelSystem : MonoBehaviour
{
    //current running total
    public int playerXp;

    //total xp needed to lvl
    public int totalXp;

    public int playerLvl;
    public Text lvlText;
    public Image expBar;

    private GameObject player; 
    private PlayerResource playerResource;

    void Awake()
    {
        player = GameObject.Find("Player");
        playerResource = player.GetComponent<PlayerResource>();
        expBar.fillAmount = 0; 
        totalXp = 100;  
        playerXp = 0;
        playerLvl = 1;
    }
     
    public void LevelUP()
    {
        playerLvl++;
        playerResource.maxHealth = 100+(playerLvl*25);
        playerResource.healthText.text = ($"{playerResource.maxHealth.ToString()}");          
        playerResource.healthSlider.fillAmount = 1;
        playerXp = playerXp - totalXp;
        lvlText.text = (playerLvl.ToString());

        //EXP CURVE NEEDS UPDATE
        totalXp = totalXp * 2;

        if(playerXp>=totalXp)
        {
            LevelUP();
        }
        else
        {
        expBar.fillAmount = (float)playerXp / (float)totalXp;
        }
    }

    public void GainExperience(int exp)
    {
        playerXp += exp;

        if (playerXp >= totalXp)
            {    
                LevelUP();
            }
        else 
            {
            expBar.fillAmount = (float)playerXp / (float)totalXp;
            }
        Debug.Log("xp gained");
    }
}
