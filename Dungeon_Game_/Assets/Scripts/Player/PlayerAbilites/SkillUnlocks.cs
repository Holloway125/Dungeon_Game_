using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUnlocks : MonoBehaviour
{
    private LevelSystem lvlSystem;
    private GameObject _player;
    int _skillCost;
    int _skillMax;
    int _skillMin;
    //Add new skills here and move the button onto SkillHolder Scriptable Object
    public List<Button> Skills;

    void Start()
    {   
        _player = GameObject.FindWithTag("Player");
        lvlSystem = _player.GetComponent<LevelSystem>();
    }

    void UnlockSkill()
    {
        _skillMin++;
        lvlSystem.SkillPoints = lvlSystem.SkillPoints - _skillCost;
    }

    public void Buy()
    {
        if(lvlSystem.SkillPoints >= _skillCost && _skillMin != _skillMax)
        {
            UnlockSkill();
        }
        else
        {
            Debug.Log("Not enough skill points!");
        }
    }

}
