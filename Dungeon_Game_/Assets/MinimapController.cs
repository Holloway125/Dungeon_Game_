using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public Transform targetToFollow;

    void LateUpdate()
    {
        Vector3 targetPosition = targetToFollow.transform.position;
        transform.position = new Vector3 (targetPosition.x, targetPosition.y, -20);
    }
}
