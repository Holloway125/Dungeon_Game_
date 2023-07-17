using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionOneBehavior : StateMachineBehaviour
{
    GameObject player;
    CharacterStats playerStats;
    PlayerController playerController;
    Animator _anim;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {         
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<CharacterStats>();
        playerController = player.GetComponent<PlayerController>();
        _anim = player.GetComponent<Animator>();
        playerStats.SetSpeed(playerStats.GetDefaultSpeed());
        playerController.canReceiveInput = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController.inputReceived && playerStats.GetCurrentStam() > 0)
        {
            playerController.canReceiveInput = false;
            playerController.inputReceived = false;
            _anim.SetFloat("MouseX",playerController.diffPos.x);
            _anim.SetFloat("MouseY",playerController.diffPos.y);
            _anim.SetTrigger("SecondAttack");
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController.canReceiveInput = false;
        playerController.inputReceived = false;
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
