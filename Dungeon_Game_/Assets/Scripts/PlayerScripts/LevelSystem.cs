using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelSystem : MonoBehaviour
{

    public int level;
    public float totalCurrentXP;
    public float currentXP;
    public float reqXP;
    public TMP_Text playerLevel;
    public Slider expBar;
    public int lvl;

    // Start is called before the first frame update
    void Start()
    {
        reqXP = 83;
        currentXP = 0;
        expBar.value = currentXP;
        expBar.maxValue = reqXP;
        lvl = 1;
        totalCurrentXP = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Equals))
        GainExperienceFlatRate(20);
        if (currentXP > reqXP)
        {    
            LevelUP();
        }
    }
    public void GainExperienceFlatRate( float xpGained)
    {
        totalCurrentXP+=20;  
        expBar.value = currentXP += xpGained; 

    }
    public void LevelUP()
    {
        lvl++;
        currentXP = Mathf.RoundToInt(currentXP - reqXP);
        playerLevel.SetText(lvl.ToString());
        reqXP = totalCurrentXP * 0.28571428571f;
    }
}
