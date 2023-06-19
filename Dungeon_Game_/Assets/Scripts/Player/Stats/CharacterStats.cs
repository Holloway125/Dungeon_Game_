using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats;

public class CharacterStats : MonoBehaviour
{
    // public BaseStats "with your stat"; this will allow you to add this stat to the BaseStats value List and set its basevalue
    [SerializeField] private float MaxHP = 100;
    [SerializeField] private float CurrentHP;
    [SerializeField] private int Attack = 10;
    [SerializeField] private int AttackSpeed = 1;
    [SerializeField] private int Crit = 0;
    [SerializeField] private int Defense = 10;
    [SerializeField] private static float DefaultSpeed = 5;
    [SerializeField] private float Speed;

    public float GetDefaultSpeed()
    {
        return DefaultSpeed;
    }

    public float GetSpeed()
    {
        return Speed;
    }

    public void SetSpeed(float i)
    {
        if(i >= 0 && i <=25)
        {
            Speed = i;
        }
    }

    public void SetCurrentHP(float i)
    {
        if(i <= MaxHP)
        {
            CurrentHP = i;
        }
    }

    public float GetCurrentHP()
    {
        return CurrentHP;
    }

    public void SetAttack(int i)
    {
        if(i >= 0 && i <=25)
        {
            Attack = i;
        }
    }

    public int GetAttack()
    {
        return Attack;
    }

    public void SetAttackSpeed(int i)
    {
        if(i >= 0 && i <=25)
        {
            AttackSpeed = i;
        }
    }

    public int GetAttackSpeed()
    {
        return AttackSpeed;
    }

    public void SetCrit(int i)
    {
        Crit = i;
    }

    public int GetCrit()
    {
        return Crit;
    }

    public void SetDefense(int i)
    {
        Defense = i;
    }

    public int GetDefense()
    {
        return Defense;
    }

    public void SetMaxHP(float i)
    {
        if(i >= 0 && i <= 1000)
        {
            MaxHP = i;
        }
    }

    public float GetMaxHP()
    {
        return MaxHP;
    }

}
