using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Ability
{
   
   PlayerController movement;
   PlayerController playerController;
   PlayerResource playerResource;
   PlayerActions _playerActions;
   ParticleSystem effect;
   AbilityHolder _abilityHolder;
   GameObject parent;

   public override void Activate(GameObject parent)
   {  
      parent = GameObject.Find("Player");
      playerController = parent.GetComponent<PlayerController>();
      playerResource = parent.GetComponent<PlayerResource>();
      movement = parent.GetComponent<PlayerController>();

      if(playerResource.staminaSlider.fillAmount >= .4f)
      {
      playerController._speed = 15f;
      playerResource.staminaSlider.fillAmount -= .4f;
      //Play Animation
      }
   }

   public override void BeginCooldown(GameObject player)
   {
      ParticleSystem effect = player.GetComponent<ParticleSystem>(); 
      TrailRenderer dashEffect = player.GetComponent<TrailRenderer>();
      PlayerController movement = player.GetComponent<PlayerController>();
      playerController._speed = 5;
   }
}
