using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

public class ChangingWeapon : StateMachineBehaviour
{
    private AbMainModule abMainModule;

    private StateModule stateModule;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        abMainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= abMainModule.GetModuleComponent<StateModule>(ModuleType.State);

        stateModule.AddState(State.SKILL);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        abMainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= abMainModule.GetModuleComponent<StateModule>(ModuleType.State);

        stateModule.RemoveState(State.SKILL);
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
