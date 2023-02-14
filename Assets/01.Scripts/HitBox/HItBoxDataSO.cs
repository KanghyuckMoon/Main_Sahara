using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.SeralizableDictionary;

namespace HitBox
{
	[System.Serializable]
	public class StringListHitBoxData : SerializableDictionary<string, HitBoxDataList> { }

	[CreateAssetMenu(fileName = "HItBoxDataSO", menuName = "SO/HItBoxDataSO")]
	public class HItBoxDataSO : ScriptableObject
	{
		public StringListHitBoxData hitBoxDataDic = new StringListHitBoxData();

		public void UploadHitBox(HitBoxData hitBoxData)
		{
			if (hitBoxData.hitBoxName is null)
			{
				Debug.LogError("��Ʈ�ڽ� �������� �̸��� ����");
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
				hitBoxDataList.hitBoxDataList.Add(HitBoxData.Copy(hitBoxData));
				hitBoxDataDic.Add(hitBoxData.hitBoxName, hitBoxDataList);
			}
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
		public string hitBoxName;
		public string ClassificationName;
		public int hitBoxIndex;
		public Vector3 offset;
		public Vector3 size;

		public static HitBoxData Copy(HitBoxData _hitBoxData)
		{
			HitBoxData _newHitBox = new HitBoxData();
			_newHitBox.hitBoxName = _hitBoxData.hitBoxName;
			_newHitBox.ClassificationName = _hitBoxData.ClassificationName;
			_newHitBox.hitBoxIndex = _hitBoxData.hitBoxIndex;
			_newHitBox.offset = _hitBoxData.offset;
			_newHitBox.size = _hitBoxData.size;

			return _newHitBox;
		}
	}

}