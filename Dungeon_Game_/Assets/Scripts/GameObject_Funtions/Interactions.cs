using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactions : MonoBehaviour
{
    private Collider2D Collider2D;
    private bool playerInRange;
    [SerializeField]
    private string playerInteraction;
    [SerializeField]
    private string displayText;

    void Start()
    {
        Collider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            SceneManager.LoadScene("RedCabin");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

}
