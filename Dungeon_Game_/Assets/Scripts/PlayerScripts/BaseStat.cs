using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class BaseStat : MonoBehaviour
{
    public enum BaseStatType { Power, Defense, AttackSpeed }

    public List<StatBonus> BaseAdditives { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public BaseStatType StatType { get; set; }
    public int BaseValue { get; set; }
    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public int FinalValue { get; set; }
    
    public BaseStat(int baseValue, string statName, string StatDescription)
    {
        this.BaseAdditives = new List<StatBonus>();
        this.BaseValue = baseValue;
        this.StatName = statName;
        this.StatDescription = StatDescription;
    }

    [Newtonsoft.Json.JsonConstructor]
    public BaseStat(BaseStatType statType, int baseValue, string statName)
    {
        this.BaseAdditives = new List<StatBonus>();
        this.StatType = statType;
        this.BaseValue = baseValue;
        this.StatName = statName;

    }



    
}