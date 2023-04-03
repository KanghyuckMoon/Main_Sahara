	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using Pool;

namespace Effect
{
    public class AnimationOnEffect : MonoBehaviour
	{
		[SerializeField]
		private AnimationEffectSO animationEffectSO;

		[SerializeField] 
		private GameObject parent;

		public void ChangeSO(AnimationEffectSO _animationEffectSO)//, string _colliderKey)
		{
			animationEffectSO = _animationEffectSO;
		}

		public void OnEffect(string _str)
		{
			EffectDataList _animationEffectList = animationEffectSO.GetEffectList(_str);
			if (_animationEffectList is not null)
			{
				foreach (EffectData _effectBoxData in _animationEffectList.effectDataList)
				{
					Vector3 _pos = transform.position + (transform.forward * _effectBoxData.offset.z) + (transform.up * _effectBoxData.offset.y) + (transform.right * _effectBoxData.offset.x);
					if (_effectBoxData.childization)
					{
						EffectManager.Instance.SetEffectDefault(_effectBoxData.effectcAddress, _pos, _effectBoxData.rotation + transform.eulerAngles, _effectBoxData.scale, parent.transform);	
					}
					else
					{
						EffectManager.Instance.SetEffectDefault(_effectBoxData.effectcAddress, _pos, _effectBoxData.rotation + transform.eulerAngles, _effectBoxData.scale);
					}
				}
			}
			else
			{
				Debug.LogError($"Not Effect Str{_str}");
			}
		}
	}
}