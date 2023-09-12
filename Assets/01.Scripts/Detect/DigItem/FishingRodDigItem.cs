using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Effect;

namespace Detect
{
    
public class FishingRodDigItem : BaseDigItem
{
    [SerializeField] private string animString;
    [SerializeField] private Animator animator;
	[SerializeField] private Transform boat;
        private bool isFindAndOut;

		protected override void OnEnable()
    {
        var _detectAnimationAction = transform.root.GetComponentInChildren<DetectAnimationAction>();
        if (_detectAnimationAction is not null)
        {
            Debug.Log("Success");
            _detectAnimationAction.ChangeAction(Dig);
            _detectAnimationAction.ChangeAction2(FishingCheck);
            _detectAnimationAction.SetAnimator(animator);
        }
    }

    public override void Dig()
    {
            if(targetItem is not null)
            {
                Debug.Log("GetOut");
                targetItem.GetOut();
            }
    }

    public void FishingCheck()
    {
        GetNearObject();
        var playerAnimator = transform.root.GetComponentInChildren<Animator>();
        if(targetItem is not null)
        {
            Debug.Log("GetOut");
            playerAnimator.Play(animString);
        }
    }

    public void BoatStart()
    {
			int floorLayerIndex = LayerMask.NameToLayer("Ground");
			int layerMask = (1 << floorLayerIndex);
            Vector3 forward = transform.root.forward;
            forward.y = 0; 
			Vector3 pos = transform.position + forward.normalized * 5 + Vector3.up * 10;
            RaycastHit hit;
            if(Physics.Raycast(pos, Vector3.down, out hit, 30, layerMask))
			{
				boat.DOJump(hit.point + Vector3.down * 0.1f, 6f, 1, 0.6f).OnComplete(() =>
				{
					GetNearObject();
					EffectManager.Instance.SetEffectDefault("SmallBoomSandVFX", hit.point, Quaternion.identity);
					if (targetItem is not null)
					{
						EffectManager.Instance.SetEffectDefault("FishingAreaFindEffect", hit.point, Quaternion.identity);
                        isFindAndOut = true;
					}
					else
					{
						EffectManager.Instance.SetEffectDefault("FishingAreaEffect", hit.point, Quaternion.identity);
                        isFindAndOut = false;
					}
                });
			}
            else
            {
                pos += Vector3.down * 13;
				boat.DOJump(pos, 5f, 1, 0.6f);
                isFindAndOut = false;
			}
	}


    public void BoatEnd()
		{
            if(isFindAndOut)
            {
			    EffectManager.Instance.SetEffectDefault("BoomSandVFX", boat.position, Quaternion.identity);
            }
            else
			{
				EffectManager.Instance.SetEffectDefault("SmallBoomSandVFX", boat.position, Quaternion.identity);
			}

			boat.DOLocalJump(new Vector3(-0.3f, 2.4f, 0.23f), 2f, 1, 0.4f);
		}

	}

}