using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : ScriptableObject
{
    // public static BaseStats "with your stat"; this will allow you to add this stat to the BaseStats value List and set its basevalue
    [SerializeField] private static int MaxHP = 100;
    [SerializeField] private static int CurrentHP;
    [SerializeField] private static  int MaxStam = 100;
    [SerializeField] private static int CurrentStam;
    [SerializeField] private static float Attack = 10;
    [SerializeField] private static float AttackSpeed = 0;
    [SerializeField] private static float Crit = 0;
    [SerializeField] private static float Defense = 10;
    [SerializeField] private static float DefaultSpeed = 3;
    [SerializeField] private static float Speed;

    public static int GetMaxStam()
    {
        return MaxStam;
    }
    public static void SetMaxStam(int i)
    {
        MaxStam = i;
        SetCurrentStam(MaxStam);
    }

    public static float GetCurrentStam()
    {
        return CurrentStam;
    }
    public static void SetCurrentStam(float i)
    {
        if(Mathf.FloorToInt(i) < MaxStam)
        {
            CurrentStam = Mathf.FloorToInt(i);
        }
        else if(Mathf.FloorToInt(i) >= MaxStam)
        {
            CurrentStam = MaxStam;
        }
        UIManager.Instance.UpdateStamBar();
    }

    public static void SetMaxHP(float i)
    {
        if(i >= 0 && i <= 1000)
        {
            MaxHP = Mathf.FloorToInt(i);
            SetCurrentHP(MaxHP);
        }
    }

    public static float GetMaxHP()
    {
        return MaxHP;
    }
    public static void SetCurrentHP(float i)
    {
        if(Mathf.FloorToInt(i) <= MaxHP)
        {
            CurrentHP = Mathf.FloorToInt(i);
        }
        UIManager.Instance.UpdateHealthBar();
    }

    public static float GetCurrentHP()
    {
        return CurrentHP;
    }

    public static float GetDefaultSpeed()
    {
        return DefaultSpeed;
    }

    public static float GetSpeed()
    {
        return Speed;
    }

    public static void SetSpeed(float i)
    {
        if(i >= 0 && i <=25)
        {
            Speed = i;
        }
    }

    public static void SetAttack(float i)
    {
        if(i >= 0 && i <=250)
        {
            Attack = i;
        }
        UIManager.Instance.UpdateValues();
    }

    public static float GetAttack()
    {
        return Attack;
    }

    public static void SetAttackSpeed(float i)
    {
        if(i >= 0 && i <=.51f)
        {
            AttackSpeed = i;
        }
        UIManager.Instance.UpdateValues();
    }

    public static float GetAttackSpeed()
    {
        return AttackSpeed;
    }

    public static void SetCrit(float i)
    {
        if(i >= 0 && i <=.51f)
        {
        Crit = i;
        }
        UIManager.Instance.UpdateValues();
    }

    public static float GetCrit()
    {
        return Crit;
    }
    public static void SetDefense(float i)
    {
        Defense = i;
        UIManager.Instance.UpdateValues();
    }

    public static float GetDefense()
    {
        return Defense;
    }
}
