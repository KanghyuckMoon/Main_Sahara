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

		public Vector3 HitBoxPos
		{
			get
			{
				if(hitBoxData is null)
				{
					return transform.position;
				}
				return transform.position + (transform.forward * hitBoxData.offset.z) + (transform.up * hitBoxData.offset.y) + (transform.right * hitBoxData.offset.x);
			}
		}

		public bool IsContactDir
		{
			get
			{
				return isContactDir;
			}
		}

		private BoxCollider col;
		private HitBoxData hitBoxData;
		private GameObject owner;
		private ulong index;
		private bool isContactDir;

		public void SetHitBox(ulong _index, HitBoxData _hitBoxData, GameObject _owner, string _tag, GameObject _parent = null)
		{
			index = _index;
			gameObject.tag = _tag;
			owner = _owner;
			col = GetComponent<BoxCollider>();
			hitBoxData = _hitBoxData;
			isContactDir = hitBoxData.isContactDirection;
			transform.position = _owner.transform.position;
			transform.rotation = _owner.transform.rotation;
			col.center = _hitBoxData.offset;
			col.size = _hitBoxData.size;

			Vector3 _pos = transform.position + (transform.forward * hitBoxData.swingEffectOffset.z) + (transform.up * hitBoxData.swingEffectOffset.y) + (transform.right * hitBoxData.swingEffectOffset.x);

			if (hitBoxData.childization)
			{
				if(_parent is null)
				{
					gameObject.transform.SetParent(owner.transform);
				}
				else
				{
					transform.position = _parent.transform.position;
					transform.rotation = _parent.transform.rotation;
					gameObject.transform.SetParent(_parent.transform);
				}
			}
			else
			{
				gameObject.transform.SetParent(null);
			}
			gameObject.SetActive(true);
			transform.localScale = Vector3.one;

			if (hitBoxData.swingEffect != "NULL")
			{
				EffectManager.Instance.SetEffectDefault(hitBoxData.swingEffect, _pos, _hitBoxData.swingEffectRotation + transform.eulerAngles, _hitBoxData.size);
			}

			if(hitBoxData.deleteDelay != -1)
			{
				StartCoroutine(DestroyHitBox());
			}
		}

		public void SetIndex(ulong _index)
		{
			index = _index;
		}

		public ulong GetIndex()
		{
			return index;
		}

		public Quaternion KnockbackDir()
		{
			Quaternion _quaternion = transform.rotation* Quaternion.Euler(hitBoxData.knockbackDir);
			return _quaternion.normalized;
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

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawCube(HitBoxPos, col.size);
		}
	}

}