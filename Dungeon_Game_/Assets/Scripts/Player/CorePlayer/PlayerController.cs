using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //GameObject and Component References
    private PlayerActions playerControls;
    private CharacterStats playerStats;
    private Rigidbody2D _rBody;
    private Vector2 _moveInput;
    private CircleCollider2D _weaponCollider;
    private CapsuleCollider2D _capsuleCollider;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera Camera;
    [SerializeField] private Animator _anim;
    private Vector3 mousePosition;
    private Vector3 mouseWorldPosition;
    private Vector3 mouseDir;
    private float rollSpeed;
    private Vector3 rollDir;
    public bool canReceiveInput;
    public bool inputReceived;
    private float angle;
    private float angleDegree;
    private PlayerInput playerInput;
    private InputAction attack;
    private InputAction roll;
    private InputAction move;

    [SerializeField] private State state;

    [SerializeField] private Rigidbody2D rb;

    //UI_Elements
    private GameObject _UI;
    [Space]
    [Header ("UI Elements")]
    public GameObject _youLose;

    //health properties
    [Space]
    [Header ("Health Bar")]

    //stamina properties
    [Header ("Stamina Settings")]
    [SerializeField]public float rollCost = .25f;
    [SerializeField]public float attackCost = .15f;
    [SerializeField]private float staminaRegenRate = 1;
    private Coroutine _staminaRegening;

    [SerializeField] private bool StaminaRegening;

    private enum State
    {
        Normal,
        Attacking,
        Rolling,
    }

    //PlayerStats and Conditions
    [SerializeField] private bool isMoving;

    private void Awake()
    {
        playerControls = new PlayerActions();
        playerInput = GetComponent<PlayerInput>();
        attack = playerInput.actions["Attack"];
        roll = playerInput.actions["Roll"];
        move = playerInput.actions["Movement"];
        _rBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _weaponCollider = GetComponent<CircleCollider2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        playerStats = GetComponent<CharacterStats>();
        _UI = GameObject.FindGameObjectWithTag("UI");
        _youLose = GameObject.Find("/PlayerUI/GameSettings/YouLoseCanvas/YouLosePanel");
    }

    private void Start()
    {
        playerControls.Player_Map.Attack.performed += Attack;
        playerControls.Player_Map.Roll.performed += Roll;
        _anim = GetComponent<Animator>();
        playerStats.SetSpeed(playerStats.GetDefaultSpeed());
        _anim.SetBool("Death", false);
        playerStats.SetCurrentStam(playerStats.GetMaxStam());
    }

    private void OnEnable()
    {
        playerControls.Player_Map.Enable();
        playerControls.Player_Map.Attack.performed += Attack;
        playerControls.Player_Map.Roll.performed += Roll;
    }

    private void OnDisable()
    {
        playerControls.Player_Map.Disable();
        playerControls.Player_Map.Attack.performed -= Attack;
        playerControls.Player_Map.Roll.performed -= Roll;
    }

    private void Update()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.nearClipPlane + 1;
        mouseWorldPosition = Camera.ScreenToWorldPoint(mousePos);
        mouseDir = mouseWorldPosition - player.transform.position;
        if(playerStats.GetCurrentStam() < playerStats.GetMaxStam() && StaminaRegening == false)
        {
            Debug.Log("Called StaminaRegen in Update!");
            _staminaRegening = StartCoroutine(StaminaRegen());
        }
    }

    private void FixedUpdate() 
    {
        switch(state)
        {
            case State.Normal:
            Move();
            break;

            // case State.Attacking:

            // break;

            case State.Rolling:
            Rolling();
            break;
        }
    }
    public IEnumerator StaminaRegen()
    {
        while (playerStats.GetCurrentStam() < playerStats.GetMaxStam())
        {
            Debug.Log("Called Stamina Regen");
            StaminaRegening = true;
            yield return new WaitForSeconds(2);
            playerStats.SetCurrentStam(playerStats.GetCurrentStam() + staminaRegenRate);
        }
        StaminaRegening = false;
    }

    public void TimeStop() // Needed for death Animation
    {
        Time.timeScale = 0f;
    }

    public void TakeDamage(float damage) // input the amount of damage you want the player to take on each monster
    {
        if (_anim.GetBool("Death") == false)
        {
            if(playerStats.GetCurrentHP() > damage)
            {
            playerStats.SetCurrentHP(playerStats.GetCurrentHP()-damage);
            }

            else if (playerStats.GetCurrentHP() <= damage)
            {
                Death();
            }
        }
    }

    public void Death() //sets current health to 0 plays death animation puts up return to title canvas and stops all movement
    {
        _anim.Play("death");
        _anim.SetBool("Death", true);
        playerStats.SetCurrentHP(0);
        //play death animation
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //_youLose.SetActive(true);
    }

    private void Move()
    {
        _moveInput = playerControls.Player_Map.Movement.ReadValue<Vector2>(); 
        _rBody.velocity = _moveInput * playerStats.GetSpeed();
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
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if(canReceiveInput)
        {
            inputReceived = true;
        }
        else if(!canReceiveInput && playerStats.GetCurrentStam() >= attackCost)
        {
            _anim.SetTrigger($"{AttackDir()}AttackOne");
            StopCoroutine(_staminaRegening);
            StaminaRegening = false;
            Debug.Log($"{AttackDir()}Attack");
        }
    }
    private void Roll(InputAction.CallbackContext context)
    {
        if(playerStats.GetCurrentStam() >= rollCost)
        {
            rollDir = mouseDir;
            rollSpeed = 10;
            playerStats.SetCurrentStam(playerStats.GetCurrentStam() - rollCost);
            _anim.SetTrigger($"{RollDir()}Roll");
            Debug.Log($"{RollDir()} Roll");            
            state = State.Rolling;
            StopCoroutine(_staminaRegening);
            StaminaRegening = false;
        }
        else
        {
            state = State.Normal;
        }
    }

    private void Rolling()
    {
        transform.position += rollDir * rollSpeed * Time.deltaTime;
        rollSpeed -= rollSpeed * 10f * Time.deltaTime;
        _capsuleCollider.enabled = false;

        if(rollSpeed < 5f)
        {
            playerStats.SetSpeed(playerStats.GetDefaultSpeed());
            _capsuleCollider.enabled = true;
            state = State.Normal;
        }
    }

    public void InputManager()
    {
        if(!canReceiveInput)
        {
            canReceiveInput = true;
        }
        else if (canReceiveInput)
        {
            canReceiveInput = false;
        }
    }

    public string AttackDir()
    {
        //Gets mouse position and returns x,y value of the pixels mouse is on in current resolution
        mousePosition = Mouse.current.position.ReadValue();

        //Sets screen position z to the near viewport of camera so it can then be translated correctly into mouse world postion
        mousePosition.z = Camera.nearClipPlane + 1;

        //returns world position location of mouse in the Scene
        mouseWorldPosition = Camera.ScreenToWorldPoint(mousePosition);

        //returns a new vector3 that is the mouses position relative to the player of the scene
        Vector3 diffPos = mouseWorldPosition - player.transform.position;

        //returns a radian that can be used to convert to degrees 
        angle = Mathf.Atan2(diffPos.y, diffPos.x);

        //converts the radian to degrees in the range of (-180, 180)
        angleDegree = angle * Mathf.Rad2Deg;

        //converts the range of (-180, 180) to a range of (0, 360) which then can be passed into the rotation z value of an object to set it rotation pointing to the cursors current position
        if(angleDegree < 0)
        {
            angleDegree+=360;           
        }   
        if (angleDegree >= 0  && angleDegree <= 22.5 || angleDegree <= 360 && angleDegree >=337.5)
        {
            return "East";
        }
        else if (angleDegree > 22.5 && angleDegree <= 67.5)
        {
            return "NorthEast";
        }
        else if (angleDegree > 67.5 && angleDegree <= 112.5)
        {
            return "North";
        }
        else if (angleDegree > 112.5 && angleDegree <= 157.5)
        {
            return "NorthWest";
        }
        else if (angleDegree > 157.5 && angleDegree <= 202.5)
        {
            return "West";
        }     
        else if (angleDegree > 202.5 && angleDegree <= 247.5)
        {
            return "SouthWest";
        }      
        else if (angleDegree > 247.5 && angleDegree <= 292.5)
        {
            return "South";
        }    
        else if (angleDegree > 292.5 && angleDegree <= 337.5)
        {
            return "SouthEast" ;
        }        
        else return "NoMouse";
    }

    public string RollDir()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = Camera.nearClipPlane + 1;
        mouseWorldPosition = Camera.ScreenToWorldPoint(mousePosition);
        Vector3 diffPos = mouseWorldPosition - player.transform.position;
        angle = Mathf.Atan2(diffPos.y, diffPos.x);
        angleDegree = angle * Mathf.Rad2Deg;
        if(angleDegree < 0)
        {
            angleDegree+=360;           
        }   
        if (angleDegree >= 0  && angleDegree <= 67.5 || angleDegree <= 360 && angleDegree >=292.5)
        {
            return "East";
        }
        else if (angleDegree > 67.5 && angleDegree <= 112.5)
        {
            return "North";
        }
        else if (angleDegree > 112.5 && angleDegree <= 247.5)
        {
            return "West";
        }     
        else if (angleDegree > 247.5 && angleDegree <= 292.5)
        {
            return "South";
        }    
        else return "NoMouse";
    }

    //Deals Damage to Monsters
    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.gameObject.TryGetComponent<BaseEnemy>(out BaseEnemy monster))
        {
            monster.TakeDamage((int)playerStats.GetAttack());
            Debug.Log("Monster took " + playerStats.GetAttack() + " damage!");
        }
    }
}
