using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu]


public class Dash : Ability
{
    public float dashVelocity;
    public override void Activate(GameObject parent)
    {
        PlayerResource stamina = parent.GetComponent<PlayerResource>();
        Player_Movement movement = parent.GetComponent<Player_Movement>();
        if(stamina.Stamina.value >=20f)
        {
        movement.speed = movement.speed * dashVelocity;
        stamina.Stamina.value -= 20f;
        }
    }

    public override void BeginCooldown(GameObject parent)
    {
        Player_Movement movement = parent.GetComponent<Player_Movement>();
        movement.speed = 12f;
    }
}    
