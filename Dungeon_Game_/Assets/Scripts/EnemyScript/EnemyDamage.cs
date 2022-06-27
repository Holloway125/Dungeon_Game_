using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageInt;
    Damage damageScript;
    GameObject otherScript;
    public bool tookDamage;
    private GameObject player;

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        otherScript = GameObject.Find("DamageScript");
        damageScript = otherScript.GetComponent<Damage>();
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

