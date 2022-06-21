using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;
    private void Awake() => skillTree = this;

    public int[] SkillLevels;
    public int[] SkillCaps;
    public string[] SkillNames;
    public string[] SkillDescriptions;

    public List<Skill> SkillList;
    public GameObject SkillHolder;

    public List<GameObject> ConnectorList;
    public GameObject ConnectorHolder;

    public int skillPoints;

    private void Start()
    {

        SkillLevels = new int[20];
        SkillCaps = new[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, };

        SkillNames = new [] {"SkillName", "SkillName", "SkillName", "SkillName", "SkillName", "SkillName", "SkillName", "SkillName", "SkillName", "SkillName", "SkillName", "SkillName", "SkillName", "SkillName", "SkillName", "SkillName", "SkillName", "SkillName", "SkillName", "SkillName", };// Skill Names go here
        SkillDescriptions = new[]//This is where you would put Descriptions for the skill
        {
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
            "PlaceHolder",
        };

        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill);
        foreach (var connector in ConnectorHolder.GetComponentsInChildren<RectTransform>()) ConnectorList.Add(connector.gameObject);


        for (var i = 0; i < SkillList.Count; i++) SkillList[i].id = i;

        SkillList[0].ConnectedSkills = new[] {1,2,3};
        SkillList[1].ConnectedSkills = new[] {6,7};
        SkillList[2].ConnectedSkills = new[] {8,9};
        SkillList[3].ConnectedSkills = new[] {4,5};
        SkillList[4].ConnectedSkills = new[] {12};
        SkillList[5].ConnectedSkills = new[] {12};
        SkillList[6].ConnectedSkills = new[] {11};
        SkillList[7].ConnectedSkills = new[] {11};
        SkillList[8].ConnectedSkills = new[] {10};
        SkillList[9].ConnectedSkills = new[] {10};
        SkillList[10].ConnectedSkills = new[] {13};
        SkillList[11].ConnectedSkills = new[] {13};
        SkillList[12].ConnectedSkills = new[] {13};


        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI()//Will UpdateSkillTree UI on Start and when skill.Buy() is called
    {
        foreach(var skill in SkillList) skill.UpdateUI();
    }
}
