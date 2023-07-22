using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelSystem : MonoBehaviour
{

    //total xp needed to lvl
    [SerializeField] private float _xpToNextLvl;
    [SerializeField] float _expGrowth = .12f;
    
    //current running total
    [SerializeField] private float _currentXP;

    [SerializeField] private int _playerLvl;

    private GameObject _player; 
    private PlayerController PlayerController;

    [SerializeField] private float _attackIncrease = 1;
    [SerializeField] private float _defenseIncrease = 1;
    [SerializeField] private float _attackSpeedIncrease = .05f;
    [SerializeField] private float _critIncrease = .05f;

    private void Awake()
    {
        _player = GameObject.Find("Player");
        PlayerController = _player.GetComponent<PlayerController>();
    }

    private void Start()
    {
        _xpToNextLvl = 100;  
        _currentXP = 0;
        _playerLvl = 1;
    }

    public float GetXpToNextLvl()
    {
        return _xpToNextLvl;
    }
    public void SetXpToNextLvl(float xp)
    {
        _xpToNextLvl = xp;
    }
    public float GetTotalXp()
    {
        return _currentXP;
    }
    public void SetTotalXp(float i)
    {
        _currentXP = i;
    }
    public int GetPlayerLvl()
    {
        return _playerLvl;
    }
    private void LevelUP()
    {
        _playerLvl++;
        Debug. Log("Leveled Up!");

        //Health Modifier
        PlayerStats.SetMaxHP(100+(_playerLvl*25));   

        //Attack Modifier
        PlayerStats.SetAttack(PlayerStats.GetAttack() + _attackIncrease);

        //Defense Modifier
        PlayerStats.SetDefense(PlayerStats.GetDefense() + _defenseIncrease);

        //AttackSpeed Modifier
        PlayerStats.SetAttackSpeed(PlayerStats.GetAttackSpeed() + _attackSpeedIncrease);
        
        //Crit Modifier
        PlayerStats.SetCrit(PlayerStats.GetCrit() + _critIncrease);

        //Exp Curve
        SetXpToNextLvl((1+_expGrowth) * 100 * _playerLvl - 100);

        UIManager.Instance.UpdateValues();


        if(_currentXP>=_xpToNextLvl)
        {
            LevelUP();
        }
        else
        {
        UIManager.Instance.UpdateValues();
        }
    }

    public void GainExperience(int exp)
    {
        _currentXP += exp;

        if (_currentXP >= _xpToNextLvl)
            {
                _currentXP = _currentXP - _xpToNextLvl;
                LevelUP();
            }
        else 
            {
            UIManager.Instance.UpdateExpBar();
            }
            Debug.Log("Gained " + exp + "XP!");
    }
}
