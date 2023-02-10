using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using UnityEngine.Animations;

public class Attack : StateMachineBehaviour
{
    AbMainModule mainModule;

    private void Awake()
    {
        mainModule = GameObject.Find("Player").GetComponentInChildren<AbMainModule>();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        mainModule.attacking = false;
        mainModule.strongAttacking = false;
    }
}
