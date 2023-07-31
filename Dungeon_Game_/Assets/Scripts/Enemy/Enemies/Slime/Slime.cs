using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : BaseEnemy
{


[Header ("Slime Specific")]
[SerializeField]
float JumpForce;
 // this is how you would override the funtions and add code


protected override void Start()
{
// if you want to keep all functionality of the inherited classes function this is how you would keep it
base.Start();
// then you can add whatever code you'd like to the function
}


    // public void LeapAbility()
    // {
    //     var enemy = GetComponent<BaseEnemy>();
    //     Vector2 Direction = new Vector2(enemy.Player.transform.position.x - this.transform.position.x, enemy.Player.transform.position.y - this.transform.position.y).normalized;
    //     this.Rb.AddForce(Direction * JumpForce);
    // }

    // public override void TakeDamage(int damageAmount)
    // {
    //     base.TakeDamage(damageAmount);
    // }

}
