using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //GameObject and Component References
    private PlayerActions _playerActions;
    private Rigidbody2D _rBody;
    private Vector2 _moveInput;
    private CircleCollider2D _weaponCollider;
    private CharacterStats _characterStats;
    [SerializeField] private GameObject player;
    private Animator _anim;

    //PlayerStats and Conditions
    
    [SerializeField] private float currentSpeed;

    [SerializeField] private bool isMoving;

    private void Awake()
    {
        _playerActions = new PlayerActions();
        _rBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _weaponCollider = GetComponent<CircleCollider2D>();
        _characterStats = GetComponent<CharacterStats>();
    }

    private void Start()
    {
        _playerActions.Player_Map.Attack.performed += context => CombatManager.instance.Attack(context);
        _anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }

    private void FixedUpdate() 
    {
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>(); 
        _rBody.velocity = _moveInput * currentSpeed;
        _anim.SetFloat("Horizontal",_rBody.velocity.x);
        _anim.SetFloat("Vertical",_rBody.velocity.y);

        if(_rBody.velocity.x != 0f)
        {
            isMoving = true;
        }
        else if (_rBody.velocity.y != 0f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        _anim.SetBool("isMoving", isMoving);

        if(CombatManager.instance.inputReceived)
        {
            _anim.SetTrigger($"{CombatManager.instance.MouseRotation()}AttackOne");       
            CombatManager.instance.InputManager();
            CombatManager.instance.inputReceived = false;
        }

     
    }
    public void SetCurrentSpeed(float i)
    {
        if(i >= 0 && i <=25)
        {
            currentSpeed = i;
        }
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    //Deals Damage to Monsters
    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.gameObject.TryGetComponent<BaseEnemy>(out BaseEnemy monster))
        {
            monster.TakeDamage((int)_characterStats.Damage.Value);
        }
    }
    public void SetTransitionAnimation()
    {
        
    }

}
