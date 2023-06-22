using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionOneBehavior : StateMachineBehaviour
{
    GameObject player;
    CharacterStats playerStats;
    PlayerController playerController;
    PlayerResource playerResource;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {         
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<CharacterStats>();
        playerController = player.GetComponent<PlayerController>();
        playerResource = player.GetComponent<PlayerResource>();
        playerStats.SetSpeed(0*0.3f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController.inputReceived && playerResource.staminaSlider.fillAmount >= playerResource.attackCost)
        {
            animator.SetTrigger($"{playerController.AttackDir()}AttackTwo");
            playerController.InputManager();
            playerController.inputReceived = false;
        }
        else if(playerResource.staminaSlider.fillAmount <= playerResource.attackCost)
        {
            playerController.inputReceived = false;
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
