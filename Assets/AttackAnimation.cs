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
        mainModule.StopOrNot = 0;

        stateModule.AddState(State.ATTACK);

        animator.SetBool("IsCombo", true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        
        stateModule.RemoveState(State.ATTACK);
        
        mainModule.Attacking = false;
        mainModule.StrongAttacking = false;
    }
}
