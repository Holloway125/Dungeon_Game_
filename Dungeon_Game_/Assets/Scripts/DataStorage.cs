using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataStorage
{
    //Store Data here to be changed and accessed between scenes
    public static int _PlayerLvl {get; set;}
    public static int _PlayerExp {get; set;}
    public static float _TimeLeft {get; set;}
    private static float _DefaultSpeed;

    public static void SetDefaultSpeed(float i)
    {
        _DefaultSpeed = i;
    }

    public static float GetDefaultSpeed()
    {
        return _DefaultSpeed;
    }
    // public static int _PlayerLvl {get; set;}
    // public static int _PlayerLvl {get; set;}
}
