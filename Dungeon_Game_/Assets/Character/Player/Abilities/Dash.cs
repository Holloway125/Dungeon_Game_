using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dash : Ability
{
    public float dashVelocity;
    public override void Activate(GameObject parent)
    {
        Player_Movement movement = parent.GetComponent<Player_Movement>();
        movement.speed = dashVelocity;
    }

    public override void BeginCooldown(GameObject parent)
    {
        Player_Movement movement = parent.GetComponent<Player_Movement>();
        movement.speed = 5f;
    }
}    
