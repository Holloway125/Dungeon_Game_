using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //GameObject and Component References
    private PlayerActions playerControls;
    private Vector2 _moveInput;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera _camera;
    [SerializeField] private Animator _anim;
    public Rigidbody2D Rb;
    public BoxCollider2D _weaponCollider;
    public PolygonCollider2D _hitBox;
    public BoxCollider2D _walkBox;
    public Vector3 MousePosition;
    public Vector3 MouseWorldPosition;
    public Vector2 DiffPos;
    public float RollSpeed;

    [Space]
    [Header ("Health Bar")]

    [Header ("Stamina Settings")]
    [SerializeField]public float RollCost = 25;
    [SerializeField]public float AttackCost = 15;
    [SerializeField]private float _staminaRegenRate = 1;
    private Coroutine _staminaRegening;
    public bool CanReceiveInput;
    public bool InputReceived;
    private bool _staminaRegenBool;
    private bool isMoving;
    public bool IsRolling;


    private void Awake()
    {
        playerControls = new PlayerActions();
        Rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _hitBox = GetComponentInChildren<PolygonCollider2D>();
        _walkBox = GetComponent<BoxCollider2D>();
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
    }
    private void Start()
    {
        _anim = GetComponent<Animator>();
        PlayerStats.SetSpeed(PlayerStats.GetDefaultSpeed());
        _anim.SetBool("Death", false);
        PlayerStats.SetCurrentStam(PlayerStats.GetMaxStam());
        PlayerStats.SetCurrentHP(PlayerStats.GetMaxHP());
        _staminaRegening = StartCoroutine(StaminaRegen());
        StopCoroutine(_staminaRegening);
    }
    private void OnEnable()
    {
        playerControls.Player_Map.Enable();
    }
    private void OnDisable()
    {
        playerControls.Player_Map.Disable();
    }

    private void FixedUpdate() 
    {
        Move();
        Attack();
        Roll();
        if(PlayerStats.GetCurrentStam() < PlayerStats.GetMaxStam() && _staminaRegenBool == false)
        {
            _staminaRegening = StartCoroutine(StaminaRegen());
        }
    }
    public IEnumerator StaminaRegen()
    {
        _staminaRegenBool = true;
        yield return new WaitForSeconds(2);
        while (PlayerStats.GetCurrentStam() < PlayerStats.GetMaxStam())
        {
            yield return new WaitForSeconds(.05f);
            PlayerStats.SetCurrentStam(PlayerStats.GetCurrentStam() + _staminaRegenRate);
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
            if(PlayerStats.GetCurrentHP() > damage)
            {
            PlayerStats.SetCurrentHP(PlayerStats.GetCurrentHP()-damage);
            }

            else if (PlayerStats.GetCurrentHP() <= damage)
            {
                Death();
            }
        }
    }
    private void Attack()
    {
        if(InputManager.GetInstance().GetAttackPressed())
        {
            if(CanReceiveInput == true)
            {
                InputReceived = true;
                MousePosition = Mouse.current.position.ReadValue();
                MousePosition.z = _camera.nearClipPlane + 1;
                MouseWorldPosition = _camera.ScreenToWorldPoint(MousePosition);
                DiffPos = (MouseWorldPosition - player.transform.position).normalized;
            }
            else if(CanReceiveInput == false && InputReceived == false)
            {
                if(PlayerStats.GetCurrentStam() > 0)
                {
                StopCoroutine(_staminaRegening);
                _staminaRegenBool = false;
                MousePosition = Mouse.current.position.ReadValue();
                MousePosition.z = _camera.nearClipPlane + 1;
                MouseWorldPosition = _camera.ScreenToWorldPoint(MousePosition);
                DiffPos = (MouseWorldPosition - player.transform.position).normalized;
                _anim.SetFloat("MouseX",DiffPos.x);
                _anim.SetFloat("MouseY",DiffPos.y);
                _anim.SetTrigger("FirstAttack");
                }
                else if(PlayerStats.GetCurrentStam() == 0)
                {
                    Debug.Log("Not enough Stamina!");
                }
            }
        }
    }
    private void Roll()
    {
        if(InputManager.GetInstance().GetRollPressed() && !IsRolling)
        {
            if(PlayerStats.GetCurrentStam() >= RollCost)
            {
                MousePosition = Mouse.current.position.ReadValue();
                MousePosition.z = _camera.nearClipPlane + 1;
                MouseWorldPosition = _camera.ScreenToWorldPoint(MousePosition);
                DiffPos = (MouseWorldPosition - player.transform.position).normalized;
                RollSpeed = 5;
                StopCoroutine(_staminaRegening);
                _staminaRegenBool = false;
                PlayerStats.SetCurrentStam(PlayerStats.GetCurrentStam() - RollCost);
                _anim.SetFloat("MouseX",DiffPos.x);
                _anim.SetFloat("MouseY",DiffPos.y);
                _anim.SetTrigger("Roll");         
            }
            else if(PlayerStats.GetCurrentStam() < RollCost && PlayerStats.GetCurrentStam() > 0)
            {
                MousePosition = Mouse.current.position.ReadValue();
                MousePosition.z = _camera.nearClipPlane + 1;
                MouseWorldPosition = _camera.ScreenToWorldPoint(MousePosition);
                DiffPos = (MouseWorldPosition - player.transform.position).normalized;
                RollSpeed = 5;
                StopCoroutine(_staminaRegening);
                _staminaRegenBool = false;
                PlayerStats.SetCurrentStam(0);
                _anim.SetFloat("MouseX",DiffPos.x);
                _anim.SetFloat("MouseY",DiffPos.y);
                _anim.SetTrigger("Roll");          
            }
            else if(PlayerStats.GetCurrentStam() == 0)
            {
                Debug.Log("Not enough Stamina!");
            }
        }
    }

    public void Move()
    {
        _moveInput = playerControls.Player_Map.Movement.ReadValue<Vector2>(); 
        Rb.velocity = _moveInput * PlayerStats.GetSpeed();
        _anim.SetFloat("Horizontal",Rb.velocity.x);
        _anim.SetFloat("Vertical",Rb.velocity.y);

        if(Rb.velocity.x != 0f)
        {
            isMoving = true;
        }
        else if (Rb.velocity.y != 0f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        _anim.SetBool("isMoving", isMoving);
    }
    public void Death()
    {
        _anim.Play("death");
        _anim.SetBool("Death", true);
        PlayerStats.SetCurrentHP(0);
        //play death animation
        Rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //YouLose.SetActive(true);
    }



    // private void OnTriggerEnter2D(Collider2D _collider)
    // {
    //     if (_collider.gameObject.TryGetComponent<BaseEnemy>(out BaseEnemy monster))
    //     {
    //         monster.TakeDamage((int)PlayerStats.GetAttack());
    //         Debug.Log("Monster took " + PlayerStats.GetAttack() + " damage!");
    //     }
    // }






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
