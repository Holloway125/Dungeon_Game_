using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu]


public class Dash : Ability
{
    public float dashVelocity;

    public override void Activate(GameObject player)
     {
        ParticleSystem effect = player.GetComponent<ParticleSystem>();
        PlayerResource stamina = player.GetComponent<PlayerResource>();
        Player_Movement movement = player.GetComponent<Player_Movement>();
        if(stamina.Stamina.value >=20f)
        {
        movement.speed = movement.speed * dashVelocity;
        stamina.Stamina.value -= 20f;
        effect.Play();
       
        
        
        }
     }

    public override void BeginCooldown(GameObject player)
     {
        ParticleSystem effect = player.GetComponent<ParticleSystem>(); 
        TrailRenderer dashEffect = player.GetComponent<TrailRenderer>();
        Player_Movement movement = player.GetComponent<Player_Movement>();
        movement.speed = 12f;
        effect.Stop();
        
        
     }

}    
