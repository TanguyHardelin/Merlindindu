﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttack : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().volume = (float)0.2;
        var audioClip = Resources.Load<AudioClip>("Sounds/Player/sword_slashMP3");
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().clip = audioClip;
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Play();
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetBool("isAttacking", false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().hasAttacked = false;
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
