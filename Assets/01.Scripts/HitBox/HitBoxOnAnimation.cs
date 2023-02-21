using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Utill.Pattern;
using System.Linq;

namespace HitBox
{
	public class HitBoxOnAnimation : MonoBehaviour
	{
		private ulong index;

		[SerializeField]
		private HitBoxDatasSO hitBoxDataSO;

		private void Start()
		{
			index = StaticHitBoxIndex.GetHitBoxIndex();
		}

		public void ChangeSO(HitBoxDatasSO _hitBoxDataSO)//, string _colliderKey)
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
					hitbox.GetComponent<InGameHitBox>().SetHitBox(index + hitBoxData.hitBoxIndex, hitBoxData, gameObject, tagname);

				}
			}
		}

		public void UpIndex()
		{
			index += 50;
		}

#if UNITY_EDITOR

		private List<BoxColEditor> boxColEditorList = new List<BoxColEditor>();

		public string hitBoxName = "";

		[ContextMenu("AddEvent")]
		public void AddEvent()
		{
			Delete();
			if(hitBoxName is null)
			{
				return;
			}
			GameObject obj = new GameObject();
			BoxColEditor _boxColEditor = obj.AddComponent<BoxColEditor>();
			HitBoxData _hitBoxData = new HitBoxData();
			_hitBoxData.hitBoxName = hitBoxName;
			_boxColEditor.hitBoxDataSO = hitBoxDataSO;
			_boxColEditor.SetHitBox(_hitBoxData);
			_boxColEditor.UploadNoneCopy();
			boxColEditorList.Add(_boxColEditor);

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

		[ContextMenu("GetEvent")]
		public void GetEvent()
		{
			Delete();
			AnimationClip _animationClip = AnimationHitBoxEditor.GetAnimationWindowAnimationClip();
			float _time = AnimationHitBoxEditor.GetAnimationWindowTime();
			AnimationEvent _animationEvent = _animationClip.events.FirstOrDefault(x => x.time == _time);
			if(_animationEvent.functionName is "OnHitBox")
			{
				if (_animationEvent.stringParameter is not null)
				{
					HitBoxDataList hitBoxDataList = hitBoxDataSO.hitBoxDataDic[_animationEvent.stringParameter];
					for (int i = 0; i < hitBoxDataList.hitBoxDataList.Count; ++i)
					{
						GameObject obj = new GameObject();
						BoxColEditor boxColEditor = obj.AddComponent<BoxColEditor>();
						boxColEditor.SetHitBox(hitBoxDataList.hitBoxDataList[i]);
						boxColEditorList.Add(boxColEditor);
					}
				}
			}
		}

		[ContextMenu("Delete")]
		public void Delete()
		{
			if(boxColEditorList.Count > 0)
			{ 
				for(int i = 0; i < boxColEditorList.Count; ++i)
				{
					DestroyImmediate(boxColEditorList[i].gameObject);
				}
			}
			boxColEditorList.Clear();
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
