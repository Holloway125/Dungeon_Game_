// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Dash : Ability
// {
   
//    PlayerController playerController;
//    PlayerActions _playerActions;
//    ParticleSystem effect;
//    AbilityHolder _abilityHolder;
//    GameObject parent;
//    CharacterStats playerStats;
//    Animator _anim;

//    public override void Activate(GameObject parent)
//    {  
//       parent = GameObject.Find("Player");
//       playerController = parent.GetComponent<PlayerController>();
//       playerStats = parent.GetComponent<CharacterStats>();
//       _anim = parent.GetComponent<Animator>();

//       if(playerStats.GetCurrentStam() >= 20)
//       {
//          playerStats.SetSpeed(15);
//          playerStats.SetCurrentStam(playerStats.GetCurrentStam() - 20);
//          _anim.SetTrigger($"{playerController.RollDir()}Roll");
//          Debug.Log($"{playerController.RollDir()} Roll");
//       }
//    }

//    public override void BeginCooldown(GameObject player)
//    {
//       ParticleSystem effect = player.GetComponent<ParticleSystem>(); 
//       TrailRenderer dashEffect = player.GetComponent<TrailRenderer>();
//       PlayerController playerController = player.GetComponent<PlayerController>();
//       playerStats.SetSpeed(playerStats.GetDefaultSpeed());
//    }
// }
