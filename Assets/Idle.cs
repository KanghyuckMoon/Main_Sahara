using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attack;
using Module;

public class Idle : StateMachineBehaviour
{
    private AbMainModule mainModule;
    private StateModule stateModule;
    
    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainModule ??= animator.GetComponent<AbMainModule>();
        stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        
        mainModule.SetActiveAnimatorRoot(0);
        
        stateModule.RemoveState(State.ATTACK);
    }
}
