using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill.Pattern;
using Utill.Addressable;
using static Streaming.StreamingUtill;


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
		private void Awake()
		{
			sceneAddressSO = AddressablesManager.Instance.GetResource<SceneAddressSO>("SceneAddressSO");
			foreach (string _sceneName in sceneAddressSO.sceneAddressList)
			{
				sceneDataDic.Add(_sceneName, new SceneData(_sceneName));
			}
		}

		/// <summary>
		/// ��ġ�� �ش��ϴ� �� �����͸� ������
		/// </summary>
		/// <param name="_sceneName"></param>
		/// <returns></returns>
		public SceneData GetSceneData(Vector3 _pos)
		{
			int _posX = Mathf.RoundToInt(_pos.x / 100);
			int _posY = Mathf.RoundToInt(_pos.y / 100);
			int _posZ = Mathf.RoundToInt(_pos.z / 100);

			if (sceneDataDic.TryGetValue(NameFromPosition(_posX, _posY, _posZ), out var _sceneData))
			{
				return _sceneData;
			}

			return null;
		}
		/// <summary>
		/// �� �̸����� �� �����͸� ������
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
		/// �ش� �� �����Ϳ� ������Ʈ �����Ͱ� �� �� �ִ��� ������
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
			return _sceneData.ObjectDataList.objectDataList.Count;
		}
		
		/// <summary>
		/// �ش� �� �����Ϳ� Ȱ��ȭ�� ������Ʈ�� �� �� �ִ��� ������
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
		/// ��� �� �������� ������Ʈ ������ ������ ������
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
		/// ��� �� �������� Ȱ��ȭ�� ������Ʈ���� ������
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
