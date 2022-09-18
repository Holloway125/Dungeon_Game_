using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelSystem : MonoBehaviour
{
    
    [SerializeField]
    private int _skillPoints;
    public int SkillPoints
    {
        get { return _skillPoints; }
        set { _skillPoints = value; }
    }

    public int playerXp;//current running total
    public int totalXp;//total xp needed to lvl
    public int playerLvl;
    public TMP_Text lvlText;
    public Slider expBar;

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

     
    public void LevelUP()
    {
        PlayerResource playerResource = player.GetComponent<PlayerResource>();
        playerLvl++;
        SkillPoints++;
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
        playerResource.SetHealth(playerResource.healthSlider.maxValue);
        playerResource.currentHealth = playerResource.maxHealth;
        playerResource.healthText.SetText($"{playerResource.currentHealth.ToString()} / {playerResource.maxHealth.ToString()}");          
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
}
