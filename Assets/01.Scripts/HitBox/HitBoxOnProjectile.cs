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
		private bool isSetting;
		[SerializeField] private bool isStage;
		private Coroutine coroutine;

		private HitBoxOnAnimation HitBoxOnAnimation
		{
			get
			{
				hitBoxOnAnimation ??= GetComponentInParent<HitBoxOnAnimation>();
				return hitBoxOnAnimation;
			}
		}


		private IEnumerator PlayCheck()
		{
			while (true)
			{
				if (GameManager.GamePlayerManager.Instance.IsPlaying)
				{
					OnEnable();
				}

				yield return null;
			}
		}

		private void Start()
		{
			hitBoxOnAnimation = GetComponentInParent<HitBoxOnAnimation>();
		}

		private void OnEnable()
		{
			if (isOnEnalbe)
			{
				if (isStage)
				{
					StartCoroutine(SetStageHitBoxOnProjectile());
				}
				else
				{
					SetEnable();
				}
			}
		}

		private IEnumerator SetStageHitBoxOnProjectile()
		{
			while (true)
			{
				if (GameManager.GamePlayerManager.Instance.IsPlaying)
				{
					yield return new WaitForSeconds(10f);
					SetEnable();
					yield break;
				}
				yield return null;
			}
		}


		public void SetEnable()
		{
			gameObject.SetActive(true);
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
					inGameHitBox.gameObject.SetActive(false);
					inGameHitBox.SetIndex(inGameHitBox.GetIndex() + 1);
					inGameHitBox.gameObject.SetActive(true);
				}
			}
		}

		private void OnDisable()
		{
			if (isSetHitbox)
			{
				if(coroutine != null)
				{
					StopCoroutine(coroutine);
				}
				coroutine = StaticCoroutineManager.Instance.StartCoroutine(OnDisableCol());
			}
		}

		private IEnumerator OnDisableCol()
		{
			yield return null;
			foreach (var _col in inGameHitBoxeList)
			{
				try
				{
					//_col.transform.SetParent(null);
					_col.gameObject.SetActive(false);
					Pool.HitBoxPoolManager.Instance.RegisterObject(_col);
				}
				catch (Exception e)
				{
					Debug.LogError(e);
					break;
				}
			}
			inGameHitBoxeList.Clear();
		}

		private void OnDestroy()
		{
			if (isSetHitbox)
			{
				RemoveHitBoxs();
			}
		}

		private void RemoveHitBoxs()
		{
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
					Debug.LogError(e);
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
			if (coroutine != null)
			{
				StopCoroutine(coroutine);
			}
			RemoveHitBoxs();
			HitBoxDataList hitBoxDataList = hitBoxDataSO.GetHitboxList(_str);
			if (hitBoxDataList is not null)
			{
				string tagname = gameObject.tag == "Player" ? "Player_Weapon" : "EnemyWeapon";
				foreach (HitBoxData hitBoxData in hitBoxDataSO.GetHitboxList(_str).hitBoxDataList)
				{
					InGameHitBox _ingameHitBox = HitBoxPoolManager.Instance.GetObject();
					if (_ingameHitBox.gameObject == null)
					{
						Debug.LogError("Error Null Hitbox GameObject");
					}
					_ingameHitBox.SetHitBox( _ingameHitBox.gameObject, index + hitBoxData.hitBoxIndex, hitBoxData, owner, tagname, gameObject, null, HitBoxOnAnimation?.hitBoxAction);
					
					inGameHitBoxeList.Add(_ingameHitBox);
				}
			}
			else
			{
				Debug.LogError("Projectile Str is null", gameObject);
			}
			isSetHitbox = true;
		}
	}
}
