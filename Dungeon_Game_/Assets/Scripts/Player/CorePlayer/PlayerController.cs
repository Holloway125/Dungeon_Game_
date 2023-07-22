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
    private Vector2 _moveInput;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera _camera;
    [SerializeField] private Animator _anim;
    private Rigidbody2D _rb;
    private CircleCollider2D _weaponCollider;
    public CapsuleCollider2D _capsuleCollider;
    public Vector3 MousePosition;
    public Vector3 MouseWorldPosition;
    public Vector3 DiffPos;
    public float RollSpeed;
    public bool CanReceiveInput;
    public bool InputReceived;
    private PlayerInput playerInput;
    private InputAction attack;
    private InputAction roll;
    private InputAction move;

    //UI_Elements
    private GameObject _UI;
    [Space]
    [Header ("UI Elements")]
    public GameObject YouLose;

    //health properties
    [Space]
    [Header ("Health Bar")]

    //stamina properties
    [Header ("Stamina Settings")]
    [SerializeField]public float RollCost = 25;
    [SerializeField]public float AttackCost = 15;
    [SerializeField]private float _staminaRegenRate = 1;
    private Coroutine _staminaRegening;

    [SerializeField] private bool _staminaRegenBool;
    [SerializeField] private bool isMoving;


    private void Awake()
    {
        playerControls = new PlayerActions();
        playerInput = GetComponent<PlayerInput>();
        attack = playerInput.actions["Attack"];
        roll = playerInput.actions["Roll"];
        move = playerInput.actions["Movement"];
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _weaponCollider = GetComponent<CircleCollider2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        playerStats = GetComponent<CharacterStats>();
        _UI = GameObject.FindGameObjectWithTag("UI");
        YouLose = GameObject.Find("/PlayerUI/GameSettings/YouLoseCanvas/YouLosePanel");
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
        if(playerStats.GetCurrentStam() < playerStats.GetMaxStam() && _staminaRegenBool == false)
        {
            _staminaRegening = StartCoroutine(StaminaRegen());
        }
    }
    public IEnumerator StaminaRegen()
    {
        _staminaRegenBool = true;
        yield return new WaitForSeconds(2);
        while (playerStats.GetCurrentStam() < playerStats.GetMaxStam())
        {
            yield return new WaitForSeconds(.05f);
            playerStats.SetCurrentStam(playerStats.GetCurrentStam() + _staminaRegenRate);
        }
        _staminaRegenBool = false;
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
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //YouLose.SetActive(true);
    }
    private void Attack(InputAction.CallbackContext context)
    {
        if(CanReceiveInput == true)
        {
            InputReceived = true;
            MousePosition = Mouse.current.position.ReadValue();
            MousePosition.z = _camera.nearClipPlane + 1;
            MouseWorldPosition = _camera.ScreenToWorldPoint(MousePosition);
            DiffPos = MouseWorldPosition - player.transform.position;
        }
        else if(CanReceiveInput == false && InputReceived == false)
        {
            if(playerStats.GetCurrentStam() > 0)
            {
            StopCoroutine(_staminaRegening);
            _staminaRegenBool = false;
            MousePosition = Mouse.current.position.ReadValue();
            MousePosition.z = _camera.nearClipPlane + 1;
            MouseWorldPosition = _camera.ScreenToWorldPoint(MousePosition);
            DiffPos = MouseWorldPosition - player.transform.position;
            _anim.SetFloat("MouseX",DiffPos.x);
            _anim.SetFloat("MouseY",DiffPos.y);
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
        if(playerStats.GetCurrentStam() >= RollCost)
        {
            MousePosition = Mouse.current.position.ReadValue();
            MousePosition.z = _camera.nearClipPlane + 1;
            MouseWorldPosition = _camera.ScreenToWorldPoint(MousePosition);
            DiffPos = MouseWorldPosition - player.transform.position;
            RollSpeed = 10;
            StopCoroutine(_staminaRegening);
            _staminaRegenBool = false;
            playerStats.SetCurrentStam(playerStats.GetCurrentStam() - RollCost);
            _anim.SetFloat("MouseX",DiffPos.x);
            _anim.SetFloat("MouseY",DiffPos.y);
            _anim.SetTrigger("Roll");         
        }
        else if(playerStats.GetCurrentStam() < RollCost && playerStats.GetCurrentStam() > 0)
        {
            MousePosition = Mouse.current.position.ReadValue();
            MousePosition.z = _camera.nearClipPlane + 1;
            MouseWorldPosition = _camera.ScreenToWorldPoint(MousePosition);
            DiffPos = MouseWorldPosition - player.transform.position;
            RollSpeed = 10;
            StopCoroutine(_staminaRegening);
            _staminaRegenBool = false;
            playerStats.SetCurrentStam(0);
            _anim.SetFloat("MouseX",DiffPos.x);
            _anim.SetFloat("MouseY",DiffPos.y);
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
        _rb.velocity = _moveInput * playerStats.GetSpeed();
        _anim.SetFloat("Horizontal",_rb.velocity.x);
        _anim.SetFloat("Vertical",_rb.velocity.y);

        if(_rb.velocity.x != 0f)
        {
            isMoving = true;
        }
        else if (_rb.velocity.y != 0f)
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
    //     MousePosition = Mouse.current.position.ReadValue();

    //     //Sets screen position z to the near viewport of camera so it can then be translated correctly into mouse world postion
    //     MousePosition.z = _camera.nearClipPlane + 1;

    //     //returns world position location of mouse in the Scene
    //     MouseWorldPosition = _camera.ScreenToWorldPoint(MousePosition);

    //     //returns a new vector3 that is the mouses position relative to the player of the scene
    //     Vector3 DiffPos = MouseWorldPosition - player.transform.position;

    //     //returns a radian that can be used to convert to degrees 
    //     angle = Mathf.Atan2(DiffPos.y, DiffPos.x);

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
    //     MousePosition = Mouse.current.position.ReadValue();
    //     MousePosition.z = _camera.nearClipPlane + 1;
    //     MouseWorldPosition = _camera.ScreenToWorldPoint(MousePosition);
    //     Vector3 DiffPos = MouseWorldPosition - player.transform.position;
    //     angle = Mathf.Atan2(DiffPos.y, DiffPos.x);
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
