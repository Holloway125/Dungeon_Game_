using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Ability
{

      PlayerController movement;
      PlayerResource playerResource;
      ParticleSystem effect;
      GameObject parent;
      PlayerController playerController;


    public override void Activate(GameObject parent)
     {  
         parent = GameObject.Find("Player");
         playerController = parent.GetComponent<PlayerController>();
         effect = parent.GetComponent<ParticleSystem>();
         playerResource = parent.GetComponent<PlayerResource>();
         movement = parent.GetComponent<PlayerController>();

        if(playerResource.staminaSlider.fillAmount >= .4f)
        {
        playerController._speed = 15f;
        playerResource.staminaSlider.fillAmount -= .4f;
        effect.Play();
        }
     }

    public override void BeginCooldown(GameObject player)
     {
        ParticleSystem effect = player.GetComponent<ParticleSystem>(); 
        TrailRenderer dashEffect = player.GetComponent<TrailRenderer>();
        PlayerController movement = player.GetComponent<PlayerController>();
        playerController._speed = 5;
        effect.Stop();
     }

}    
