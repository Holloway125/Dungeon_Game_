using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills
{
    public enum SkillType {
        BackStab,
    }

    private List<SkillType> unlockedSkillTypeList;

    public PlayerSkills(){
        unlockedSkillTypeList = new List<SkillType>();
    }
    
    public void UnlockSkill(SkillType skillType) {
        unlockedSkillTypeList.Add(skillType);
    }

    public bool IsSkillUnlocked(SkillType skillType) {
        return unlockedSkillTypeList.Contains(skillType);
    }
}
