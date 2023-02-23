using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using Pool;
using Streaming;

namespace Spawner
{
	public class LODObjectDataRegister : MonoBehaviour
	{
		private static Dictionary<string, bool> isSpawnDic = new Dictionary<string, bool>();
		private static int nameKey;

		[SerializeField]
		private string spawnerName;
		[SerializeField]
		private string lodAddress;
		[SerializeField]
		private ObjectDataSO objectDataSO;

		[ContextMenu("RandomName")]
		public void RandomName()
		{
			spawnerName = gameObject.name + nameKey++;
		}


		public void OnEnable()
		{
			if (isSpawnDic.TryGetValue(spawnerName, out bool _bool))
			{
				if (_bool)
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
				_objectData.key = ObjectData.totalKey++;
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