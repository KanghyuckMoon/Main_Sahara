using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.SeralizableDictionary;

namespace Effect
{
	[System.Serializable]
	public class StringListEffectData : SerializableDictionary<string, EffectDataList> { }
	[CreateAssetMenu(fileName = "AnimationEffectSO", menuName = "SO/AnimationEffectSO")]
	public class AnimationEffectSO : ScriptableObject
	{
		public StringListEffectData effectDataListDic = new StringListEffectData();

		public string removeEffectKey = null;

		public List<string> debugList = new List<string>();

		[ContextMenu("ResetDic")]
		public void ResetDic()
		{
			effectDataListDic.Clear();
			foreach (var _obj in debugList)
			{
				EffectDataList _effectDataList = new EffectDataList();
				effectDataListDic.Add(_obj, _effectDataList);
			}
		}
		
		public void UploadEffectData(string _key, EffectData _effectData)
		{
			if (_key is null)
			{
				Debug.LogError("이펙트 데이터의 이름이 없음");
				return;
			}

			if (effectDataListDic.TryGetValue(_key, out var list))
			{
				EffectData _findEffectData = list.effectDataList.Find(x => x.index == _effectData.index);
				if (_findEffectData is not null)
				{
					_findEffectData.Copy(_effectData);
				}
				else
				{
					list.effectDataList.Add(EffectData.CopyNew(_effectData));
				}
			}
			else
			{
				EffectDataList _effectDataList = new EffectDataList();
				_effectDataList.effectDataList.Add(EffectData.CopyNew(_effectData));
				effectDataListDic.Add(_key, _effectDataList);
			}

#if UNITY_EDITOR
			UnityEditor.EditorUtility.SetDirty(this);
#endif
		}

		public void RemoveEffect()
		{
			effectDataListDic.Remove(removeEffectKey);
		}

		public EffectDataList GetEffectList(string str)
		{
			if (effectDataListDic.TryGetValue(str, out var value))
			{
				return value;
			}
			return null;
		}

	}

	[System.Serializable]
	public class EffectDataList
	{
		public List<EffectData> effectDataList = new List<EffectData>();
	}

	[System.Serializable]
	public class EffectData
	{
		public string effectcAddress = null;
		public int index = 0;
		public Vector3 offset = Vector3.zero;
		public Vector3 rotation = Vector3.zero;
		public Vector3 scale = Vector3.one;
		public bool childization = false;

		public void Copy(EffectData _effectData)
		{
			effectcAddress = _effectData.effectcAddress;
			offset = _effectData.offset;
			rotation = _effectData.rotation;
			scale = _effectData.scale;
			childization = _effectData.childization;
		}
		public static EffectData CopyNew(EffectData _effectData)
		{
			EffectData _neweffectData = new EffectData();
			_neweffectData.effectcAddress = _effectData.effectcAddress;
			_neweffectData.offset = _effectData.offset;
			_neweffectData.rotation = _effectData.rotation;
			_neweffectData.scale = _effectData.scale;
			_neweffectData.childization = _effectData.childization;
			return _neweffectData;
		}
	}
}
