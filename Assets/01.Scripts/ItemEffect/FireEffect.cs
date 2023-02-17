using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemEffect
{

	public class FireEffect : MonoBehaviour
	{
		[SerializeField]
		private ParticleSystem[] particleSystemArray;

		public void OnEnable()
		{
			MeshRenderer _parentRenderer = transform.GetComponentInParent<MeshRenderer>();

			if (_parentRenderer is null)
			{
				return;
			}

			foreach (var _particle in particleSystemArray)
			{
				var _shape = _particle.shape;
				_shape.meshRenderer = _parentRenderer;
			}
		}
	}

}