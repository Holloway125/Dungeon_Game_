using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float _speed;
    private PlayerActions _playerActions;
    private Rigidbody2D _rBody;
    private Vector2 _moveInput;
    private CircleCollider2D _weaponCollider;
    private CharacterStats _characterStats;

    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject player;
    private Animator _anim;
    private bool isMoving;

    private Vector3 mousePosition;
    private Vector3 mouseWorldPosition;
    private float angle;
    private float angleDegree;

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
        _playerActions.Player_Map.Attack.performed += context => Attacking();
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
        _rBody.velocity = _moveInput * _speed;
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


    private void Attacking()
    {
        _speed = 0;
        _anim.Play($"{MouseRotation()}Attack");
    }  

    //Deals Damage to Monsters
    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.gameObject.TryGetComponent<BaseEnemy>(out BaseEnemy monster))
        {
            monster.TakeDamage((int)_characterStats.Damage.Value);
        }
    }

    private void StopAttacking()
    {
        _speed = 5;
        _anim.SetTrigger("Idle");
    }

    private string MouseRotation()
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
            return "SouthEast";
        }        
        else return "NoMouse"; 
    }
}
