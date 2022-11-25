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
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject player;
    private Animator _anim;


    //MouseRotation Variables
    private Vector3 mousePosition;
    private Vector3 mouseWorldPosition;
    private float angle;
    private float angleDegree;

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
        _playerActions.Player_Map.Movement.performed += context => Movement(context);
        _playerActions.Player_Map.Movement.canceled += context => CancelMovement(context);
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
            if(_rBody.velocity.x != 0f && isMoving == false)
            {
                isMoving = true;
                _anim.SetBool("isMoving", isMoving);
                Debug.Log("something");
            }
            else if (_rBody.velocity.y != 0f && isMoving == false)
            {
                isMoving = true;
                _anim.SetBool("isMoving", isMoving);
            }
            else if (_rBody.velocity.y == 0 && _rBody.velocity.x == 0)
            {
                isMoving = false;
                _anim.SetBool("isMoving", isMoving);
            }

     
    }

    private void Movement(InputAction.CallbackContext context)
    {
        _anim.SetBool("isMoving", true);
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>(); 
        _rBody.velocity = _moveInput * currentSpeed;
        _anim.SetFloat("Horizontal",_rBody.velocity.x);
        _anim.SetFloat("Vertical",_rBody.velocity.y);
    }
    
    private void CancelMovement(InputAction.CallbackContext context)
    {
        _anim.SetBool("isMoving", false);
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>(); 
        _rBody.velocity = _moveInput * 0;
        _anim.SetFloat("Horizontal",_rBody.velocity.x);
        _anim.SetFloat("Vertical",_rBody.velocity.y); 
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

    public string MouseRotation()
    {
        //Gets mouse position and returns x,y value of the pixels mouse is on in current resolution
        mousePosition = Mouse.current.position.ReadValue();

        //Sets screen position z to the near viewport of camera so it can then be translated correctlying into mouse world postion
        mousePosition.z = _camera.nearClipPlane + 1;

        //returns world position location of mouse in the Scene
        mouseWorldPosition = _camera.ScreenToWorldPoint(mousePosition);

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
}
