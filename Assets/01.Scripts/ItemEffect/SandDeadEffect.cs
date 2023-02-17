using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace ItemEffect
{
	public class SandDeadEffect : MonoBehaviour
	{
		public VisualEffect visualEffect;
		public string propertyName;

		public void OnEnable()
		{
			SkinnedMeshRenderer _skinnedMeshRenderer = GetComponentInParent<SkinnedMeshRenderer>();
			visualEffect.SetSkinnedMeshRenderer(propertyName, _skinnedMeshRenderer);
		}
	}
}
