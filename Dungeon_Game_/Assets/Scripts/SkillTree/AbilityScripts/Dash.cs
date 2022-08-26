using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Ability
{
    public float dashVelocity;

    public override void Activate(GameObject parent)
     {
        ParticleSystem effect = parent.GetComponent<ParticleSystem>();
        PlayerResource playerResource = parent.GetComponent<PlayerResource>();
        PlayerController movement = parent.GetComponent<PlayerController>();
        if(playerResource.staminaSlider.value >=20f)
        {
        movement.speed = movement.speed * dashVelocity;
        playerResource.staminaSlider.value -= 20f;
        effect.Play();
        }
     }

    public override void BeginCooldown(GameObject player)
     {
        ParticleSystem effect = player.GetComponent<ParticleSystem>(); 
        TrailRenderer dashEffect = player.GetComponent<TrailRenderer>();
        PlayerController movement = player.GetComponent<PlayerController>();
        movement.speed = 5f;
        effect.Stop();
     }

}    
