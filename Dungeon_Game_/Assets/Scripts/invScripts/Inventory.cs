using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public GameObject[] inventory = new GameObject[20];//sets unity inventory to have 20 public game objects

    public void AddItem(GameObject item)//Adds item to inventory
    {
        bool itemAdded = false;

        for (int i = 0; i < inventory.Length; i++)//checks current inventory slots until it finds an empty slot if no empty slot is found "Inventory Full!" message will display in log
        {
                if (inventory [i] == null)
                {
                    inventory [i] = item;
                    Debug.Log (item.name + " was added.");
                    itemAdded = true;
                    item.SendMessage("DoInteraction");
                    break;
                }
        }

        if (!itemAdded)//if item was not added sends this message to console
        {
            Debug.Log("Inventory Full!");
        }

    }
    public GameObject FindItemByType(string itemType) //Looks for item in inventory by public string itemType
    {
            for(int i = 0; i < inventory.Length; i++)
            {
                if(inventory[i] != null)
                {
                    if(inventory [i].GetComponent <ItemActions>().itemType == itemType) 
                    {
                        return inventory[i];
                    }
                }    
            }
            return null;
    }


    public bool FindItem(GameObject item) //Looks for an item by name in inventory if found returns true if not false
    {
        for(int i = 0; i < inventory.Length; i++)
        {
                if(inventory [i] == item) 
                {
                    return true;
                }
        }
            return false;
    }

    public void RemoveItem(GameObject item)
    {
        for(int i = 0; i < inventory.Length; i++)
            {
                if(inventory[i] == item)
                {
                   inventory[i] = null;
                   Debug.Log(item.name + "removed from inventory");
                   break;
                }    
            }
    }

}
