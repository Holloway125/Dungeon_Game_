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

    private int playerLvl;
    private Text lvlText;
    private Image expBar;

    private GameObject player; 
    private PlayerResource playerResource;
    private CharacterStats playerStats;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerResource = player.GetComponent<PlayerResource>();
        expBar = GameObject.Find("/PlayerUI/Exp/Background/FillMask").GetComponent<Image>();
        lvlText = GameObject.Find("/PlayerUI/Level").GetComponent<Text>();
        playerStats = player.GetComponent<CharacterStats>();
    }

    private void Start()
    {
        expBar.fillAmount = 0;
        totalXp = 100;  
        playerXp = 0;
        playerLvl = 1;
    }

    private void LevelUP()
    {
        playerLvl++;
        Debug. Log("Leveled Up!");

        //Health Modifier
        playerStats.SetMaxHP(100+(playerLvl*25));
        playerResource._healthText.text = ($"{playerStats.GetMaxHP().ToString()}");          
        playerResource._healthSlider.fillAmount = 1;
        playerStats.SetCurrentHP(playerStats.GetMaxHP());
        Debug.Log("New Max HP " + playerStats.GetMaxHP());

        //Attack Modifier
        playerStats.SetAttack(playerStats.GetAttack() + 2);
        Debug.Log("New Attack " + playerStats.GetAttack());

        //Defense Modifier
        playerStats.SetDefense(playerStats.GetDefense() + 1);
        Debug.Log("New Defense " + playerStats.GetDefense());

        //AttackSpeed Modifier
        playerStats.SetAttackSpeed(playerStats.GetAttackSpeed() + 1);
        Debug.Log("New AttackSpeed " + playerStats.GetAttackSpeed());

        //Crit Modifier
        playerStats.SetCrit(playerStats.GetCrit() + 1);
        Debug.Log("New Crit " + playerStats.GetCrit());

        playerXp = playerXp - totalXp;
        lvlText.text = (playerLvl.ToString());

        //Exp Curve
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
            Debug.Log("Gained " + exp + "XP!");
    }
}
