using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using UnityEngine.Animations;

public class Hit : StateMachineBehaviour
{
    private AbMainModule mainModule;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        mainModule.isHit = false;
    }
}
