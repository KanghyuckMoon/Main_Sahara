using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Attack;
using UnityEngine.Animations;

public class Hit : StateMachineBehaviour
{
    private AbMainModule mainModule;
    private SwordSetting swordSetting;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        swordSetting ??= animator.GetComponent<SwordSetting>();

        swordSetting.SetAttackCollider(0);
        mainModule.Attacking = false;
        mainModule.StrongAttacking = false;
        //mainModule.IsHit = false;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
    //    mainModule.Attacking = false;
    //    mainModule.StrongAttacking = false;
        mainModule.IsHit = false;
        animator.SetBool("Hit", false);
    }
}
