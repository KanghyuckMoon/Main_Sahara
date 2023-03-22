using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

public class Skill : StateMachineBehaviour
{
    private StateModule stateModule;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        stateModule ??= animator.GetComponent<AbMainModule>().GetModuleComponent<StateModule>(ModuleType.State);
        
        stateModule.RemoveState(State.SKILL);
        
        animator.SetBool("WeaponSkill", false);
        animator.SetBool("Skill", false);
    }
}
