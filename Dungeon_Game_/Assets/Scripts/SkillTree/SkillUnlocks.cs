using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillUnlocks : MonoBehaviour
{
    [SerializeField]
    private int _skillCost;

    private LevelSystem lvlSystem;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        lvlSystem = _player.GetComponent<LevelSystem>();
    }

    void UnlockSkill()
    {
        lvlSystem.SkillPoints = lvlSystem.SkillPoints - _skillCost;
    }

    public void Buy()
    {

        if(lvlSystem.SkillPoints >= _skillCost)
        {
            UnlockSkill();
        }
        else
        {
            Debug.Log("Not enough skill points!");
        }
    }

}
