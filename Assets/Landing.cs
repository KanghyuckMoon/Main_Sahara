using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using UnityEngine.Animations;

public class Landing : StateMachineBehaviour
{
    private AbMainModule _mainModule;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _mainModule = animator.GetComponent<AbMainModule>();
        _mainModule.StopOrNot = 0;
        //_mainModule.canMove = false;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _mainModule.StopOrNot = 1;
        //_mainModule.canMove = true;
    }
}
