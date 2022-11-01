using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Controller : MonoBehaviour
{
    private PlayerControls playerControls;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;


    public Vector2 movement;
    public PlayerProperties playerproperties;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start()   
    {

    }

    void Update()
    {
        Vector2 move = playerControls.Player.Move.ReadValue<Vector2>();
        Debug.Log(move);
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void Disable()
    {
        playerControls.Disable();
    }   
}
