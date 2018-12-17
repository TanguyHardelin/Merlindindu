using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttackBoss : StateMachineBehaviour
{   
    public CastLauncher castLauncher;
    public GameObject prefab;
    public GameObject meteor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        castLauncher = Instantiate(prefab, Vector3.one, Quaternion.identity).GetComponent<CastLauncher>();;
        GameObject.Find("Boss").GetComponent<Boss>().canMove = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        castLauncher.isCasted = true;
        castLauncher.targetPosition = GameObject.Find("Player").transform.position;
        animator.SetBool("attacking", false);
        GameObject.Find("Boss").GetComponent<Boss>().canMove = true;
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
