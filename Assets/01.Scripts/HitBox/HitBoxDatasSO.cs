using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.SeralizableDictionary;
using System.Linq;
using Buff;

namespace HitBox
{
	[System.Serializable]
	public class StringListHitBoxData : SerializableDictionary<string, HitBoxDataList> { }

	[CreateAssetMenu(fileName = "HitBoxDatasSO", menuName = "SO/HitBoxDatasSO")]
	public class HitBoxDatasSO : ScriptableObject
	{
		public StringListHitBoxData hitBoxDataDic = new StringListHitBoxData();
		public List<BuffData> setAllHitBoxBuffList = new List<BuffData>();
		
		public HitBoxDataList GetHitboxList(string str)
		{
			if (hitBoxDataDic.TryGetValue(str, out var value))
			{
				return value;
			}
			return null;
		}

		[ContextMenu("SetAllHitBoxBuff")]
		public void SetAllHitBoxBuff()
		{
			foreach (var _hitList in hitBoxDataDic)
			{
				foreach (var _hit in _hitList.Value.hitBoxDataList)
				{
					_hit.buffList =  setAllHitBoxBuffList.Select(x => new BuffData(x)).ToList();
				}
			}
		}

		public void UploadHitBox(HitBoxData hitBoxData)
		{
			if (hitBoxData.hitBoxName is null)
			{
				Debug.LogError("히트박스 데이터의 이름이 없음");
				return;
			}

			if(hitBoxDataDic.TryGetValue(hitBoxData.hitBoxName, out var list))
			{
				HitBoxData _hitBoxClassificationData = list.hitBoxDataList.Find(x => x.ClassificationName == hitBoxData.ClassificationName);
				if (_hitBoxClassificationData is not null)
				{
					_hitBoxClassificationData = hitBoxData;
				}
				else
				{
					list.hitBoxDataList.Add(hitBoxData);
				}
			}
			else
			{
				HitBoxDataList hitBoxDataList = new HitBoxDataList();
				hitBoxDataList.hitBoxDataList.Add(HitBoxData.CopyNew(hitBoxData));
				hitBoxDataDic.Add(hitBoxData.hitBoxName, hitBoxDataList);
			}

#if UNITY_EDITOR
			UnityEditor.EditorUtility.SetDirty(this);
#endif
		}
		public void UploadHitBoxNoneCopy(HitBoxData hitBoxData)
		{
			if (hitBoxData.hitBoxName is null)
			{
				Debug.LogError("히트박스 데이터의 이름이 없음");
				return;
			}

			if (hitBoxDataDic.TryGetValue(hitBoxData.hitBoxName, out var list))
			{
				HitBoxData _hitBoxClassificationData = list.hitBoxDataList.Find(x => x.ClassificationName == hitBoxData.ClassificationName);
				if (_hitBoxClassificationData is not null)
				{
					_hitBoxClassificationData.Copy(hitBoxData);
				}
				else
				{
					list.hitBoxDataList.Add(HitBoxData.CopyNew(hitBoxData));
				}
			}
			else
			{
				HitBoxDataList hitBoxDataList = new HitBoxDataList();
				hitBoxDataList.hitBoxDataList.Add(HitBoxData.CopyNew(hitBoxData));
				hitBoxDataDic.Add(hitBoxData.hitBoxName, hitBoxDataList);
			}

#if UNITY_EDITOR
			UnityEditor.EditorUtility.SetDirty(this);
#endif
		}
	}

	[System.Serializable]
	public class HitBoxDataList
	{
		public List<HitBoxData> hitBoxDataList = new List<HitBoxData>();
	}

	[System.Serializable]
	public class HitBoxData
	{
		//판정
		public string hitBoxName = "NULL";
		public string ClassificationName = "NULL";
		public ulong hitBoxIndex;
		public float deleteDelay = 0.1f;
		public bool childization = false;
		public Vector3 offset = Vector3.zero;
		public float radius = 1f;
		public float height = 1f;
		public Vector3 rotation = Vector3.zero;

		//넉백
		public bool isContactDirection = false; 
		public Vector3 knockbackDir = Vector3.forward;
		public float defaultPower = 1f;

		//이펙트
		public Vector3 swingEffectOffset = Vector3.zero;
		public Vector3 swingEffectRotation = Vector3.zero;
		public Vector3 swingEffectSize = Vector3.one;
		public float hitStunDelay;
		public float attackStunDelay;
		public bool swingEffectChildization = false;
		public string swingEffect = "NULL";
		public string hitEffect = "NULL";
		
		//버프, 디버프
		public List<BuffData> buffList = new List<BuffData>();
		
		//데미지 
		public float physicsAttackWeight = 1f;
		public float magicalAttackWeight = 1f;

		public static HitBoxData CopyNew(HitBoxData _hitBoxData)
		{
			HitBoxData _newHitBox = new HitBoxData();
			_newHitBox.hitBoxName = _hitBoxData.hitBoxName;
			_newHitBox.ClassificationName = _hitBoxData.ClassificationName;
			_newHitBox.hitBoxIndex = _hitBoxData.hitBoxIndex;
			_newHitBox.deleteDelay = _hitBoxData.deleteDelay;
			_newHitBox.childization = _hitBoxData.childization;
			_newHitBox.offset = _hitBoxData.offset;
			_newHitBox.radius = _hitBoxData.radius;
			_newHitBox.rotation = _hitBoxData.rotation;

			_newHitBox.knockbackDir = _hitBoxData.knockbackDir;
			_newHitBox.defaultPower = _hitBoxData.defaultPower;

			_newHitBox.swingEffectOffset = _hitBoxData.swingEffectOffset;
			_newHitBox.swingEffectRotation = _hitBoxData.swingEffectRotation;
			_newHitBox.swingEffectSize = _hitBoxData.swingEffectSize;
			_newHitBox.swingEffect = _hitBoxData.swingEffect;
			_newHitBox.swingEffectChildization = _hitBoxData.swingEffectChildization;
			_newHitBox.hitEffect = _hitBoxData.hitEffect;
			_newHitBox.buffList = _hitBoxData.buffList;
			
			_newHitBox.physicsAttackWeight = _hitBoxData.physicsAttackWeight;
			_newHitBox.magicalAttackWeight = _hitBoxData.magicalAttackWeight;

			return _newHitBox;
		}

		public void Copy(HitBoxData _hitBoxData)
		{
			hitBoxName = _hitBoxData.hitBoxName;
			ClassificationName = _hitBoxData.ClassificationName;
			hitBoxIndex = _hitBoxData.hitBoxIndex;
			deleteDelay = _hitBoxData.deleteDelay;
			childization = _hitBoxData.childization;
			offset = _hitBoxData.offset;
			radius = _hitBoxData.radius;
			rotation = _hitBoxData.rotation;

			knockbackDir = _hitBoxData.knockbackDir;
			defaultPower = _hitBoxData.defaultPower;

			swingEffectOffset = _hitBoxData.swingEffectOffset;
			swingEffectRotation = _hitBoxData.swingEffectRotation;
			swingEffectSize = _hitBoxData.swingEffectSize;
			swingEffect = _hitBoxData.swingEffect;
			swingEffectChildization = _hitBoxData.swingEffectChildization;
			hitEffect = _hitBoxData.hitEffect;
			
			buffList = _hitBoxData.buffList;
			
			physicsAttackWeight = _hitBoxData.physicsAttackWeight;
			magicalAttackWeight = _hitBoxData.magicalAttackWeight;
		}
	}

}