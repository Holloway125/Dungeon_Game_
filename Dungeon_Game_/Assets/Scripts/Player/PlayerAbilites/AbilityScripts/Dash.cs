using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Ability
{

      PlayerController movement;
      PlayerResource playerResource;
      ParticleSystem effect;
      GameObject parent;
      PlayerProperties playerproperties;


    public override void Activate(GameObject parent)
     {  

         parent = GameObject.Find("Player");
         playerproperties = parent.GetComponent<PlayerProperties>();
         effect = parent.GetComponent<ParticleSystem>();
         playerResource = parent.GetComponent<PlayerResource>();
         movement = parent.GetComponent<PlayerController>();

        if(playerResource.staminaSlider.fillAmount >=.2f)
        {
        playerproperties.Speed = 25f;
        playerResource.staminaSlider.fillAmount -= .2f;
        effect.Play();
        Debug.Log("Dash Active");
        Debug.Log(playerproperties.Speed);
        }
     }

    public override void BeginCooldown(GameObject player)
     {
        ParticleSystem effect = player.GetComponent<ParticleSystem>(); 
        TrailRenderer dashEffect = player.GetComponent<TrailRenderer>();
        PlayerController movement = player.GetComponent<PlayerController>();
        playerproperties.Speed = 5;
        effect.Stop();
     }

}    
