using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelSystem : MonoBehaviour
{

    //total xp needed to lvl
    [SerializeField] private float totalXp;
    [SerializeField] float expGrowth = .12f;
    
    //current running total
    [SerializeField] private float playerXp;

    [SerializeField] private float playerLvl;
    private Text lvlText;
    private Image expBar;

    private GameObject player; 
    private PlayerResource playerResource;
    private CharacterStats playerStats;
    private GameObject UICharacterStats;
    private UICharacterStats UIPlayerStats;

    [SerializeField] private float attackIncrease = 1;
    [SerializeField] private float defenseIncrease = 1;
    [SerializeField] private float attackSpeedIncrease = .05f;
    [SerializeField] private float critIncrease = .05f;


    private void Awake()
    {
        player = GameObject.Find("Player");
        playerResource = player.GetComponent<PlayerResource>();
        expBar = GameObject.Find("/PlayerUI/Exp/Background/FillMask").GetComponent<Image>();
        lvlText = GameObject.Find("/PlayerUI/Level").GetComponent<Text>();
        playerStats = player.GetComponent<CharacterStats>();
        UICharacterStats = GameObject.Find("UI_Character Stats");
        UIPlayerStats = UICharacterStats.GetComponent<UICharacterStats>();
    }

    private void Start()
    {
        expBar.fillAmount = 0;
        totalXp = 100;  
        playerXp = 0;
        playerLvl = 1;
    }

    private void SetTotalXP(float xp)
    {
        totalXp = xp;
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

        //Attack Modifier
        playerStats.SetAttack(playerStats.GetAttack() + attackIncrease);

        //Defense Modifier
        playerStats.SetDefense(playerStats.GetDefense() + defenseIncrease);

        //AttackSpeed Modifier
        playerStats.SetAttackSpeed(playerStats.GetAttackSpeed() + attackSpeedIncrease);

        //Crit Modifier
        playerStats.SetCrit(playerStats.GetCrit() + critIncrease);

        playerXp = playerXp - totalXp;
        lvlText.text = (playerLvl.ToString());

        //Exp Curve
        SetTotalXP((1+expGrowth) * 100 *playerLvl);
        UIPlayerStats.UpdateValues();

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
