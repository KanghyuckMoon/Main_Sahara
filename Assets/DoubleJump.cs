using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : StateMachineBehaviour
{
    private float a = 0.2f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("DoubleJump", false);
        a = 0.2f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (a <= 0)
        {
            animator.SetBool("DoubleJump", false);
        }
        else
        {
            a -= Time.deltaTime;
        }
    }
}
