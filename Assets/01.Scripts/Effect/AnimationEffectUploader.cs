using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{

	[ExecuteInEditMode]
	public class AnimationEffectUploader : MonoBehaviour
	{
		[SerializeField]
		private AnimationEffectSO animationEffectSO;
		[SerializeField]
		private Transform targetEffectTransform;
		[SerializeField]
		private string key;
		[SerializeField]
		private EffectData effectData = new EffectData();

		public void Update()
		{
			if (targetEffectTransform != null)
			{
				effectData.offset = targetEffectTransform.localPosition;
				effectData.rotation = targetEffectTransform.eulerAngles;
				effectData.scale = targetEffectTransform.localScale;
			}
		}

		[ContextMenu("Upload")]
		public void Upload()
		{
			animationEffectSO.UploadEffectData(key, effectData);
		}

	}

}