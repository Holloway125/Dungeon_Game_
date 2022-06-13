using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//Custom Namespace if you want to add BaseStats to anything you must declare this namespace ("using Stats;")
namespace Stats
{
    [Serializable]
    public class BaseStats
    {

        public float BaseValue;
        
        public virtual float Value { 
            get {
                if (Changed || BaseValue != lastBaseValue)
            {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                Changed = false;
            }   
            return _value;
            }
        }
        protected bool Changed = true;
        protected float _value;
        protected float lastBaseValue = float.MinValue;

        protected readonly List<StatModifer> statModifer;
        public readonly ReadOnlyCollection<StatModifer> StatModifer;

        //intiallizes the List
        public BaseStats()
        {
            statModifer = new List<StatModifer>();
            StatModifer = statModifer.AsReadOnly();
        }

        
        // Constructor of BaseStats that takes the value of the stat you want to set
        public BaseStats(float baseValue) : this()
        {
            BaseValue = baseValue;

        }

         // places the modifers in the correct order
        protected virtual int CompareModiferOrder(StatModifer a, StatModifer b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0;
        }

        // adds value of modifer to stats
        public virtual void AddModifer(StatModifer mod) 
        {
            Changed = true;
            statModifer.Add(mod);
            statModifer.Sort(CompareModiferOrder);
        }
        
        // removes value of modifer from stats
        public virtual bool RemoveModifer(StatModifer mod)
        {
            if (statModifer.Remove(mod));
            {
                Changed = true;
                return true;
            }
            return false;
        }

        // removes all modifers from stats
        public virtual bool RemoveAllModifersFromSource(object source)
        {
            bool didRemove = false;

            for ( int i = statModifer.Count - 1; i >=0; i--)
            {
                if (statModifer[i].Source == source)
                {
                    Changed = true;
                    didRemove = true;
                    statModifer.RemoveAt(i);
                }
            }
            return didRemove;
        }

        // After all modifers are placed in the correct order this will then do the math and update the new stat values
        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifer.Count; i++)
            {
                StatModifer mod = statModifer[i];

                if (mod.Type == StatModType.Flat)
                {
                    finalValue += mod.Value;
                }
                else if (mod.Type == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;
                    if (i + i >= statModifer.Count || statModifer[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.Type == StatModType.PercentMultiple)
                {
                    finalValue *= 1 + mod.Value;
                }
            }

            return (float)Math.Round(finalValue, 4);
        }
    }
}