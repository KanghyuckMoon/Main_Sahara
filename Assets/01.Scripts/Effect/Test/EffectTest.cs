using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{
	public class EffectTest : MonoBehaviour
	{
		[SerializeField]
		private string address;

		[ContextMenu("EffectOn")]
		public void EffectOn()
		{
			EffectManager.Instance.SetEffectDefault(address, Vector3.zero, Quaternion.identity);
		}

	}
}
