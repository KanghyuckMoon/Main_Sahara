using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;
using Pool;
using Utill.Pattern;
using Effect;

namespace ItemEffect
{
	public class SandDeadEffect : MonoBehaviour, ISkinEffect
	{
		[SerializeField]
		private VisualEffect visualEffect;
		[SerializeField]
		public string addressName;
		[SerializeField]
		public string skinMeshRendererProperty;
		private MyVFXTransformBinder myTransformBinder;

		public void Setting(SkinnedMeshRenderer _skinnedMeshRenderer, Transform _modelRoot)
		{
			myTransformBinder ??= gameObject.GetComponent<MyVFXTransformBinder>();
			myTransformBinder.Target = _modelRoot;
			visualEffect.SetSkinnedMeshRenderer(skinMeshRendererProperty, _skinnedMeshRenderer);
		}

		public void OnEnable()
		{
			StartCoroutine(Disable());
		}

		private IEnumerator Disable()
		{
			yield return new WaitForSeconds(5f);
			ObjectPoolManager.Instance.RegisterObject(addressName, gameObject);
			gameObject.SetActive(false);
		}
	}
}
