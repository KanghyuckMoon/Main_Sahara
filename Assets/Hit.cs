using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Attack;
using UnityEngine.Animations;

public class Hit : StateMachineBehaviour
{
    private AbMainModule mainModule;
    private StateModule stateModule;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //mainModule ??= animator.GetComponent<AbMainModule>();
        
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        
        mainModule.IsHit = true;
        mainModule.CanMove = false;
        animator.SetBool("Hit", false);
        animator.SetBool("ConsecutiveAttack", false);
        animator.SetBool("Dash", false);
        animator.SetBool("IsCombo", false);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
    //    mainModule.Attacking = false;
    //    mainModule.StrongAttacking = false;
        mainModule.IsHit = false;
        mainModule.CanMove = true;
        
        animator.GetComponent<ProjectileGenerator>()?.MoveProjectile();
        stateModule.RemoveTypeState(State.ATTACK);
        stateModule.RemoveTypeState(State.SKILL);
        stateModule.RemoveTypeState(State.CHARGE);
        //stateModule.Clea

        mainModule.Attacking = false;
        mainModule.StrongAttacking = false;
        mainModule.CanConsecutiveAttack = false;
    }
}
