using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using Pool;
using Streaming;

namespace Spawner
{
	public class MonsterSpawner : MonoBehaviour
	{
		private static Dictionary<string, bool> isSpawnDic = new Dictionary<string, bool>();

		[SerializeField]
		private string spawnerName;
		[SerializeField]
		private string lodAddress;
		[SerializeField]
		private ObjectDataSO objectDataSO;
		

		public void OnEnable()
		{
			if(isSpawnDic.TryGetValue(spawnerName, out bool _bool))
			{
				if(_bool)
				{
					return;
				}
				else
				{
					_bool = true;
				}
			}
			else
			{
				isSpawnDic.Add(spawnerName, true);
				//GameObject obj = gameObject;
				ObjectData _objectData = new ObjectData();
				_objectData.position = transform.position;
				_objectData.rotation = transform.rotation;
				_objectData.scale = transform.localScale;
				_objectData.lodAddress = lodAddress;
				_objectData.lodType = LODType.On;
				SceneDataManager.Instance.GetSceneData(transform.position).AddOnlyLODObjectData(_objectData);


			}
		}

	}
}
