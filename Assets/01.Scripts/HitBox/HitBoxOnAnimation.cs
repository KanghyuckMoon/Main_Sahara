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

#if UNITY_EDITOR
		[ContextMenu("AddEvent")]
		public void AddEvent()
		{
			BoxColEditor _boxColEditor = GetComponent<BoxColEditor>();
			_boxColEditor.Refresh();
			_boxColEditor.Upload();
			//Animator _animator = GetComponent<Animator>();
			AnimationClip _animationClip = AnimationHitBoxEditor.GetAnimationWindowAnimationClip();
			AnimationEvent e = new AnimationEvent();
			e.functionName = "OnHitBox";
			e.stringParameter = _boxColEditor.hitBoxData.hitBoxName;
			e.time = AnimationHitBoxEditor.GetAnimationWindowTime();
			AnimationEvent[] animationEvents = new AnimationEvent[_animationClip.events.Length + 1];
			for(int i = 0; i < _animationClip.events.Length; ++i)
			{
				animationEvents[i] = _animationClip.events[i];
			}
			animationEvents[_animationClip.events.Length] = e;
			UnityEditor.AnimationUtility.SetAnimationEvents(_animationClip, animationEvents);
		}

		[ContextMenu("CheckFrame")]
		public void CheckFrame()
		{
			Debug.Log(AnimationHitBoxEditor.GetAnimationWindowCurrentFrame());
		}

		[ContextMenu("CheckTime")]
		public void CheckTime()
		{
			Debug.Log(AnimationHitBoxEditor.GetAnimationWindowTime());
		}
#endif
	}
}
