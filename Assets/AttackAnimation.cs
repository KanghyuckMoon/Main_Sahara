using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Attack;
using CondinedModule;
using Weapon;
using UnityEngine.Animations;

public class AttackAnimation : StateMachineBehaviour
{
    private AbMainModule mainModule;
    private StateModule stateModule;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        animator.SetBool("IsCombo", true);
        
        mainModule.SetAnimationLayerOn(0, 0f);
        mainModule.SetConsecutiveAttack(0);
        mainModule.SetActiveAnimatorRoot(1);

        stateModule.AddState(State.ATTACK);
        mainModule.StopOrNot = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        
        mainModule.Attacking = false;
        mainModule.StrongAttacking = false;
        //stateModule.RemoveState(State.ATTACK);
        mainModule.SetAnimationLayerOn(1, 0.3f);
    }
}
