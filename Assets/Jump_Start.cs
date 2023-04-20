using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

public class Jump_Start : StateMachineBehaviour
{
    private AbMainModule mainModule;
    private JumpModule jumpModule;

    private bool isJump;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        jumpModule ??= mainModule.GetModuleComponent<JumpModule>(ModuleType.Jump);

        isJump = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(isJump) return;

        if (!mainModule.isGround)
        {
            animator.SetBool("CanLand", true);
            isJump = true;
        }
    }
}
