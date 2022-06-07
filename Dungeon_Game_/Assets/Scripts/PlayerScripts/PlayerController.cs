using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //when user left clicks shoots ray from main camera to mouse pointer position
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))//when ray hits an object those objects will be stored here
            {
                Debug.Log (" We hit" + hit.collider.name + " " + hit.point);
            }
        }
    }
}
