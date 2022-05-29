using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

        public int enemyMaxHealth;
        public int enemyCurrentHealth;
        public Slider slider;
        public GameObject healthBarUI;


    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        slider.value = enemyCurrentHealth;
        slider.maxValue = enemyMaxHealth;
        slider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = enemyCurrentHealth;

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
