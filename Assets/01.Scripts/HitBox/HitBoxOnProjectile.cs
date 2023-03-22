using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Utill.Pattern;
using Utill.Coroutine;

namespace HitBox
{
	public class HitBoxOnProjectile : MonoBehaviour
	{
		[SerializeField]
		private HitBoxDatasSO hitBoxDataSO;

		[SerializeField]
		private string hitboxString;

		[SerializeField]
		private GameObject owner;

		[SerializeField]
		private bool isTimeIndexCange = false;

		private ulong index = 0;
		private bool isInit = false;
		private bool isSetHitbox = false;
		private List<InGameHitBox> inGameHitBoxeList = null;
		
		[SerializeField] private bool isOnEnalbe = false;

		private void OnEnable()
		{
			if (isOnEnalbe)
			{
				SetEnable();
			}
		}

		public void SetEnable()
		{
			if (!isInit)
			{
				index = StaticHitBoxIndex.GetHitBoxIndex();
				isInit = true;
			}
			index++;

			if (isTimeIndexCange)
			{
				inGameHitBoxeList = new List<InGameHitBox>();
			}

			OnHitBox(hitboxString);

			if (isTimeIndexCange)
			{
				StartCoroutine(IndexChage());
			}
		}

		private IEnumerator IndexChage()
		{
			while(true)
			{
				yield return new WaitForSeconds(1f);
				foreach (InGameHitBox inGameHitBox in inGameHitBoxeList)
				{
					inGameHitBox.SetIndex(inGameHitBox.GetIndex() + 1);
				}
			}
		}

		private void OnDisable()
		{
			if (isSetHitbox)
			{
				StaticCoroutineManager.Instance.InstanceDoCoroutine(DisableHitBoxs());
			}
		}

		private IEnumerator DisableHitBoxs()
		{
			yield return null;
			InGameHitBox[] _inGameHitBoxArray = GetComponentsInChildren<InGameHitBox>();
			foreach (var _col in _inGameHitBoxArray)
			{
				_col.transform.SetParent(null);
				_col.gameObject.SetActive(false);
				Pool.ObjectPoolManager.Instance.RegisterObject("HitBox", _col.gameObject);
			}
		}

		public void SetOwner(GameObject _owner)
		{
			owner = _owner;
		}

		public void OnHitBox(string _str)
		{
			isSetHitbox = false;
			HitBoxDataList hitBoxDataList = hitBoxDataSO.GetHitboxList(_str);
			if (hitBoxDataList is not null)
			{
				string tagname = gameObject.tag == "Player" ? "Player_Weapon" : "EnemyWeapon";
				foreach (HitBoxData hitBoxData in hitBoxDataSO.GetHitboxList(_str).hitBoxDataList)
				{
					GameObject hitbox = ObjectPoolManager.Instance.GetObject("HitBox");
					InGameHitBox _ingameHitBox = hitbox.GetComponent<InGameHitBox>();
					_ingameHitBox.SetHitBox(index + hitBoxData.hitBoxIndex, hitBoxData, owner, tagname, gameObject);

					if (isTimeIndexCange)
					{
						inGameHitBoxeList.Add(_ingameHitBox);
					}
				}
			}
			isSetHitbox = true;
		}
	}
}
