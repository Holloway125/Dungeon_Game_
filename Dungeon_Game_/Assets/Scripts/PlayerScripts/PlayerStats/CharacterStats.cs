using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public List<BaseValueStat> stats = new List<BaseValueStat>();

    void Start()
    {
        stats.Add(new BaseValueStat(4, "Power", "Your power level."));
        stats.Add(new BaseValueStat(2, "Defense", "Your defense level."));
        Debug.Log(stats[0].GetCalculatedStatValue());
    }
    
    public void AddStatBonus(List<BaseValueStat> statBonuses)
    {
        foreach(BaseValueStat statBonus in statBonuses)
        {
            stats.Find(x => x.StatName == statBonus.StatName).AddStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }

        public void RemoveStatBonus(List<BaseValueStat> statBonuses)
    {
        foreach(BaseValueStat statBonus in statBonuses)
        {
            stats.Find(x => x.StatName == statBonus.StatName).RemoveStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }
    // public CharacterStats(int power, int defense, int attackSpeed)
    // {
    //     stats = new List<BaseValueStat>() { 
    //         new BaseValueStat(BaseValueStat.BaseStatType.Power, power, "Power"),
    //         new BaseValueStat(BaseValueStat.BaseStatType.Defense, defense, "Defense"),
    //         new BaseValueStat(BaseValueStat.BaseStatType.AttackSpeed, attackSpeed, "Attack Speed")
    //     };
    // }

    // public BaseValueStat GetStat(BaseValueStat.BaseStatType stat)
    // {
    //     return this.stats.Find(x => x.StatType == stat);
    // }

    // public void AddStatBonus(List<BaseValueStat> statBonuses)
    // {
    //     foreach(BaseValueStat statBonus in statBonuses)
    //     {
    //         GetStat(statBonus.StatType).AddStatBonus(new StatBonus(statBonus.BaseValue));
    //     }
    // }

    // public void RemoveStatBonus(List<BaseValueStat> statBonuses)
    // {
    //     foreach(BaseValueStat statBonus in statBonuses)
    //     {
    //         GetStat(statBonus.StatType).RemoveStatBonus(new StatBonus(statBonus.BaseValue));
    //     }
    // }

}
