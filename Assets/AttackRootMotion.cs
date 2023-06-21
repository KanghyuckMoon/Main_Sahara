using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Attack;
using CondinedModule;
using Weapon;
using UnityEngine.Animations;

public class AttackRootMotion : StateMachineBehaviour
{
    private AbMainModule mainModule;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        mainModule.SetActiveAnimatorRoot(1);
    }
}
