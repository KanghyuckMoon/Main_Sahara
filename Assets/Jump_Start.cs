using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

public class Jump_Start : StateMachineBehaviour
{
    private AbMainModule mainModule;

    private bool isJump;
    private float delay;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        delay = 0.4f;
        mainModule.StopOrNot = 0.5f;
        isJump = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isJump) return;

        delay -= Time.deltaTime;

        if (delay >= 0) return;
        animator.SetBool("CanLand", true);
        mainModule.StopOrNot = 1;
        isJump = true;

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float remainingTime = stateInfo.length * (1 - stateInfo.normalizedTime);
        Debug.Log($"Jump LIndex {layerIndex} {remainingTime}");
        animator.SetBool("Jump", false);
    }
}
