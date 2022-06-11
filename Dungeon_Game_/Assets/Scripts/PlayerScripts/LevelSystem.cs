using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelSystem : MonoBehaviour
{
     // public HealthBar healthBar;
     public int level;
     public float totalCurrentXP;
     public float currentXP;
     public float reqXP;
     // public Slider healthBarSlider;
     public float currentHealth;
     public TMP_Text playerLevel;
     public Slider expBar;
     public int lvl;
     public TMP_Text healthText;

     void Start()
     {
         reqXP = 83;
         currentXP = 0;
         expBar.value = currentXP;
         expBar.maxValue = reqXP;
         lvl = 1;
         totalCurrentXP = 1;        
     }

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
        //healthBar.SetHealth(healthBarSlider.maxValue);
        //currentHealth = healthBarSlider.value;
         //healthText.SetText($"{currentHealth.ToString()}  {healthBarSlider.maxValue.ToString()}");       
    }
}
