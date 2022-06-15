using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Stats;



    public enum WeaponType
    {
        Claymore,
        Sword,
        BattleAxe,
        Axe,
        Halberd,
        Poleaxe,
        Rapier,
        Spear,
        Staff,
        Wand,
        BigMace,
        Mace,
    }

        
    [CreateAssetMenu]
    
    public class Weapon : ScriptableObject
    {

        public int DamageBonus;
        public int CritalChanceBonus;
        public int CritalDamageBonus;
        public int AttackSpeedBonus;
        public int DefenseBonus;
        [Space]
        public float DamagePercentBonus;
        public float CritalChancePercentBonus;
        public float CritalDamagePercentBonus;
        public float AttackSpeedPercentBonus;
        public float DefensePercentBonus;
        [Space]
        public WeaponType weaponType;
        bool notEquip = true;

       		public void Equip(CharacterStats c, WeaponType weapon)
		{

            if(notEquip)
            {
			if (DamageBonus!= 0)
				c.Damage.AddModifier(new StatModifier(DamageBonus, StatModType.Flat, this));
			if (CritalChanceBonus != 0)
				c.CritalChance.AddModifier(new StatModifier(CritalChanceBonus, StatModType.Flat, this));
			if (CritalDamageBonus != 0)
				c.CritalDamage.AddModifier(new StatModifier(CritalDamageBonus, StatModType.Flat, this));
			if (AttackSpeedBonus != 0)
				c.AttackSpeed.AddModifier(new StatModifier(AttackSpeedBonus, StatModType.Flat, this));
            if (DefenseBonus != 0)
				c.Defense.AddModifier(new StatModifier(DefenseBonus, StatModType.Flat, this));

			if (DamagePercentBonus != 0)
				c.Damage.AddModifier(new StatModifier(DamagePercentBonus, StatModType.PercentMultiple, this));
			if (CritalChancePercentBonus != 0)
				c.CritalChance.AddModifier(new StatModifier(CritalChancePercentBonus, StatModType.PercentMultiple, this));
			if (CritalDamagePercentBonus != 0)
				c.CritalDamage.AddModifier(new StatModifier(CritalDamagePercentBonus, StatModType.PercentMultiple, this));
			if (AttackSpeedPercentBonus != 0)
				c.AttackSpeed.AddModifier(new StatModifier(AttackSpeedPercentBonus, StatModType.PercentMultiple, this));
            if (DefensePercentBonus != 0)
				c.Defense.AddModifier(new StatModifier(DefensePercentBonus, StatModType.Flat, this));
                notEquip = false;
                Debug.Log(c.Damage.Value);
            }
		}

		public void Unequip(CharacterStats c, WeaponType weapon)
		{
            if(notEquip == false)
			{
                c.Damage.RemoveAllModifiersFromSource(this);
			c.CritalChance.RemoveAllModifiersFromSource(this);
			c.CritalDamage.RemoveAllModifiersFromSource(this);
			c.AttackSpeed.RemoveAllModifiersFromSource(this);
            c.Defense.RemoveAllModifiersFromSource(this);
            notEquip = true;
            }
		} 
            

    }


