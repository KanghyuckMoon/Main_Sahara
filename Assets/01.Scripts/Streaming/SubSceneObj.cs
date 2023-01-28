using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill.Addressable;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using static Streaming.StreamingUtill;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Streaming
{
	[RequireComponent(typeof(LODMaker))]
	public class SubSceneObj : MonoBehaviour
	{
		/// <summary>
		/// 맡은 씬 이름
		/// </summary>
		public string SceneName
		{
			get
			{
				return sceneName;
			}
		}

		public LODMaker LODMaker
		{
			get
			{
				if (lodMaker is null)
				{
					lodMaker ??= GetComponent<LODMaker>();
					lodMaker?.Init(this);
				}
				return lodMaker;
			}
			set
			{
				lodMaker = value;
			}
		}
		[SerializeField]
		private string sceneName;
		[SerializeField]
		private LODMaker lodMaker;

#if UNITY_EDITOR
		public Scene EditingScene
		{
			get
			{
				if (sceneAsset == null)
					return default(Scene);

				return SceneManager.GetSceneByPath(AssetDatabase.GetAssetPath(sceneAsset));
			}
		}

		public GUISkin GUISkin
		{
			get
			{
				return guiSkin;
			}
			set
			{
				guiSkin = value;
			}
		}
		[SerializeField]
		private SceneAsset sceneAsset;
		[SerializeField]
		private bool isLoadScene;
		[SerializeField]
		private GUISkin guiSkin;


		/// <summary>
		/// 맡고 있는 씬 에셋을 설정
		/// </summary>
		/// <param name="_scene"></param>
		public void SetSceneAsset(SceneAsset _scene)
		{
			//씬 에셋 설정
			sceneAsset = _scene;
		}
		
		/// <summary>
		/// 씬 에셋 등이 변경되면 해당 내용에 따라 변수들을 바꾸고 씬을 불러옴
		/// </summary>
		public void OnValidate()
		{
			if (sceneAsset == null)
			{
				return;
			}
			else
			{
				SceneAssetToSceneName();
				gameObject.name = sceneAsset.name;
				transform.position = StringToVector3AndScale(sceneAsset.name);
			}

			Scene _scene = EditingScene;

			if (isLoadScene)
			{
				string _path = AssetDatabase.GetAssetPath(sceneAsset);
				EditorSceneManager.OpenScene(_path, OpenSceneMode.Additive);
			}
			else
			{
				if (_scene.isLoaded)
				{
					SceneManager.UnloadSceneAsync(_scene);
					//EditorSceneManager.CloseScene(scene, false); 작동 안 됨
				}
			}
		}

		/// <summary>
		/// 씬 에셋 이름을 씬 이름에 할당
		/// </summary>
		[ContextMenu("SceneAssetToSceneName")]
		public void SceneAssetToSceneName()
		{
			sceneName = sceneAsset.name;
		}

#endif

		public void Start()
		{
			if (SceneName != null)
			{
				SceneDataManager.Instance.GetSceneData(SceneName).SetLoadSceneObjNLodMaker(this, LODMaker);
			}
		}

		public void OnDrawGizmos()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireCube(transform.position, Vector3.one * 100);


#if UNITY_EDITOR
			if (guiSkin == null)
			{
				return;
			}
			Handles.Label(transform.position, sceneAsset.name, guiSkin.GetStyle("MapStyle"));

			if (Application.isPlaying && SceneDataManager.Instance is not null)
			{
				Handles.Label(new Vector3(transform.position.x, transform.position.y + 40, transform.position.z), $"{SceneDataManager.Instance.GetSceneData(SceneName).ObjectDataList.Count}", guiSkin.GetStyle("CountStyle"));
			}
#endif
		}

		/// <summary>
		/// 씬이 활성화된 상태인지 체크한다
		/// </summary>
		/// <returns></returns>
		public bool IsActiveScene()
		{
			return CheckCurrentlyActhive(SceneName);
		}

		/// <summary>
		/// 맡은 씬을 불러온다
		/// </summary>
		public void LoadScene()
		{
			if (!IsActiveScene())
			{
				AddressablesManager.Instance.LoadSceneAsync(SceneName, LoadSceneMode.Additive, LoadSceneObject);
			}
		}

		/// <summary>
		/// 맡은 씬이 활성화되어 있어 있다면 비활성화한다
		/// </summary>
		public void UnLoadScene()
		{
			if (IsActiveScene())
			{
				AddressablesManager.Instance.UnLoadSceneAsync(SceneName, UnLoadSceneObject);
			}
		}

		private void LoadSceneObject(AsyncOperationHandle<SceneInstance> obj)
		{
			if (obj.Status == AsyncOperationStatus.Succeeded)
			{
				SceneDataManager.Instance.GetSceneData(SceneName).Load();
				LODMaker.Load();
			}
		}

		private void UnLoadSceneObject(AsyncOperationHandle<SceneInstance> obj)
		{
			if (obj.Status == AsyncOperationStatus.Succeeded)
			{
				SceneDataManager.Instance.GetSceneData(SceneName).UnLoad();
				LODMaker.UnLoad();
			}
		}

#if UNITY_EDITOR

#endif

		#region DebugCode

		/// <summary>
		/// 중앙제단을 오브젝트 데이터에 추가함
		/// </summary>
		[ContextMenu("AddObjectInScene")]
		public void AddObjectInScene()
		{
			ObjectData _obj1 = new ObjectData();
			_obj1.address = "Middle";
			_obj1.lodType = LODType.On;
			_obj1.lodAddress = "MiddleLOD";
			_obj1.key = ObjectData.totalKey++;
			_obj1.position = StringToVector3(sceneName) * 100f + new Vector3(0, 20, 0);
			_obj1.rotation = Quaternion.identity;
			_obj1.scale = new Vector3(1, 1, 1) * 0.2f;

			SceneDataManager.Instance.GetSceneData(SceneName).AddObjectData(_obj1);
		}

		#endregion

	}

}