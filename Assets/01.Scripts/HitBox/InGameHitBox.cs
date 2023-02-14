using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HitBox
{
	public class InGameHitBox : MonoBehaviour
	{
		private HitBoxData hitBoxData;
		private GameObject owner;

		public void SetHitBox(HitBoxData _hitBoxData, GameObject _owner, string _tag)
		{
			gameObject.tag = _tag;
			owner = _owner;
			hitBoxData = _hitBoxData;
			if (hitBoxData.childization)
			{
				gameObject.transform.SetParent(owner.transform);
			}
			else
			{
				gameObject.transform.SetParent(null);
			}
			gameObject.SetActive(true);
			StartCoroutine(DestroyHitBox());
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