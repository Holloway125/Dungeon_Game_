using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : BaseEnemy
{

 // this is how you would override the funtions and add code
protected override void Start()
{
// if you want to keep all functionality of the inherited classes function this is how you would keep it
base.Start();
// then you can add whatever code you'd like to the function
Debug.Log("I am a Slime");
}
}
