using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelSystem : MonoBehaviour
{

    //total xp needed to lvl
    [SerializeField] private float XpToNextLvl;
    [SerializeField] float expGrowth = .12f;
    
    //current running total
    [SerializeField] private float TotalXp;

    [SerializeField] private int playerLvl;
    private Text lvlText;
    private Image expBar;

    private GameObject player; 
    private PlayerController PlayerController;
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
        PlayerController = player.GetComponent<PlayerController>();
        expBar = GameObject.Find("/PlayerUI/Exp/Background/FillMask").GetComponent<Image>();
        lvlText = GameObject.Find("/PlayerUI/Level").GetComponent<Text>();
        playerStats = player.GetComponent<CharacterStats>();
        UICharacterStats = GameObject.Find("UI_Character Stats");
        UIPlayerStats = UICharacterStats.GetComponent<UICharacterStats>();
    }

    private void Start()
    {
        expBar.fillAmount = 0;
        XpToNextLvl = 100;  
        TotalXp = 0;
        playerLvl = 1;
    }

    public float GetXpToNextLvl()
    {
        return XpToNextLvl;
    }
    public void SetXpToNextLvl(float xp)
    {
        XpToNextLvl = xp;
    }
    public float GetTotalXp()
    {
        return TotalXp;
    }
    public void SetTotalXp(float i)
    {
        TotalXp = i;
    }
    public int GetPlayerLvl()
    {
        return playerLvl;
    }
    private void LevelUP()
    {
        playerLvl++;
        Debug. Log("Leveled Up!");

        //Health Modifier
        playerStats.SetMaxHP(100+(playerLvl*25));   

        playerStats.SetCurrentHP(playerStats.GetMaxHP());

        //Attack Modifier
        playerStats.SetAttack(playerStats.GetAttack() + attackIncrease);

        //Defense Modifier
        playerStats.SetDefense(playerStats.GetDefense() + defenseIncrease);

        //AttackSpeed Modifier
        playerStats.SetAttackSpeed(playerStats.GetAttackSpeed() + attackSpeedIncrease);
        
        //Crit Modifier
        playerStats.SetCrit(playerStats.GetCrit() + critIncrease);

        TotalXp = TotalXp - XpToNextLvl;
        //Exp Curve
        SetXpToNextLvl((1+expGrowth) * 100 *playerLvl);

        UIPlayerStats.UpdateValues();


        if(TotalXp>=XpToNextLvl)
        {
            LevelUP();
        }
        else
        {
        expBar.fillAmount = (float)TotalXp / (float)XpToNextLvl;
        }
    }

    public void GainExperience(int exp)
    {
        TotalXp += exp;

        if (TotalXp >= XpToNextLvl)
            {    
                LevelUP();
            }
        else 
            {
            expBar.fillAmount = (float)TotalXp / (float)XpToNextLvl;
            }
            Debug.Log("Gained " + exp + "XP!");
    }
}
