using System.Collections;
using System.Collections.Generic;
using Module;
using UnityEngine;

public class ConsecutiveAttacking : StateMachineBehaviour
{
    private AbMainModule abMainModule;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        abMainModule ??= animator.GetComponent<AbMainModule>();
        
        abMainModule.SetConsecutiveAttack(1);
    }
    
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        abMainModule ??= animator.GetComponent<AbMainModule>();

        abMainModule.SetConsecutiveAttack(0);
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
