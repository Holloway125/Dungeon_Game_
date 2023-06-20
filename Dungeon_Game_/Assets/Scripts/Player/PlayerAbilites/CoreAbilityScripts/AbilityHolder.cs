using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolder : MonoBehaviour
{
    private PlayerActions _playerActions;
    private AbilityState state = AbilityState.ready;
    private AbilityState stateTwo = AbilityState.ready;
    private enum AbilityState 
    {
        ready, 
        active,
        cooldown
    }

    private float cooldownTime;
    private float activeTime;

    public Ability ability;
    public Ability abilityTwo;


    private void Awake()
    {
        _playerActions = new PlayerActions();
    }

    private void Start()
    {
        _playerActions.Player_Map.Roll.performed += state => AbilityActivate();
        _playerActions.Player_Map.Fire.performed += stateTwo => AbilityActivateTwo();
    }

    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }
    
    public void AbilityActivate()
    {
        ability.Activate(gameObject);
        state = AbilityState.active;
        activeTime = ability.activeTime;
    }

    public void AbilityActivateTwo()
    {
        abilityTwo.Activate(gameObject);
        stateTwo = AbilityState.active;
        activeTime = abilityTwo.activeTime;
    }

    private void Update()
    {
        switch(state)
        {
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

        switch(stateTwo)
        {
            case AbilityState.active:
                if ( activeTime > 0 )
                {
                    activeTime -= Time.deltaTime;
                }
                else if (activeTime <= 0 )
                {
                    abilityTwo.BeginCooldown(gameObject);
                    stateTwo = AbilityState.cooldown;
                    cooldownTime = abilityTwo.cooldownTime;
                }
            break;

            case AbilityState.cooldown:
                if ( cooldownTime > 0 )
                {
                    cooldownTime -= Time.deltaTime;
                }
                else if ( cooldownTime <= 0 )
                {
                    stateTwo = AbilityState.ready;
                } 
            break;
        }
    }
}
