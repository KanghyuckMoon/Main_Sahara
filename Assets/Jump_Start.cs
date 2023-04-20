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
        delay = 0.1f;
        isJump = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(isJump) return;

        if (!mainModule.isGround)
        {
            delay -= Time.deltaTime;

            if (delay >= 0) return;
            animator.SetBool("CanLand", true);
            isJump = true;
        }
    }
}
