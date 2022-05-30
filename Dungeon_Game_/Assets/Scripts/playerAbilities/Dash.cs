using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu]


public class Dash : Ability
{
    public float dashVelocity;
    public override void Activate(GameObject player)
    {
        PlayerResource stamina = player.GetComponent<PlayerResource>();
        Player_Movement movement = player.GetComponent<Player_Movement>();
        if(stamina.Stamina.value >=20f)
        {
        movement.speed = movement.speed * dashVelocity;
        stamina.Stamina.value -= 20f;
        }
    }

    public override void BeginCooldown(GameObject player)
    {
        Player_Movement movement = player.GetComponent<Player_Movement>();
        movement.speed = 12f;
    }
}    
