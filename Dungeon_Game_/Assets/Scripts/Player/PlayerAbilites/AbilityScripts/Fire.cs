using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Ability
{
   PlayerController movement;
   PlayerResource playerResource;
   ParticleSystem effect;
   GameObject parent;
   PlayerController playerController;

   public override void Activate(GameObject parent)
   {  
    //   parent = GameObject.Find("Player");
    //   playerController = parent.GetComponent<PlayerController>();
    //   playerResource = parent.GetComponent<PlayerResource>();
    //    movement = parent.GetComponent<PlayerController>();
    Debug.Log("Fire!");
   }

   public override void BeginCooldown(GameObject player)
   {
    //   ParticleSystem effect = player.GetComponent<ParticleSystem>(); 
    //   TrailRenderer dashEffect = player.GetComponent<TrailRenderer>();
    //   PlayerController movement = player.GetComponent<PlayerController>();
    //   playerController._speed = 5;
   }

}   
