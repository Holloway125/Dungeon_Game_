using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    private Vector3 ScreenPosition;
    private Vector3 mouseWorldPosition;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerHand;
    private float angle;
    private float angleDegree;


    public void MouseRotation()
    {
        //Gets mouse position and returns x,y value of the pixels mouse is on in current resolution
        ScreenPosition = Input.mousePosition; 
        //Sets screen position z to the near viewport of camera so it can then be translated correctlying into mouse world postion
        ScreenPosition.z = mainCamera.nearClipPlane + 1;
        //returns world position location of mouse in the Scene
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(ScreenPosition);
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
              

    }

    public void SetAnimationRotation()
    {
        //Gets mouse position and returns x,y value of the pixels mouse is on in current resolution
        ScreenPosition = Input.mousePosition; 
        //Sets screen position z to the near viewport of camera so it can then be translated correctlying into mouse world postion
        ScreenPosition.z = mainCamera.nearClipPlane + 1;
        //returns world position location of mouse in the Scene
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(ScreenPosition);
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
        Quaternion rotation = Quaternion.Euler(0, 0, angleDegree);
        playerHand.transform.rotation = rotation;
        playerHand.transform.position = new Vector3 ((Mathf.Cos(angle)) + player.transform.position.x, Mathf.Sin(angle) + player.transform.position.y, 0f);
    }



}


// Compute the position of the object
    
/* 
 x =.75 y = .6 at angle 0 right
    vector3 = mouseWorldPosition 
        
x = -.75 y = 0 at angle 180 and -180 left
        
        
x = 0 y = .75 at angle 90 up
        
        
x = 0 y = -.75 at angle -90 down

2pi = 360d

pi = 180d

150d = 

pi over 2 * 10 over 17

100/289

*/