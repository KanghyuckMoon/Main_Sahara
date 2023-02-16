using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effect;

namespace HitBox
{
	public class InGameHitBox : MonoBehaviour
	{
		public GameObject Owner
		{
			get
			{
				return owner;
			}
		}

		private BoxCollider col;
		private HitBoxData hitBoxData;
		private GameObject owner;

		public void SetHitBox(HitBoxData _hitBoxData, GameObject _owner, string _tag)
		{
			gameObject.tag = _tag;
			owner = _owner;
			col = GetComponent<BoxCollider>();
			hitBoxData = _hitBoxData;
			transform.position = _owner.transform.position;
			transform.rotation = _owner.transform.rotation;
			col.center = _hitBoxData.offset;

			Vector3 _pos = transform.position + (transform.forward * hitBoxData.swingEffectOffset.z) + (transform.up * hitBoxData.swingEffectOffset.y) + (transform.right * hitBoxData.swingEffectOffset.x);

			if (hitBoxData.childization)
			{
				gameObject.transform.SetParent(owner.transform);
			}
			else
			{
				gameObject.transform.SetParent(null);
			}
			gameObject.SetActive(true);

			if (hitBoxData.swingEffect != null)
			{
				EffectManager.Instance.SetEffectDefault(hitBoxData.swingEffect, _pos, _hitBoxData.swingEffectRotation + transform.eulerAngles, _hitBoxData.size);
			}

			if(hitBoxData.deleteDelay != -1)
			{
				StartCoroutine(DestroyHitBox());
			}
		}

		public Quaternion KnockbackDir()
		{
			return transform.rotation * Quaternion.Euler(hitBoxData.knockbackDir);
		}

		public float KnockbackPower()
		{
			return hitBoxData.defaultPower;
		}
		private IEnumerator DestroyHitBox()
		{
			yield return new WaitForSeconds(hitBoxData.deleteDelay);
			transform.SetParent(null);
			gameObject.SetActive(false);
			Pool.ObjectPoolManager.Instance.RegisterObject("HitBox", gameObject);
		}
	}

}