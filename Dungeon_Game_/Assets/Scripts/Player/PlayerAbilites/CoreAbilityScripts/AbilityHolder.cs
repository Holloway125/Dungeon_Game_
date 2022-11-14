using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolder : MonoBehaviour
{
    private PlayerActions _playerActions;
    public Ability ability;
    public InputActionReference key;

    float cooldownTime;
    float activeTime;
    enum AbilityState 
    {
        ready, 
        active,
        cooldown
    }

    AbilityState state = AbilityState.ready;

    void Awake()
    {
        _playerActions = new PlayerActions();
    }

    void Start()
    {
        _playerActions.Player_Map.Dodge.performed += state => AbilityActivate();

    }

    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }

    private void AbilityActivate()
    {
        switch (state)
        {
            case AbilityState.ready:
                {
                    ability.Activate(gameObject);
                    state = AbilityState.active;
                    activeTime = ability.activeTime;
                }
            break;

            case AbilityState.active:
                if ( activeTime > 0 )
                {
                    activeTime -= Time.deltaTime;
                }
                else if (activeTime <= 0 )
                {
                    ability.BeginCooldown(gameObject);
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                }
            break;

            case AbilityState.cooldown:
                if ( cooldownTime > 0 )
                {
                    cooldownTime -= Time.deltaTime;
                }
                else if ( cooldownTime <= 0 )
                {
                    state = AbilityState.ready;
                } 
            break;
        }
    }
    
    // void Update()
    // {
            // switch (state)
            // {
            //     case AbilityState.ready:
            //         {
            //             ability.Activate(gameObject);
            //             state = AbilityState.active;
            //             activeTime = ability.activeTime;
            //         }
            //     break;

            //     case AbilityState.active:
            //         if ( activeTime > 0 )
            //         {
            //             activeTime -= Time.deltaTime;
            //         }
            //         else if (activeTime <= 0 )
            //         {
            //             ability.BeginCooldown(gameObject);
            //             state = AbilityState.cooldown;
            //             cooldownTime = ability.cooldownTime;
            //         }
            //     break;

            //     case AbilityState.cooldown:
            //         if ( cooldownTime > 0 )
            //         {
            //             cooldownTime -= Time.deltaTime;
            //         }
            //         else if ( cooldownTime <= 0 )
            //         {
            //             state = AbilityState.ready;
            //         } 
            //     break;
            // }
    // }
}
