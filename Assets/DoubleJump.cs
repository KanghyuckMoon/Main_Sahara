using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : StateMachineBehaviour
{
    private bool isJump;
    private float delay;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        delay = 0.4f;
        isJump = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isJump) return;

        delay -= Time.deltaTime;

        if (delay >= 0) return;
        animator.SetBool("CanLand", true);
        isJump = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float remainingTime = stateInfo.length * (1 - stateInfo.normalizedTime);
        //Debug.Log($"DoubleJump LIndex {layerIndex} {remainingTime}");
        //animator.SetBool("DoubleJump", false);
    }
}
