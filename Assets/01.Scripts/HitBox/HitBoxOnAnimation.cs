using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Utill.Pattern;
using System.Linq;
using UnityEngine.Serialization;
using Utill.Measurement;

namespace HitBox
{
	public class HitBoxOnAnimation : MonoBehaviour
	{
		private ulong index;

		[SerializeField]
		private HitBoxDatasSO mainHitBoxDataSO;
		[SerializeField]
		private HitBoxDatasSO subHitBoxDataSO;

		[SerializeField] 
		private Transform waeponHandle;
		
		public HitBoxInAction HitBoxInAction
		{
			set { hitBoxInAction = value;}
			get { return hitBoxInAction; }
		}
		public HitBoxInAction hitBoxInAction;
		public HitBoxAction hitBoxAction;
			
		private void Start()
		{
			index = StaticHitBoxIndex.GetHitBoxIndex();
		}

		public void ChangeSO(HitBoxDatasSO _hitBoxDataSO)//, string _colliderKey)
		{
			if(_hitBoxDataSO.mainHitBox is null)
			{
				mainHitBoxDataSO = _hitBoxDataSO;
				subHitBoxDataSO = _hitBoxDataSO;
			}
			else
			{
				mainHitBoxDataSO = _hitBoxDataSO.mainHitBox;
				subHitBoxDataSO = _hitBoxDataSO;
			}
			//colliderKey = _colliderKey;
		}

		public void OnHitBox(string _str)
		{
			HitBoxDataList hitBoxDataList = null;
			if (subHitBoxDataSO != null)
			{
				hitBoxDataList = subHitBoxDataSO.GetHitboxList(_str);
			}
			hitBoxDataList ??= mainHitBoxDataSO.GetHitboxList(_str);
			Logging.Log(_str);
			if (hitBoxDataList is not null)
			{
				string tagname = gameObject.tag == "Player" ? "Player_Weapon" : "EnemyWeapon";
				foreach (HitBoxData hitBoxData in hitBoxDataList.hitBoxDataList)
				{
					Logging.Log("In : " + _str);
					InGameHitBox hitbox = HitBoxPoolManager.Instance.GetObject();
					if (waeponHandle == null)
					{
						hitbox.SetHitBox(hitbox.gameObject, index + hitBoxData.hitBoxIndex, hitBoxData, gameObject, tagname, null, null);
					}
					else
					{
						hitbox.SetHitBox(hitbox.gameObject,index + hitBoxData.hitBoxIndex, hitBoxData, gameObject, tagname, waeponHandle.gameObject, waeponHandle.gameObject, hitBoxAction);
					}
				}
			}
		}

		public void UpIndex()
		{
			index += 50;
		}

#if UNITY_EDITOR

		[Header("DEBUGOPTION")]
		public string hitBoxName = "";
		
		[SerializeField]
		private HitBoxDatasSO debugHitBoxDataSO;
		
		[SerializeField]
		private CapsuleColEditor capsuleColEditor;

		[ContextMenu("AddEvent")]
		public void AddEvent()
		{
			if(hitBoxName is null)
			{
				return;
			}
			capsuleColEditor.hitBoxDataSO = debugHitBoxDataSO;
			HitBoxData _hitBoxData = new HitBoxData();
			_hitBoxData.hitBoxName = hitBoxName;
			capsuleColEditor.SetHitBox(_hitBoxData);
			capsuleColEditor.Upload();
			
			//Animator _animator = GetComponent<Animator>();
			AnimationClip _animationClip = AnimationHitBoxEditor.GetAnimationWindowAnimationClip();
			AnimationEvent e = new AnimationEvent();
			e.functionName = "OnHitBox";
			e.stringParameter = capsuleColEditor.hitBoxData.hitBoxName;
			e.time = AnimationHitBoxEditor.GetAnimationWindowTime();
			AnimationEvent[] animationEvents = new AnimationEvent[_animationClip.events.Length + 1];
			for(int i = 0; i < _animationClip.events.Length; ++i)
			{
				animationEvents[i] = _animationClip.events[i];
			}
			animationEvents[_animationClip.events.Length] = e;
			UnityEditor.AnimationUtility.SetAnimationEvents(_animationClip, animationEvents);
		}

		[ContextMenu("GetEvent")]
		public void GetEvent()
		{
			AnimationClip _animationClip = AnimationHitBoxEditor.GetAnimationWindowAnimationClip();
			float _time = AnimationHitBoxEditor.GetAnimationWindowTime();
			AnimationEvent _animationEvent = _animationClip.events.FirstOrDefault(x => x.time == _time);
			if(_animationEvent.functionName is "OnHitBox")
			{
				if (_animationEvent.stringParameter is not null)
				{
					HitBoxDataList hitBoxDataList = debugHitBoxDataSO.hitBoxDataDic[_animationEvent.stringParameter];
					for (int i = 0; i < hitBoxDataList.hitBoxDataList.Count; ++i)
					{
						capsuleColEditor.SetHitBox(hitBoxDataList.hitBoxDataList[i]);
						capsuleColEditor.hitBoxDataSO = debugHitBoxDataSO;
					}
				}
			}
		}

		[ContextMenu("CheckFrame")]
		public void CheckFrame()
		{
			Logging.Log(AnimationHitBoxEditor.GetAnimationWindowCurrentFrame());
		}

		[ContextMenu("CheckTime")]
		public void CheckTime()
		{
			Logging.Log(AnimationHitBoxEditor.GetAnimationWindowTime());
		}
#endif
	}
}
