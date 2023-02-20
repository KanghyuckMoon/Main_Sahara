using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attack;
using Module;

public class Idle : StateMachineBehaviour
{
    private AbMainModule mainModule;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //mainModule ??= animator.GetComponent<AbMainModule>();

        //mainModule.Attacking = false;
        //mainModule.StrongAttacking = false;
    }
}
