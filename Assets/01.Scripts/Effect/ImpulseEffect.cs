using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Effect
{
	public class ImpulseEffect : MonoBehaviour
	{
		private CinemachineImpulseSource cinemachineImpulse;

		public void OnEnable()
		{
			cinemachineImpulse ??= GetComponent<CinemachineImpulseSource>();
			cinemachineImpulse.GenerateImpulse();
		}
	}
}

