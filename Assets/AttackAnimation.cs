using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Attack;
using Weapon;
using UnityEngine.Animations;

public class AttackAnimation : StateMachineBehaviour
{
    private AbMainModule mainModule;
    private StateModule stateModule;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);

        mainModule.Attacking = false;
        mainModule.StrongAttacking = false;
        mainModule.CanConsecutiveAttack = false;

        mainModule.Animator.SetBool("ConsecutiveAttack", false);

        stateModule.RemoveState(State.ATTACK);
        //Debug.Log("aflahfaiufhaliuhlaiuehgaliuehlaueghlawiueghliueghlawueghlahuegl");
    }
}
