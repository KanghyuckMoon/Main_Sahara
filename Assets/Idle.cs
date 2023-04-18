using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attack;
using Module;

public class Idle : StateMachineBehaviour
{
    private AbMainModule mainModule;
    private StateModule stateModule;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        
        mainModule.SetActiveAnimatorRoot(0);
        mainModule.CanMove = true;
        
        stateModule.RemoveState(State.ATTACK);
        stateModule.RemoveState(State.SKILL);
        
        //mainModule.CanConsecutiveAttack = false;

        animator.SetBool("IsCombo", false);
        animator.SetBool("CanLand", false);
    }
}
