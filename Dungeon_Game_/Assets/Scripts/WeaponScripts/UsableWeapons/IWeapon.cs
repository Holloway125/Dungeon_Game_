using System.Collections.Generic;

public interface IWeapon 
{
    List<BaseValueStat> Stats { get; set; }
    void PerformAttack();

}
