using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SkillTree;


public class LevelSystem : MonoBehaviour
{
    public int playerXp;//current running total
    public int totalXp;//total xp needed to lvl
    public int playerLvl;
    public TMP_Text lvlText;
    public Slider expBar;
    public SkillTree skillTree;
    
    private GameObject player; 

    void Awake()
    {
        player = GameObject.Find("Player");
        expBar.value = playerXp;
        expBar.maxValue = totalXp; 
        totalXp = 100;  
        playerXp = 0;
        playerLvl = 1;
    }

    void Start()
    {

    }

    public void GainExperience(int exp)
    {
        playerXp += exp;
        expBar.value += exp;
            if (playerXp >= totalXp)
                {    
                LevelUP();
                }
    }
     
    public void LevelUP()
    {
        PlayerResource playerResource = player.GetComponent<PlayerResource>();
        skillTree.skillPoints ++;
        playerResource.SetMaxHealth(50+(playerLvl*5)); //increases health by 5 everytime playerlvls
        lvlText.SetText(playerLvl.ToString());
        int oldTotal = totalXp; // holds previous lvls totalxp value
        totalXp = totalXp * 2; //exp curve... change later

        if(playerXp>=totalXp) //levels up until playerXp is less than totalxp
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
