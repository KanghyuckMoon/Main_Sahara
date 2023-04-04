using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

public class Skill : StateMachineBehaviour
{
    private StateModule stateModule;
    private AbMainModule mainModule;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);

        mainModule.SetActiveAnimatorRoot(1);
        //stateModule.AddState(State.ATTACK);
        //mainModule.SetActiveAnimatorRoot(1);
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        
        mainModule.SetActiveAnimatorRoot(0);
        
        stateModule.RemoveState(State.SKILL);
        
        animator.SetBool("WeaponSkill", false);
        animator.SetBool("Skill", false);
    }
}
