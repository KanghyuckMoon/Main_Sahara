using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using Pool;
using Streaming;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;

#endif

namespace Spawner
{
	public class LODObjectDataRegister : MonoBehaviour
	{
		private static Dictionary<string, bool> isSpawnDic = new Dictionary<string, bool>();
		private static int nameKey;

		[SerializeField]
		private string lodAddress;
		[SerializeField]
		private ObjectDataSO objectDataSO;
		
		[SerializeField]
		private bool isNotEnable;

		[SerializeField] 
		private string key;
		
#if UNITY_EDITOR
		[ContextMenu("RandomName")]
		public void RandomName()
		{
			var _prefeb = UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(gameObject);
			//gameObject.name = _prefeb.name + nameKey++;
			key = _prefeb.name + nameKey++;
			#if UNITY_EDITOR
			EditorSceneManager.MarkSceneDirty(gameObject.scene);
			
			#endif
		}
		[ContextMenu("SetLODName")]
		public void SetLODName()
		{
			//var _prefeb = UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(gameObject);
			lodAddress = gameObject.name + "LOD";
		}
#endif

		public void OnEnable()
		{
			if (isNotEnable)
			{
				return;
			}

			SetLOD();
		}

		public void SetLOD()
		{

			if (isSpawnDic.TryGetValue(key, out bool _bool))
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
				isSpawnDic.Add(gameObject.name, true);
				//GameObject obj = gameObject;
				ObjectData _objectData = new ObjectData();
				_objectData.key = ObjectData.totalKey++;
				_objectData.position = transform.position;
				_objectData.rotation = transform.rotation;
				_objectData.scale = transform.localScale;
				_objectData.lodAddress = lodAddress;
				_objectData.lodType = LODType.On;
				SceneData _sceneData = SceneDataManager.Instance.GetSceneData(gameObject.scene.name);
				_sceneData.AddOnlyLODObjectData(_objectData);
			}
		}
	}
}