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
   CharacterStats playerStats;

   public override void Activate(GameObject parent)
   {  
      //movement and playerController are the same reference?
      parent = GameObject.Find("Player");
      playerController = parent.GetComponent<PlayerController>();
      playerResource = parent.GetComponent<PlayerResource>();
      movement = parent.GetComponent<PlayerController>();
      playerStats = parent.GetComponent<CharacterStats>();

      if(playerResource.staminaSlider.fillAmount >= .4f)
      {
      playerStats.SetSpeed(15);
      playerResource.staminaSlider.fillAmount -= .4f;
      //Play Animation
      }
   }

   public override void BeginCooldown(GameObject player)
   {
      ParticleSystem effect = player.GetComponent<ParticleSystem>(); 
      TrailRenderer dashEffect = player.GetComponent<TrailRenderer>();
      PlayerController movement = player.GetComponent<PlayerController>();
      playerStats.SetSpeed(playerStats.GetDefaultSpeed());
   }
}
