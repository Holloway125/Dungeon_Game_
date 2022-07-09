using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField]
    private float _enemyHealth;
    public float EnemyHealth
    {
    get {return _enemyHealth;}
    set {_enemyHealth = EnemyHealth;}
    }
    
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _checkRadius;
    [SerializeField]
    private float _attackRadius;
    [SerializeField]
    private int _challengeLevel;
    public GameObject GameObject;
    private LevelSystem _levelSystem;
    public bool ShouldRotate;

    //Make sure Player is on Player Layer in Inspector
    public LayerMask WhatIsPlayer; 
    private Transform _target;
    private Rigidbody2D _rb;

    //Defines properties on start
    private Animator _anim;
    private Vector2 _movement;
    private Vector2 _dir;
    private bool _isInChaseRange;
    private bool _isInAttackRange;

        protected virtual void Start()
    {
        _levelSystem = GameObject.FindWithTag("Player").GetComponent<LevelSystem>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _target = GameObject.FindWithTag("Player").transform;
        if(_challengeLevel >= 0 )
        {
            _challengeLevel = 1;
        }
    }

        protected virtual void Update()
    {
        _anim.SetBool("isMoving", _isInChaseRange);
        _isInChaseRange = Physics2D.OverlapCircle(transform.position, _checkRadius, WhatIsPlayer);
        _isInAttackRange = Physics2D.OverlapCircle(transform.position, _attackRadius, WhatIsPlayer);

        _dir = _target.position - transform.position;
        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        _dir.Normalize();
        _movement = _dir;
        // if _shouldRotate is checked in inspector this will run if unchecked this will not run
        if(ShouldRotate)
        {
            _anim.SetFloat("X", _dir.x);
            _anim.SetFloat("Y", _dir.y);
        }

    }

        protected virtual void FixedUpdate()
    {
         if(_isInChaseRange && !_isInAttackRange)
        {           
             MoveEnemy(_movement);
        }
         if(_isInAttackRange)
        {
            _rb.velocity = Vector2.zero;
        }
    }

        private void MoveEnemy(Vector2 _dir)
    {
        _rb.MovePosition((Vector2)transform.position + (_dir * _speed * Time.deltaTime));
    }

    public virtual void TakeDamage(int damageAmount)
    {
        EnemyHealth -= damageAmount;
        if(EnemyHealth <= 0)
        {
            _anim.SetBool("Death", true);
        }
    }

    protected virtual void Death()
    {
        _levelSystem.GainExperience(_challengeLevel+_levelSystem.playerLvl*100);
        Destroy(gameObject);
    }
}
