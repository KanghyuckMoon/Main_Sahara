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
		private HitBoxDataSO hitBoxDataSO;

		[SerializeField]
		private string hitboxString;


		private void OnEnable()
		{
			OnHitBox(hitboxString);
		}
		private void OnDisable()
		{
			StaticCoroutineManager.Instance.InstanceDoCoroutine(DisableHitBoxs());
		}

		private IEnumerator DisableHitBoxs()
		{
			yield return new WaitForSeconds(0.1f);
			InGameHitBox[] _inGameHitBoxArray = GetComponentsInChildren<InGameHitBox>(); 
			foreach(var _col in _inGameHitBoxArray)
			{
				_col.transform.SetParent(null);
				_col.gameObject.SetActive(false);
				Pool.ObjectPoolManager.Instance.RegisterObject("HitBox", _col.gameObject);
			}
		}

		public void OnHitBox(string _str)
		{
			HitBoxDataList hitBoxDataList = hitBoxDataSO.GetHitboxList(_str);
			if (hitBoxDataList is not null)
			{
				string tagname = gameObject.tag == "Player" ? "Player_Weapon" : "EnemyWeapon";
				foreach (HitBoxData hitBoxData in hitBoxDataSO.GetHitboxList(_str).hitBoxDataList)
				{
					GameObject hitbox = ObjectPoolManager.Instance.GetObject("HitBox");
					hitbox.GetComponent<InGameHitBox>().SetHitBox(hitBoxData, gameObject, tagname);
				}
			}
		}
	}
}
