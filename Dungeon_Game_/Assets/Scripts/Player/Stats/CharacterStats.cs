using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats;

public class CharacterStats : MonoBehaviour
{
    // public BaseStats "with your stat"; this will allow you to add this stat to the BaseStats value List and set its basevalue
    [SerializeField] private int MaxHP = 100;
    [SerializeField] private int CurrentHP;
    [SerializeField] private int MaxStam = 100;
    [SerializeField] private int CurrentStam;
    [SerializeField] private float Attack = 10;
    [SerializeField] private float AttackSpeed = 0;
    [SerializeField] private float Crit = 0;
    [SerializeField] private float Defense = 10;
    [SerializeField] private static float DefaultSpeed = 3;
    [SerializeField] private float Speed;





    public int GetMaxStam()
    {
        return MaxStam;
    }
    public void SetMaxStam(int i)
    {
        MaxStam = i;
    }






    public float GetCurrentStam()
    {
        return CurrentStam;
    }
    public void SetCurrentStam(float i)
    {
        if(Mathf.FloorToInt(i) < MaxStam)
        {
            CurrentStam = Mathf.FloorToInt(i);
        }
        else if(Mathf.FloorToInt(i) > MaxStam)
        {
            CurrentStam = MaxStam;
        }
    }







    public void SetMaxHP(float i)
    {
        if(i >= 0 && i <= 1000)
        {
            MaxHP = Mathf.FloorToInt(i);
        }
    }

    public float GetMaxHP()
    {
        return MaxHP;
    }






    public void SetCurrentHP(float i)
    {
        if(Mathf.FloorToInt(i) <= MaxHP)
        {
            CurrentHP = Mathf.FloorToInt(i);
        }
    }

    public float GetCurrentHP()
    {
        return CurrentHP;
    }














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



    public void SetAttack(float i)
    {
        if(i >= 0 && i <=250)
        {
            Attack = i;
        }
    }

    public float GetAttack()
    {
        return Attack;
    }

    public void SetAttackSpeed(float i)
    {
        if(i >= 0 && i <=.51f)
        {
            AttackSpeed = i;
        }
    }

    public float GetAttackSpeed()
    {
        return AttackSpeed;
    }

    public void SetCrit(float i)
    {
        if(i >= 0 && i <=.51f)
        {
        Crit = i;
        }
    }

    public float GetCrit()
    {
        return Crit;
    }

    public void SetDefense(float i)
    {
        Defense = i;
    }

    public float GetDefense()
    {
        return Defense;
    }


}
