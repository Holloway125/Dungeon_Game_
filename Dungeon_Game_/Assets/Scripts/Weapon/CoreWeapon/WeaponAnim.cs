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

    void Awake()
    {
        WR = Player.GetComponent<WeaponRotation>();
        _collider = GetComponent<EdgeCollider2D>();
        c = Player.GetComponent<CharacterStats>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !weaponAnim.GetBool("attacking"))
        {
            WeaponAttack(weaponAnim, WR.MouseRotation());
        }
    } 
    
    //function that will take the animator of each weapon and then the name 
    //of the animation to play it when Q is pressed in update

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
