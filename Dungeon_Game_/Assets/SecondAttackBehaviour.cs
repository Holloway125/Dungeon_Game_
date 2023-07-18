using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondAttackBehaviour : StateMachineBehaviour
{
    GameObject player;
    CharacterStats playerStats;
    PlayerController playerController;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<CharacterStats>();
        playerController = player.GetComponent<PlayerController>();
        playerController.canReceiveInput = true;
        playerController.inputReceived = false;
        playerStats.SetSpeed(0);
        if(playerStats.GetCurrentStam() >= playerController.attackCost)
        {
            playerStats.SetCurrentStam(playerStats.GetCurrentStam() - playerController.attackCost);
        }
        else if(playerStats.GetCurrentStam() < playerController.attackCost && playerStats.GetCurrentStam() > 0)
        {
            playerStats.SetCurrentStam(0);
        }
        animator.speed = (playerStats.GetAttackSpeed()+1);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        playerController.canReceiveInput = false;
        playerStats.SetSpeed(playerStats.GetDefaultSpeed());
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
