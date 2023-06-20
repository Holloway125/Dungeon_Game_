using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Ability
{
   
   PlayerController playerController;
   PlayerResource playerResource;
   PlayerActions _playerActions;
   ParticleSystem effect;
   AbilityHolder _abilityHolder;
   GameObject parent;
   CharacterStats playerStats;
   Animator _anim;

   public override void Activate(GameObject parent)
   {  
      parent = GameObject.Find("Player");
      playerController = parent.GetComponent<PlayerController>();
      playerResource = parent.GetComponent<PlayerResource>();
      playerController = parent.GetComponent<PlayerController>();
      playerStats = parent.GetComponent<CharacterStats>();
      _anim = parent.GetComponent<Animator>();

      if(playerResource.staminaSlider.fillAmount >= .4f)
      {
         playerStats.SetSpeed(15);
         playerResource.staminaSlider.fillAmount -= .4f;
         _anim.SetTrigger($"{playerController.RollDir()}Roll");
         Debug.Log($"{playerController.RollDir()} Roll");
      }
   }

   public override void BeginCooldown(GameObject player)
   {
      ParticleSystem effect = player.GetComponent<ParticleSystem>(); 
      TrailRenderer dashEffect = player.GetComponent<TrailRenderer>();
      PlayerController playerController = player.GetComponent<PlayerController>();
      playerStats.SetSpeed(playerStats.GetDefaultSpeed());
   }
}
