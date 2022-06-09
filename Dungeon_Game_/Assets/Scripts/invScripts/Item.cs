using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public List<BaseValueStat> Stats { get; set; }
    public string  ObjectSlug { get; set; }

    public Item(List<BaseValueStat> _Stats, string _ObjectSlug)
    {
        this.Stats = _Stats;
        this.ObjectSlug = _ObjectSlug;
    }
}
