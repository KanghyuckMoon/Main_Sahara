using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAnimationAction : MonoBehaviour
{
    private System.Action action;
    
    public void ChangeAction(System.Action _action)
    {
        action = _action;
    }

    public void InvokeDetectAction()
    {
        action?.Invoke();
    }
}
