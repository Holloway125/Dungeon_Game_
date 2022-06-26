using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnim : MonoBehaviour
{
    public Animator weaponAnim;
    [SerializeField]
    GameObject player;
    WeaponRotation wR;

    // When the object is turned on using the toggle in the inv menu the void awake will run and set wR to the WeaponRotation Script on the player
    void Awake()
    {
        wR = player.GetComponent<WeaponRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            WeaponAttack(weaponAnim, "swordswing");
        }

    } 
        //function that will take the animator of each weapon and then the name of the animation to play it when Q is pressed in update
        public void WeaponAttack(Animator anim, string animation)
    {
        wR.SetAnimationRotation();
        anim.Play(animation);

    }

}
