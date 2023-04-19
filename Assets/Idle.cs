using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attack;
using Module;using CondinedModule;

public class Idle : StateMachineBehaviour
{
    private AbMainModule mainModule;
    private StateModule stateModule;

    private float _half;

    private float current;
    private bool isUp;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        
        mainModule.SetActiveAnimatorRoot(0);
        
        mainModule.CanMove = true;
        
        stateModule.RemoveState(State.ATTACK);
        stateModule.RemoveState(State.SKILL);

        _half = 1;//stateInfo.length - (stateInfo.length / 9);
        current = 0;

        isUp = true;
        //mainModule.CanConsecutiveAttack = false;

        //animator.SetBool("IsCombo", false);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isUp) return;
        if (current < _half)
        {
            current += Time.deltaTime;
            mainModule.StopOrNot = Mathf.Min(1, mainModule.StopOrNot + Time.deltaTime / _half);
        }

        else
        {
            isUp = false;
            //animator
            mainModule.SetAnimationLayerOn(1, 0);
            mainModule.StopOrNot = 1;
            animator.SetBool("IsCombo", false);
            //animator.SetBool("CanLand", false);
        }
    }
}
