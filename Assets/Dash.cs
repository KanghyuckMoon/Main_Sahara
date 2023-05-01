using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

public class Dash : StateMachineBehaviour
{
    private StateModule stateModule;
    private AbMainModule mainModule;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        
        stateModule.AddState(State.SKILL);
        //mainModule.StopOrNot = 0;
        //mainModule.SetActiveAnimatorRoot(1);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();

        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);

        stateModule.RemoveState(State.SKILL);
        stateModule.RemoveState(State.ATTACK);

        animator.SetBool("WeaponSkill", false);
        animator.SetBool("Skill", false);
        animator.SetBool("Dash", false);
    }
}
