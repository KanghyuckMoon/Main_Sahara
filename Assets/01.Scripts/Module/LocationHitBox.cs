using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
		protected AbMainModule mainModule;

		[SerializeField]
		protected float attackMulti = 1.0f;

		[SerializeField] protected UnityEvent hitEvent;
		
		protected virtual void OnTriggerEnter(Collider other)
		{
			PhysicsModule _physicsModule = mainModule.GetModuleComponent<PhysicsModule>(ModuleType.Physics);
			_physicsModule.OnTriggerEnter(other, this, hitEvent);
		}
		
	}
}
