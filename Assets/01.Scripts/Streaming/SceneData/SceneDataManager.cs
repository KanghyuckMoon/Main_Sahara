using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill.Pattern;
using Utill.Addressable;


namespace Streaming
{
	public class SceneDataManager : MonoSingleton<SceneDataManager>
	{
		public Dictionary<string, SceneData> SceneDataDic
		{
			get
			{
				return sceneDataDic;
			}
			set
			{
				sceneDataDic = value;
			}
		}
		private Dictionary<string, SceneData> sceneDataDic = new Dictionary<string, SceneData>();
		private SceneAddressSO sceneAddressSO = null;
		private void Start()
		{
			sceneAddressSO = AddressablesManager.Instance.GetResource<SceneAddressSO>("SceneAddressSO");
			foreach (string _sceneName in sceneAddressSO.sceneAddressList)
			{
				sceneDataDic.Add(_sceneName, new SceneData(_sceneName));
			}
		}

		/// <summary>
		/// 씬 이름으로 씬 데이터를 가져옴
		/// </summary>
		/// <param name="_sceneName"></param>
		/// <returns></returns>
		public SceneData GetSceneData(string _sceneName)
		{
			if (sceneDataDic.TryGetValue(_sceneName, out var _sceneData))
			{
				return _sceneData;
			}

			return null;
		}

		/// <summary>
		/// 해당 씬 데이터에 오브젝트 데이터가 몇 개 있는지 가져옴
		/// </summary>
		/// <param name="_sceneName"></param>
		/// <returns></returns>
		public int GetObjectDataCount(string _sceneName)
		{
			SceneData _sceneData = GetSceneData(_sceneName);
			if (_sceneData is null)
			{
				return 0;
			}
			return _sceneData.ObjectDataList.Count;
		}
		
		/// <summary>
		/// 해당 씬 데이터에 활성화된 오브젝트가 몇 개 있는지 가져옴
		/// </summary>
		/// <param name="_sceneName"></param>
		/// <returns></returns>
		public int GetObjectCount(string _sceneName)
		{
			SceneData _sceneData = GetSceneData(_sceneName);
			if (_sceneData is null)
			{
				return 0;
			}
			return _sceneData.ObjectCheckerList.Count;
		}

		/// <summary>
		/// 모든 씬 데이터의 오브젝트 데이터 갯수를 가져옴
		/// </summary>
		/// <returns></returns>
		public int AllGetObjectDataCount()
		{
			int _count = 0;
			foreach (var _sceneData in sceneDataDic)
			{
				_count += GetObjectDataCount(_sceneData.Key);
			}
			return _count;
		}

		/// <summary>
		/// 모든 씬 데이터의 활성화된 오브젝트들을 가져옴
		/// </summary>
		/// <returns></returns>
		public int AllGetObjectCount()
		{
			int _count = 0;
			foreach (var _sceneData in sceneDataDic)
			{
				_count += GetObjectCount(_sceneData.Key);
			}
			return _count;
		}

	}
}
