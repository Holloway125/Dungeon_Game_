using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    public bool canReceiveInput;
    public bool inputReceived;
    GameObject player;
    Animator _anim;
    private Vector3 mousePosition;
    private Vector3 mouseWorldPosition;
    private float angle;
    private float angleDegree;
    [SerializeField] private Camera _camera;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        _anim = player.GetComponent<Animator>();
        canReceiveInput = true;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(canReceiveInput)
            {
                inputReceived = true;
                canReceiveInput = false;
            }
            else
            {
                return;
            }
        }
    }
    public void InputManager()
    {
        if(!canReceiveInput)
        {
            canReceiveInput = true;
        }
        else
        {
            canReceiveInput = false;
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
