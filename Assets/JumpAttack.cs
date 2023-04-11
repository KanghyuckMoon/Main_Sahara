using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

public class JumpAttack : StateMachineBehaviour
{
    private AbMainModule mainModule;
    private StateModule stateModule;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //mainModule ??= animator.GetComponent<AbMainModule>();
        //stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);

        //mainModule.Attacking = false;
        //mainModule.StrongAttacking = false;
        //mainModule.CanConsecutiveAttack = false;
        
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        
        stateModule.AddState(State.ATTACK);
        animator.SetBool("ConsecutiveAttack", false);

        //stateModule.RemoveState(State.ATTACK);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);

        //mainModule.SetActiveAnimatorRoot(0);
        stateModule.RemoveState(State.ATTACK);
        
        mainModule.Attacking = false;
        mainModule.StrongAttacking = false;
        //mainModule.CanConsecutiveAttack = false;

        //animator.SetBool("ConsecutiveAttack", false);

        //stateModule.RemoveState(State.ATTACK);
        //Debug.Log("aflahfaiufhaliuhlaiuehgaliuehlaueghlawiueghliueghlawueghlahuegl");
    }
}
