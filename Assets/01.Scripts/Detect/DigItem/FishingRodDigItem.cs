using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Detect
{
    
public class FishingRodDigItem : BaseDigItem
{
    [SerializeField] private string animString;
    [SerializeField] private Animator animator;
    
    protected override void OnEnable()
    {
        var _detectAnimationAction = transform.root.GetComponentInParent<DetectAnimationAction>();
        if (_detectAnimationAction is not null)
        {
            Debug.Log("Success");
            _detectAnimationAction.ChangeAction(Dig);
            _detectAnimationAction.ChangeAction2(FishingCheck);
            _detectAnimationAction.SetAnimator(animator);
        }
    }

    public void FishingCheck()
    {
        GetNearObject();
        var playerAnimator =  transform.root.GetComponentInParent<Animator>();
        if(targetItem is not null)
        {
            Debug.Log("GetOut");
            playerAnimator.Play(animString);
        }
    }
}

}