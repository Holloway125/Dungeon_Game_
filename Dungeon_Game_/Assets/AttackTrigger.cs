using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    BaseEnemy instance;
    public bool InRange;
    void Start()
    {
        instance = GetComponentInParent<BaseEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        InRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        InRange = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        InRange = true;
    }
}
