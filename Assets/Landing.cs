using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using UnityEngine.Animations;

public class Landing : StateMachineBehaviour
{
    private AbMainModule _mainModule;
    private StateModule stateModule;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= _mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        _mainModule.StopOrNot = 0;

        animator.SetBool("CanLand", false);
        //_mainModule.MoveSpeed = 0;
        //_mainModule.canMove = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_mainModule.StopOrNot < 1){
            _mainModule.StopOrNot += 0.04f;
        }
        else
        {
            _mainModule.StopOrNot = 1;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //_mainModule.StopOrNot = 1;
        //_mainModule.CanConsecutiveAttack = false;
        //_mainModule.canMove = true;
        
        stateModule.RemoveTypeState(State.JUMP);
    }
}
