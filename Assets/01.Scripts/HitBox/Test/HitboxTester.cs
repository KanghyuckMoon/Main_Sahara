using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;

namespace HitBox
{
	public class HitboxTester : MonoBehaviour
	{
		public HitBoxDataSO hitBoxDataSO;
		public string hitboxStr;
		 
		[ContextMenu("SetHitBox")]
		public void SetHitBox()
		{
			foreach(HitBoxData hitBoxData in hitBoxDataSO.GetHitboxList(hitboxStr).hitBoxDataList)
			{
				GameObject hitbox = ObjectPoolManager.Instance.GetObject("HitBox");
				hitbox.GetComponent<InGameHitBox>().SetHitBox(hitBoxData, gameObject, "Player_Weapon");
			}
		}
	}
}
