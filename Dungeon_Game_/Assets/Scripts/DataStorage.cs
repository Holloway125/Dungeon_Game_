using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataStorage
{
    //Store Data here to be changed and accessed between scenes
    public static int _PlayerLvl {get; set;}
    public static int _PlayerExp {get; set;}
    public static float _TimeLeft {get;set;}

    // public static void SetTime(float i)
    // {
    //     _TimeLeft = i;
    // }    
}
