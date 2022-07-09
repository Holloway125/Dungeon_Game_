using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int damageInt;
    [SerializeField]
    Damage damageScript;
    public bool tookDamage;
    private GameObject player;

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");

        damageScript = player.GetComponent<Damage>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
     {

         if (collision.gameObject.tag == "Player")
         {
            damageScript.TakeDamage(damageInt); 
            tookDamage = true;
         }
     }

}

