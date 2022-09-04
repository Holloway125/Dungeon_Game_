using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAbilityHolder : MonoBehaviour
{

    public Ability Ability;
    private float _CooldownTime;
    private float _ActiveTime;
    [SerializeField]
    public bool IsInAbilityRange;
    [SerializeField]
    protected float AbilityRadius;
    [SerializeField]
    protected LayerMask WhatIsPlayer;
    [HideInInspector]
    protected BaseEnemy BaseEnemyScript;
    public enum EnemyAbilityState 
    {
        ready, 
        active,
        cooldown
    }

    void Start()
    {
        BaseEnemyScript = GetComponent<BaseEnemy>();
    }
    public EnemyAbilityState state = EnemyAbilityState.ready;


    void Update()
    {
        IsInAbilityRange = Physics2D.OverlapCircle(transform.position, AbilityRadius, WhatIsPlayer);
        switch (state)
        {
            case EnemyAbilityState.ready:
               if (IsInAbilityRange && BaseEnemyScript.Aggroed)
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
