using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    public int damage;
    PlayerResource playerResource;



      private void OnCollisionEnter2D(Collision2D collision)
     {
         if(collision.gameObject.tag == "Player")
         {
             playerResource.TakeDamage(damage);
         }
     }


}

