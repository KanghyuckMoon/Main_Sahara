using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

public class JumpAttack : StateMachineBehaviour
{
    private AbMainModule mainModule;
    private StateModule stateModule;
    private float gravity;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        
        stateModule.AddState(State.ATTACK);
        animator.SetBool("Hit", false);
        animator.SetBool("ConsecutiveAttack", false);

        gravity = mainModule.GravityScale;

        //stateModule.RemoveState(State.ATTACK);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule.GravityScale = Mathf.Lerp(0, gravity, mainModule.PersonalDeltaTime * 20f);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);

        //mainModule.SetActiveAnimatorRoot(0);
        stateModule.RemoveState(State.ATTACK);
        
        mainModule.Attacking = false;
        mainModule.StrongAttacking = false;

        mainModule.GravityScale = gravity;
    }
}
