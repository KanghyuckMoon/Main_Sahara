using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using System.Threading.Tasks;
using Utill.Pattern;

namespace Utill.Addressable
{
	/// <summary>
	/// ��巹���� ���ҽ� ������ ����ϴ� ��ƿ ��ũ��Ʈ
	/// </summary>
	public class AddressablesManager : Singleton<AddressablesManager>
	{
		private List<string> loadedScene = new List<string>();
		private Dictionary<string, AsyncOperationHandle<SceneInstance>> sceneInstanceDic = new Dictionary<string, AsyncOperationHandle<SceneInstance>>();
		private Queue<string> loadMessageQueue = new Queue<string>();

		/// <summary>
		/// ���ҽ��� �������� �Լ�
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="_name"></param>
		/// <returns></returns>
		public T GetResource<T>(string _name)
		{
			var _handle = Addressables.LoadAssetAsync<T>(_name);

			_handle.WaitForCompletion();

			return _handle.Result;
		}

		/// <summary>
		/// �񵿱�� ���ҽ��� �������� �Լ�, �ڵ��� �Ϸ�ɽÿ� �ݹ� �Լ��� �߰��ؾ���
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="_name"></param>
		/// <param name="_action"></param>
		public AsyncOperationHandle<T> GetResourceAsync<T>(string _name)
		{
			var _handle = Addressables.LoadAssetAsync<T>(_name);
			
			return _handle;
		}

		/// <summary>
		/// �񵿱�� ���ҽ��� �������� �Լ�, ������ �Լ��� ����� �־������
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="_name"></param>
		/// <param name="_action"></param>
		public AsyncOperationHandle<T> GetResourceAsync<T>(string _name, System.Action<T> _action)
		{
			var _handle = Addressables.LoadAssetAsync<T>(_name);
			_handle.Completed += (_x) =>
			{
				_action(_x.Result);
			};
			return _handle;
		}

		/// <summary>
		/// �񵿱�� ���ҽ��� �������� �Լ�, ������ �Լ��� ����� �־������
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <param name="_action"></param>
		public AsyncOperationHandle<T> GetResourceAsync<T, J>(string name, System.Action<T, J> _action, J _parameter)
		{
			var _handle = Addressables.LoadAssetAsync<T>(name);
			_handle.Completed += (_x) =>
			{
				_action(_x.Result, _parameter);
			};
			return _handle;
		}

		/// <summary>
		/// ���� �ҷ��´�
		/// </summary>
		/// <param name="_name"></param>
		/// <param name="_loadSceneMode"></param>
		/// <param name="_action"></param>
		public void LoadScene(string _name, LoadSceneMode _loadSceneMode, System.Action<AsyncOperationHandle<SceneInstance>> _action)
		{
			if (!loadedScene.Contains(_name))
			{
				loadedScene.Add(_name);
				var _handle = Addressables.LoadScene(_name, _loadSceneMode);
				_handle.Completed += (x) =>
				{
					sceneInstanceDic.Add(_name, _handle);
				};
				_handle.Completed += _action;
			}
		}

		/// <summary>
		/// ���� �񵿱�� �ҷ��´�
		/// </summary>
		/// <param name="_name"></param>
		/// <param name="_loadSceneMode"></param>
		/// <param name="_action"></param>
		public void LoadSceneAsync(string _name, LoadSceneMode _loadSceneMode, System.Action<AsyncOperationHandle<SceneInstance>> _action)
		{
			if (!loadedScene.Contains(_name))
			{
				loadedScene.Add(_name);
				var _handle = Addressables.LoadSceneAsync(_name, _loadSceneMode);
				_handle.Completed += (x) =>
				{
					sceneInstanceDic.Add(_name, _handle);
				};
				_handle.Completed += _action;
			}

		}
		
		/// <summary>
		/// ���� �����Ѵ�
		/// </summary>
		/// <param name="_name"></param>
		/// <param name="_action"></param>
		public void UnLoadScene(string _name, System.Action<AsyncOperationHandle<SceneInstance>> _action)
		{
			if (loadedScene.Contains(_name))
			{
				var _handle = Addressables.UnloadScene(sceneInstanceDic[_name]);
				if (_action is not null)
				{
					_handle.Completed += _action;
				}
				Addressables.Release(sceneInstanceDic[_name]);
				sceneInstanceDic.Remove(_name);
				loadedScene.Remove(_name);
			}
		}

		/// <summary>
		/// ���� �񵿱�� �����Ѵ�
		/// </summary>
		/// <param name="_name"></param>
		/// <param name="_action"></param>
		public void UnLoadSceneAsync(string _name, System.Action<AsyncOperationHandle<SceneInstance>> _action)
		{
			if (loadedScene.Contains(_name))
			{
				if (sceneInstanceDic.ContainsKey(_name))
				{
					var _handle = Addressables.UnloadSceneAsync(sceneInstanceDic[_name]);
					if (_action is not null)
					{
						_handle.Completed += _action;
					}
					Addressables.Release(sceneInstanceDic[_name]);
					sceneInstanceDic.Remove(_name);
					loadedScene.Remove(_name);
				}
			}
		}
	}

}