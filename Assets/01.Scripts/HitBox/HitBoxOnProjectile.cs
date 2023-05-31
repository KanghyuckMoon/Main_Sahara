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
		private List<InGameHitBox> inGameHitBoxeList = new List<InGameHitBox>();
		
		[SerializeField] private bool isOnEnalbe = false;

		private HitBoxOnAnimation hitBoxOnAnimation;

		private HitBoxOnAnimation HitBoxOnAnimation
		{
			get
			{
				hitBoxOnAnimation ??= GetComponentInParent<HitBoxOnAnimation>();
				return hitBoxOnAnimation;
			}
		}

		private void Start()
		{
			hitBoxOnAnimation = GetComponentInParent<HitBoxOnAnimation>();
		}

		private void OnEnable()
		{
			try
			{
				if (isOnEnalbe)
				{
					SetEnable();
				}
			}
			catch (Exception e)
			{
				Debug.LogWarning(e, gameObject);
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
			
			foreach (var _col in inGameHitBoxeList)
			{
				try
				{
					_col.transform.SetParent(null);
					_col.gameObject.SetActive(false);
					Pool.HitBoxPoolManager.Instance.RegisterObject(_col);
				}
				catch (Exception e)
				{
					Debug.LogWarning(e);
					break;
				}
			}
			inGameHitBoxeList.Clear();
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
					InGameHitBox _ingameHitBox = HitBoxPoolManager.Instance.GetObject();
					_ingameHitBox.SetHitBox(index + hitBoxData.hitBoxIndex, hitBoxData, owner, tagname, gameObject, null, HitBoxOnAnimation?.hitBoxAction);

					inGameHitBoxeList.Add(_ingameHitBox);
				}
			}
			isSetHitbox = true;
		}
	}
}
