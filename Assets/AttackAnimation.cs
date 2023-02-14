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
    private SwordSetting swordSetting;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        swordSetting ??= animator.GetComponent<SwordSetting>();//.SetAttackCollider(1);
        //swordSetting.SetAttackCollider(1);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        swordSetting ??= animator.GetComponent<SwordSetting>();
        //swordSetting.SetAttackCollider(0);
        mainModule ??= animator.GetComponent<AbMainModule>();
        mainModule.Attacking = false;
        mainModule.StrongAttacking = false;


        Debug.Log("aflahfaiufhaliuhlaiuehgaliuehlaueghlawiueghliueghlawueghlahuegl");
    }

    //IEnumerator Set
}
