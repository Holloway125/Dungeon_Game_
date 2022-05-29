using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

        public int enemyMaxHealth;
        public int enemyCurrentHealth;
        public Slider slider;
        public GameObject canvasHealthSlider;


    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = enemyMaxHealth;
        slider.minValue = 0;
        slider.value = enemyCurrentHealth;
        enemyCurrentHealth = enemyMaxHealth;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = enemyCurrentHealth;

        if(enemyCurrentHealth < enemyMaxHealth)
        {
            canvasHealthSlider.SetActive(true);
        }

        if(enemyCurrentHealth <= 0)
        {
            
            Destroy (gameObject);
        }

        if(enemyCurrentHealth > enemyMaxHealth)
        {
             enemyCurrentHealth = enemyMaxHealth;
        }
    }

}
