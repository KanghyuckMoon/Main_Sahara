using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
	public class LocationHitBox : MonoBehaviour
	{
		public float AttackMulti
		{
			get
			{
				return attackMulti;
			}
		}

		[SerializeField]
		private AbMainModule mainModule;

		[SerializeField]
		private float attackMulti = 1.0f;

		private void OnTriggerEnter(Collider other)
		{
			PhysicsModule _physicsModule = mainModule.GetModuleComponent<PhysicsModule>(ModuleType.Physics);
			_physicsModule.OnTriggerEnter(other, this);
		}
	}
}
