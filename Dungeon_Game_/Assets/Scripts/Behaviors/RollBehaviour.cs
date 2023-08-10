using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBehaviour : StateMachineBehaviour
{
    GameObject player;
    PlayerController playerController;
    Animator _anim;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player");
        _anim = player.GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController>();
        PlayerStats.SetSpeed(0);
        _anim.SetBool("IsRolling", true);
        playerController.IsRolling = true;
        playerController._hitBox.enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //                                   *****Read Comment*****
        //You can put normalized Diffpos to make the player go a set distance everytime no matter close the mouse is!!
        playerController.Rb.position += playerController.DiffPos * playerController.RollSpeed * Time.deltaTime;
        playerController.RollSpeed -= playerController.RollSpeed * Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
     override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController._hitBox.enabled = true;
        PlayerStats.SetSpeed(PlayerStats.GetDefaultSpeed());
        _anim.SetBool("IsRolling", false);
        playerController.IsRolling = false;
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