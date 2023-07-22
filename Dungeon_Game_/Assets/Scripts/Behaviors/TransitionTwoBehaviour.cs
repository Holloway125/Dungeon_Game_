using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTwoBehaviour : StateMachineBehaviour
{
    GameObject player;
    PlayerController playerController;
    Animator _anim;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {         
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        _anim = player.GetComponent<Animator>();
        PlayerStats.SetSpeed(PlayerStats.GetDefaultSpeed());
        playerController.CanReceiveInput = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController.InputReceived && PlayerStats.GetCurrentStam() > 0)
        {
            playerController.CanReceiveInput = false;
            playerController.InputReceived = false;
            _anim.SetFloat("MouseX",playerController.DiffPos.x);
            _anim.SetFloat("MouseY",playerController.DiffPos.y);
            _anim.SetTrigger("FinalAttack");
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController.CanReceiveInput = false;
        playerController.InputReceived = false;
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
