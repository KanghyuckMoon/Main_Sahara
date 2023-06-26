using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

public class Skill : StateMachineBehaviour
{
    private StateModule stateModule;
    private AbMainModule mainModule;
    private EndSkillAnimation endSkillAnimation;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);

        stateModule.AddState(State.SKILL);
        stateModule.AddState(State.ATTACK);
        mainModule.SetActiveAnimatorRoot(1);
        //mainModule.SetAnimationLayerOn();
        mainModule.SetAnimationLayerOn(0,0);
        mainModule.StopOrNot = 0;
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);

        endSkillAnimation ??= animator.gameObject.GetComponentInChildren<EndSkillAnimation>();
        
        stateModule.RemoveState(State.SKILL);
        stateModule.RemoveState(State.ATTACK);
        
        animator.SetBool("WeaponSkill", false);
        animator.SetBool("Skill", false);
        
        mainModule.SetAnimationLayerOn(1,0);
        
        mainModule.StopOrNot = 1;

        if (endSkillAnimation != null)
        {
            endSkillAnimation.EndEvent();
        }
    }
}
