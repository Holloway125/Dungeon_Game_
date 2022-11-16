using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Transform targetToFollow;

    private void Awake()
    {
        targetToFollow = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = targetToFollow.transform.position;
        transform.position = new Vector3 (targetPosition.x, targetPosition.y, -20);
    }
}
