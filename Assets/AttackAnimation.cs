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
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        mainModule.Attacking = false;
        mainModule.StrongAttacking = false;


        //Debug.Log("aflahfaiufhaliuhlaiuehgaliuehlaueghlawiueghliueghlawueghlahuegl");
    }
}
