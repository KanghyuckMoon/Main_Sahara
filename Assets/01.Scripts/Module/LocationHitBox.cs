using Attack;
using Effect;
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
			if(mainModule == null)
			{
				return;
			}

			if(!mainModule.enabled)
			{
				return;
			}

			foreach (string _tagName in mainModule.HitCollider)
			{
				if (other.CompareTag(_tagName) && !mainModule.IsDead)
				{
					PhysicsModule _physicsModule = mainModule.GetModuleComponent<PhysicsModule>(ModuleType.Physics);
					_physicsModule.OnTriggerEnter(other, this, hitEvent);
				}
			}
		}
		
		public void TimeSlow(float _additionTime)
		{
			TimeSlowManager.Instance.SlowTime += _additionTime;
		}

		public void AddHitEffect(string _effectAddress)
		{
			EffectManager.Instance.SetEffectDefault(_effectAddress, transform.position, Quaternion.identity);
		}

	}
}
