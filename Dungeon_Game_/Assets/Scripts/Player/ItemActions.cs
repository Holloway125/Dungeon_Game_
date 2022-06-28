using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]//RequireComponent will add the component to the inspector if it is not there already

public class ItemActions : MonoBehaviour
{
 

    public bool inventory;  //if true item can be stored in inventory. Can set to true in INSPECTOR
    //public bool openable;   
    //public bool locked;
    //public bool equipable;
    //public bool talks; 
    public string itemType; //equipment, potion, key item, etc.

    //public GameObject itemNeeded; //item needed to interact with gameobject
    //public string message;  //message to player

    //public Animator anim;
    void Awake()
    {     
        CircleCollider2D cc;
        cc = gameObject.GetComponent<CircleCollider2D>();
        cc.isTrigger = true;
    }
    public void DoInteraction()
    {
        //Pick up item and put in inventory
        gameObject.SetActive(false);
        
    }


    
}
