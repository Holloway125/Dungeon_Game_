using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondAttack : StateMachineBehaviour
{
    GameObject player;
    PlayerController playerController;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerController.CanReceiveInput = true;
        playerController.InputReceived = false;
        PlayerStats.SetSpeed(0);
        if(PlayerStats.GetCurrentStam() >= playerController.AttackCost)
        {
            PlayerStats.SetCurrentStam(PlayerStats.GetCurrentStam() - playerController.AttackCost);
        }
        else if(PlayerStats.GetCurrentStam() < playerController.AttackCost && PlayerStats.GetCurrentStam() > 0)
        {
            PlayerStats.SetCurrentStam(0);
        }
        animator.speed = (PlayerStats.GetAttackSpeed()+1);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        playerController.CanReceiveInput = false;
        PlayerStats.SetSpeed(PlayerStats.GetDefaultSpeed());
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
