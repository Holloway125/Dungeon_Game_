using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnim : MonoBehaviour
{
    [SerializeField]
    Animator weaponAnim;
    BaseEnemy monster;
    [SerializeField]
    GameObject Player;
    WeaponRotation WR;
    EdgeCollider2D _collider;
    CharacterStats c;


    // When the object is turned on using the toggle in the inv menu the void awake will run and set WR to the WeaponRotation Script on the Player
    void Awake()
    {
        WR = Player.GetComponent<WeaponRotation>();
        _collider = GetComponent<EdgeCollider2D>();
        c = Player.GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !weaponAnim.GetBool("attacking"))
        {
            WeaponAttack(weaponAnim, WR.MouseRotation());
        }
    } 
        //function that will take the animator of each weapon and then the name of the animation to play it when Q is pressed in update
        public void WeaponAttack(Animator anim, string animation)
    {
        anim.Play($"{animation}Attack");
    }

        void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.gameObject.TryGetComponent<BaseEnemy>(out BaseEnemy monster))
        {
            monster.TakeDamage((int)c.Damage.Value);
        }
    }

}
