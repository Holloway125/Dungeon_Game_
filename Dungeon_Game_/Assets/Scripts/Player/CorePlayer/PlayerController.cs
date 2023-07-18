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
    public CapsuleCollider2D _capsuleCollider;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera Camera;
    [SerializeField] private Animator _anim;
    public Vector3 mousePosition;
    public Vector3 mouseWorldPosition;
    public Vector3 diffPos;
    public float rollSpeed;
    public bool canReceiveInput;
    public bool inputReceived;
    private PlayerInput playerInput;
    private InputAction attack;
    private InputAction roll;
    private InputAction move;

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

    [SerializeField] private bool StaminaRegenBool;
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
        playerStats.SetCurrentHP(playerStats.GetMaxHP());
        _staminaRegening = StartCoroutine(StaminaRegen());
        StopCoroutine(_staminaRegening);
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
    private void FixedUpdate() 
    {
        Move();
        if(playerStats.GetCurrentStam() < playerStats.GetMaxStam() && StaminaRegenBool == false)
        {
            _staminaRegening = StartCoroutine(StaminaRegen());
        }
    }
    public IEnumerator StaminaRegen()
    {
        StaminaRegenBool = true;
        yield return new WaitForSeconds(2);
        while (playerStats.GetCurrentStam() < playerStats.GetMaxStam())
        {
            yield return new WaitForSeconds(.05f);
            playerStats.SetCurrentStam(playerStats.GetCurrentStam() + staminaRegenRate);
        }
        StaminaRegenBool = false;
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
    private void Attack(InputAction.CallbackContext context)
    {
        if(canReceiveInput == true)
        {
            inputReceived = true;
            mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = Camera.nearClipPlane + 1;
            mouseWorldPosition = Camera.ScreenToWorldPoint(mousePosition);
            diffPos = mouseWorldPosition - player.transform.position;
        }
        else if(canReceiveInput == false && inputReceived == false)
        {
            if(playerStats.GetCurrentStam() > 0)
            {
            StopCoroutine(_staminaRegening);
            StaminaRegenBool = false;
            mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = Camera.nearClipPlane + 1;
            mouseWorldPosition = Camera.ScreenToWorldPoint(mousePosition);
            diffPos = mouseWorldPosition - player.transform.position;
            _anim.SetFloat("MouseX",diffPos.x);
            _anim.SetFloat("MouseY",diffPos.y);
            _anim.SetTrigger("FirstAttack");
            }
            else if(playerStats.GetCurrentStam() == 0)
            {
                Debug.Log("Not enough Stamina!");
            }
        }
    }
    private void Roll(InputAction.CallbackContext context)
    {
        if(playerStats.GetCurrentStam() >= rollCost)
        {
            mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = Camera.nearClipPlane + 1;
            mouseWorldPosition = Camera.ScreenToWorldPoint(mousePosition);
            diffPos = mouseWorldPosition - player.transform.position;
            rollSpeed = 10;
            StopCoroutine(_staminaRegening);
            StaminaRegenBool = false;
            playerStats.SetCurrentStam(playerStats.GetCurrentStam() - rollCost);
            _anim.SetFloat("MouseX",diffPos.x);
            _anim.SetFloat("MouseY",diffPos.y);
            _anim.SetTrigger("Roll");         
        }
        else if(playerStats.GetCurrentStam() < rollCost && playerStats.GetCurrentStam() > 0)
        {
            mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = Camera.nearClipPlane + 1;
            mouseWorldPosition = Camera.ScreenToWorldPoint(mousePosition);
            diffPos = mouseWorldPosition - player.transform.position;
            rollSpeed = 10;
            StopCoroutine(_staminaRegening);
            StaminaRegenBool = false;
            playerStats.SetCurrentStam(0);
            _anim.SetFloat("MouseX",diffPos.x);
            _anim.SetFloat("MouseY",diffPos.y);
            _anim.SetTrigger("Roll");          
        }
        else if(playerStats.GetCurrentStam() == 0)
        {
            Debug.Log("Not enough Stamina!");
        }
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
    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.gameObject.TryGetComponent<BaseEnemy>(out BaseEnemy monster))
        {
            monster.TakeDamage((int)playerStats.GetAttack());
            Debug.Log("Monster took " + playerStats.GetAttack() + " damage!");
        }
    }
    // public string AttackDir()
    // {
    //     //Gets mouse position and returns x,y value of the pixels mouse is on in current resolution
    //     mousePosition = Mouse.current.position.ReadValue();

    //     //Sets screen position z to the near viewport of camera so it can then be translated correctly into mouse world postion
    //     mousePosition.z = Camera.nearClipPlane + 1;

    //     //returns world position location of mouse in the Scene
    //     mouseWorldPosition = Camera.ScreenToWorldPoint(mousePosition);

    //     //returns a new vector3 that is the mouses position relative to the player of the scene
    //     Vector3 diffPos = mouseWorldPosition - player.transform.position;

    //     //returns a radian that can be used to convert to degrees 
    //     angle = Mathf.Atan2(diffPos.y, diffPos.x);

    //     //converts the radian to degrees in the range of (-180, 180)
    //     angleDegree = angle * Mathf.Rad2Deg;

    //     //converts the range of (-180, 180) to a range of (0, 360) which then can be passed into the rotation z value of an object to set it rotation pointing to the cursors current position
    //     if(angleDegree < 0)
    //     {
    //         angleDegree+=360;           
    //     }   
    //     if (angleDegree >= 0  && angleDegree <= 22.5 || angleDegree <= 360 && angleDegree >=337.5)
    //     {
    //         return "East";
    //     }
    //     else if (angleDegree > 22.5 && angleDegree <= 67.5)
    //     {
    //         return "NorthEast";
    //     }
    //     else if (angleDegree > 67.5 && angleDegree <= 112.5)
    //     {
    //         return "North";
    //     }
    //     else if (angleDegree > 112.5 && angleDegree <= 157.5)
    //     {
    //         return "NorthWest";
    //     }
    //     else if (angleDegree > 157.5 && angleDegree <= 202.5)
    //     {
    //         return "West";
    //     }     
    //     else if (angleDegree > 202.5 && angleDegree <= 247.5)
    //     {
    //         return "SouthWest";
    //     }      
    //     else if (angleDegree > 247.5 && angleDegree <= 292.5)
    //     {
    //         return "South";
    //     }    
    //     else if (angleDegree > 292.5 && angleDegree <= 337.5)
    //     {
    //         return "SouthEast" ;
    //     }        
    //     else return "NoMouse";
    // }

    // public string RollDir()
    // {
    //     mousePosition = Mouse.current.position.ReadValue();
    //     mousePosition.z = Camera.nearClipPlane + 1;
    //     mouseWorldPosition = Camera.ScreenToWorldPoint(mousePosition);
    //     Vector3 diffPos = mouseWorldPosition - player.transform.position;
    //     angle = Mathf.Atan2(diffPos.y, diffPos.x);
    //     angleDegree = angle * Mathf.Rad2Deg;
    //     if(angleDegree < 0)
    //     {
    //         angleDegree+=360;           
    //     }   
    //     if (angleDegree >= 0  && angleDegree <= 67.5 || angleDegree <= 360 && angleDegree >=292.5)
    //     {
    //         return "East";
    //     }
    //     else if (angleDegree > 67.5 && angleDegree <= 112.5)
    //     {
    //         return "North";
    //     }
    //     else if (angleDegree > 112.5 && angleDegree <= 247.5)
    //     {
    //         return "West";
    //     }     
    //     else if (angleDegree > 247.5 && angleDegree <= 292.5)
    //     {
    //         return "South";
    //     }    
    //     else return "NoMouse";
    // }

    //Deals Damage to Monsters
}
