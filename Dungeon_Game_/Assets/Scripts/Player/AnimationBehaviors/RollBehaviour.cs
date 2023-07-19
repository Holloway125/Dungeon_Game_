using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBehaviour : StateMachineBehaviour
{
    GameObject player;
    CharacterStats playerStats;
    PlayerController playerController;
    Animator _anim;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player");
        _anim = player.GetComponent<Animator>();
        playerStats = player.GetComponent<CharacterStats>();
        playerController = player.GetComponent<PlayerController>();
        playerStats.SetSpeed(0);
        _anim.SetBool("IsRolling", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player.transform.position += playerController.diffPos * playerController.rollSpeed * Time.deltaTime;
        playerController.rollSpeed -= playerController.rollSpeed * 10f * Time.deltaTime;
        playerController._capsuleCollider.enabled = false;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
     override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController._capsuleCollider.enabled = true;
        playerStats.SetSpeed(playerStats.GetDefaultSpeed());
        _anim.SetBool("IsRolling", false);
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
