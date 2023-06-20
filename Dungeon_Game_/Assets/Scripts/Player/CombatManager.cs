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

}
