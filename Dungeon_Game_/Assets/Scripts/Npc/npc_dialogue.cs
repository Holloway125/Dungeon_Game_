using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class npc_dialogue : MonoBehaviour
{
    public GameObject chatBox;
    public TMP_Text dialogText;
    public string dialog;
    public bool playerInRange;

    public Timer timer;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if(chatBox.activeInHierarchy)
            {
                chatBox.SetActive(false);
            }
            else
            {
                timer.StartTimer();
                chatBox.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            chatBox.SetActive(false);
        }
    }
}
