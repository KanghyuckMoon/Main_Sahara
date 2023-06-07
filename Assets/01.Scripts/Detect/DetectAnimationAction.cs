using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAnimationAction : MonoBehaviour
{
    private System.Action action;
    private System.Action action2;
    private Animator itemAnimator;

    public void ChangeAction(System.Action _action)
    {
        action = _action;
    }

    public void SetAnimator(Animator _animator)
    {
        itemAnimator = _animator;
    }

    public void InvokeDetectAction()
    {
        action?.Invoke();
    }
    
    
    public void ChangeAction2(System.Action _action)
    {
        action2 = _action;
    }

    public void InvokeDetectAction2()
    {
        action2?.Invoke();
    }

    public void InvokeDigItemAnimation(string anim)
    {
        itemAnimator.Play(anim);
    }
}
