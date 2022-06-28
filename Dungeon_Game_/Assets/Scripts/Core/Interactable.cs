using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    ItemActions itemActions;
    public GameObject currentIntObj = null;
    public ItemActions currentIntObjScript = null;
    public Inventory inventory;

    void Update()
    {
        if(Input.GetButtonDown("Interact") && currentIntObj) //Interacts with currentInteractableObject 
        {
            if(currentIntObjScript.inventory)
            {
                inventory.AddItem(currentIntObj);
            }
        }

         if (Input.GetButtonDown ("Use Potion"))
        {
            GameObject potion = inventory.FindItemByType("Potion");       
                if(potion != null)
                {
                 //use item and apply effect
                inventory.RemoveItem(currentIntObj);
                }
        }
    }

    void OnTriggerEnter2D(Collider2D other)//sets currentIntObj from null to the gameobject player is currently standing on
    {
        if ( other.CompareTag ("interactable"))
        {
            currentIntObj = other.gameObject;
            currentIntObjScript = currentIntObj.GetComponent<ItemActions> ();
            Debug.Log (other.name);

        }
    }

    void OnTriggerExit2D(Collider2D other)// sets currentIntObj back to null after leaving range of interactable game object
    {
        if ( other.CompareTag ("interactable"))
        {
            if(other.gameObject == currentIntObj)
            {
                currentIntObj = null;
            }
        }
    }
}

