using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using static SkillTree;

public class Skill : MonoBehaviour
{
    public int id;

    public TMP_Text TitleText;
    public TMP_Text DescriptionText;

    public int[] ConnectedSkills;

    public void UpdateUI()
    {
        TitleText.text = $"{skillTree.SkillNames[id]}";
        DescriptionText.text = $"{skillTree.SkillDescriptions[id]}";

        GetComponent<Image>().color = skillTree.SkillLevels[id] >= skillTree.SkillCaps[id] ? Color.yellow
            : skillTree.SkillPoints > 0 ? Color.green : Color.white;

            foreach(var connectedSkill in ConnectedSkills)
            {
                skillTree.SkillList[connectedSkill].gameObject.SetActive(skillTree.SkillLevels[id] > 0);
                skillTree.ConnectorList[connectedSkill].SetActive(skillTree.SkillLevels[id] > 0);
            }
    }

    public void Buy()
    {
        if(skillTree.SkillPoints < 1 || skillTree.SkillLevels[id] >= skillTree.SkillCaps[id]) return;
        skillTree.SkillPoints -= 1;
        skillTree.SkillLevels[id]++;
        skillTree.UpdateAllSkillUI();

    }
}
