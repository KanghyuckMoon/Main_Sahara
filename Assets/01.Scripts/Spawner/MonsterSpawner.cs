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
		private string enemyAddress;
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
				GameObject obj = ObjectPoolManager.Instance.GetObject(enemyAddress);
				ObjectClassCycle objectClassCycle = obj.GetComponentInChildren<ObjectClassCycle>();
				objectClassCycle.TargetObject = obj;
				ObjectData objectData = new ObjectData();
				objectData.CopyObjectDataSO(objectDataSO);
				ObjectSceneChecker _objectSceneChecker = ClassPoolManager.Instance.GetClass<ObjectSceneChecker>("ObjectSceneChecker");
				if (_objectSceneChecker is null)
				{
					_objectSceneChecker = new ObjectSceneChecker();
				}
				_objectSceneChecker.ObjectClassCycle = objectClassCycle;
				_objectSceneChecker.ObjectData = objectData;
				objectClassCycle.AddObjectClass(_objectSceneChecker);
				obj.transform.position = transform.position;
				obj.SetActive(true);

			}
		}

	}
}
