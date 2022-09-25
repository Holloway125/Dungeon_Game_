using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour

{
    private float _speed = 5;

    public float Speed
    {
        get => _speed;
        set 
        {
            if (value >= 0 && value <= 30)
            {
                _speed = value;
                Debug.Log("SetSpeed");
            }
        }
    }
}

