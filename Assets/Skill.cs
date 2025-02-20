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

        stateModule.AddState(State.ATTACK);
        mainModule.SetActiveAnimatorRoot(1);
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        
        stateModule.RemoveState(State.SKILL);
        stateModule.RemoveState(State.ATTACK);
        
        animator.SetBool("WeaponSkill", false);
        animator.SetBool("Skill", false);
    }
}
