using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageInt;
    Damage damageScript;
    GameObject otherScript;
    public bool tookDamage;

    void Awake()
    {
        otherScript = GameObject.Find("DamageScript");
        damageScript = otherScript.GetComponent<Damage>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
     {
         GameObject player = GameObject.FindWithTag("Player");

         if (collision.gameObject.tag == "Player")
         {
            damageScript.TakeDamage(damageInt); 
            tookDamage = true;
         }
     }

}

