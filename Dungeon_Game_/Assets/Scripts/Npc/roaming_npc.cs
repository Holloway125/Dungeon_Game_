using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roaming_npc : MonoBehaviour
{
    public float moveSpeed;
    public bool isWalking;
    public float walkTime;
    public float waitTime;
    public float waitCounter;
    //Defines the zone where the NPC can walk via collider
    public Collider2D walkArea;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;
    private Rigidbody2D npcRigidbody;
    private float walkCounter;
    private int WalkDirection;
    private bool hasWalkArea;
    public Collider2D dialog;

    // Start is called before the first frame update
    void Start()
    {
        npcRigidbody = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
        //Defines the min/max values in which the NPC can walk
        if(walkArea != null)
        {
            minWalkPoint = walkArea.bounds.min;
            maxWalkPoint = walkArea.bounds.max;
            hasWalkArea = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isWalking)
        {
            walkCounter -= Time.deltaTime;
            //Defines the different directions the NPC can move
            switch(WalkDirection)
            {
                case 0:
                    npcRigidbody.velocity = new Vector2(0, moveSpeed);
                    if(hasWalkArea && transform.position.y > maxWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 1:
                    npcRigidbody.velocity = new Vector2(0, moveSpeed);
                    if (hasWalkArea && transform.position.y > maxWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 2:
                    npcRigidbody.velocity = new Vector2(moveSpeed, 0);
                    if (hasWalkArea && transform.position.x > maxWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 3:
                    npcRigidbody.velocity = new Vector2(moveSpeed, 0);
                    if (hasWalkArea && transform.position.x > maxWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 4:
                    npcRigidbody.velocity = new Vector2(0, -moveSpeed);
                    if (hasWalkArea && transform.position.y < minWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 5:
                    npcRigidbody.velocity = new Vector2(0, -moveSpeed);
                    if (hasWalkArea && transform.position.y < minWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 6:
                    npcRigidbody.velocity = new Vector2(-moveSpeed, 0);
                    if (hasWalkArea && transform.position.x < minWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 7:
                    npcRigidbody.velocity = new Vector2(-moveSpeed, 0);
                    if (hasWalkArea && transform.position.x < minWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;

            }

            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }
        }
        //Tells the NPC to choose a new direction after the "wait time" or if isWalking is false
        else
        {
            waitCounter -= Time.deltaTime;

            npcRigidbody.velocity = Vector2.zero;
            if(waitCounter < 0)
            {
                ChooseDirection ();
            }
        }
    }
    
    public void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 8);
        isWalking = true;
        walkCounter = walkTime;
    }
}
