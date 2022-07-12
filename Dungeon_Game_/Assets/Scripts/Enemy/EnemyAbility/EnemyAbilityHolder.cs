using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAbilityHolder : MonoBehaviour
{

    public Ability Ability;
    private float _CooldownTime;
    private float _ActiveTime;
    [SerializeField]
    protected bool IsInAbilityRange;
    [SerializeField]
    protected float AbilityRadius;
    [SerializeField]
    protected LayerMask WhatIsPlayer;
    enum EnemyAbilityState 
    {
        ready, 
        active,
        cooldown
    }

    EnemyAbilityState state = EnemyAbilityState.ready;


    void Update()
    {
        IsInAbilityRange = Physics2D.OverlapCircle(transform.position, AbilityRadius, WhatIsPlayer);
        switch (state)
        {
            case EnemyAbilityState.ready:
               if (IsInAbilityRange)
            {
                Ability.Activate(gameObject);
                state = EnemyAbilityState.active;
                _ActiveTime = Ability.activeTime;

            }

            break;
            case EnemyAbilityState.active:
                if (_ActiveTime > 0 )
                {
                    _ActiveTime -= Time.deltaTime;
                }
                else 
                {
                    Ability.BeginCooldown(gameObject);
                    state = EnemyAbilityState.cooldown;
                    _CooldownTime = Ability.cooldownTime;
                }
            break;
            case EnemyAbilityState.cooldown:
                if (_CooldownTime > 0 )
                {
                    _CooldownTime -= Time.deltaTime;
                }
                else 
                {
                    state = EnemyAbilityState.ready;
                }
                    
            break;     
         }
    }
}
