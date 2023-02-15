using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Utill.Pattern;

namespace HitBox
{
	public class HitBoxOnAnimation : MonoBehaviour
	{
		[SerializeField]
		private HitBoxDataSO hitBoxDataSO;

		//private string colliderKey;

		public void ChangeSO(HitBoxDataSO _hitBoxDataSO)//, string _colliderKey)
		{
			hitBoxDataSO = _hitBoxDataSO;
			//colliderKey = _colliderKey;
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
