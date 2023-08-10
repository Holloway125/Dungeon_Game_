using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossLogic : MonoBehaviour

{
    [SerializeField]
    FirstBossStats Stats;
    [SerializeField]
    Animator anim;
    [SerializeField]
    string[] movestring;
    string DoIt;


    public string MoveSelector()
    {
        return movestring[Random.Range(0, movestring.Length)];
    }

    public void WhatToDo()
    {
        anim.SetTrigger(MoveSelector());
    }
    
    // public void LeftPunch()
    // {
    //     anim.SetTrigger("LeftPunch");
    // }

    //     public void RightPunch()
    // {
    //     anim.SetTrigger("RightPunch");
    // }
    //     public void HeadButt()

    // {       
    //     anim.SetTrigger("HeadButt");
    // }
    
    //     public void FireBreath()
    // {
    //     anim.SetTrigger("FireBreath");
    // } 


}
