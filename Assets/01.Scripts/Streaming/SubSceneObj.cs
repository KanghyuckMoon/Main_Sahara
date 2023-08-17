using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill.Addressable;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using static Streaming.StreamingUtill;
using EventQueue;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Streaming
{
    [RequireComponent(typeof(LODMaker)), RequireComponent(typeof(LODGroup))]
    public class SubSceneObj : MonoBehaviour
    {
        /// <summary>
        /// ���� �� �̸�
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
                if (lodMaker == null)
                {
                    lodMaker = GetComponent<LODMaker>();
                    if (lodMaker != null)
                    {
                        lodMaker.Init(this);
                    }
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
		private bool isLoadBool;
        [SerializeField]
        private bool isDebugLoaded;
		[SerializeField]
		private GUISkin guiSkin;
        private bool isLoadSceneNotSave;

		/// <summary>
		/// �ð� �ִ� �� ������ ����
		/// </summary>
		/// <param name="_scene"></param>
		public void SetSceneAsset(SceneAsset _scene)
		{
			//�� ���� ����
			sceneAsset = _scene;
		}
		
		/// <summary>
		/// �� ���� ���� ����Ǹ� �ش� ���뿡 ���� �������� �ٲٰ� ���� �ҷ���
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

            if(isLoadBool)
            {
                isLoadSceneNotSave = !isLoadSceneNotSave;
                isDebugLoaded = isLoadSceneNotSave;
            }
            isLoadBool = false;

			if (isLoadSceneNotSave)
			{
				string _path = AssetDatabase.GetAssetPath(sceneAsset);
				EditorSceneManager.OpenScene(_path, OpenSceneMode.Additive);
			}
			else
			{
				if (_scene.isLoaded)
				{
					SceneManager.UnloadSceneAsync(_scene);
					//EditorSceneManager.CloseScene(scene, false); �۵� �� ��
				}
			}
		}

		/// <summary>
		/// �� ���� �̸��� �� �̸��� �Ҵ�
		/// </summary>
		[ContextMenu("SceneAssetToSceneName")]
		public void SceneAssetToSceneName()
		{
			sceneName = sceneAsset.name;
		}

#endif

		public void SetSceneName(string _name)
        {
            sceneName = _name;
        }

        public void Start()
        {
            if (lodMaker is null)
            {
                lodMaker = GetComponent<LODMaker>();
                if (lodMaker != null)
                {
                    lodMaker.Init(this);
                }
            }

            if (SceneName != null)
            {
                SceneData sceneData = SceneDataManager.Instance.GetSceneData(SceneName);
                sceneData.SetLoadSceneObjNLodMaker(this, LODMaker);
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
				Handles.Label(new Vector3(transform.position.x, transform.position.y + 40, transform.position.z), $"{SceneDataManager.Instance.GetSceneData(SceneName).ObjectDataList.objectDataList.Count}", guiSkin.GetStyle("CountStyle"));
			}
#endif
        }

        /// <summary>
        /// ���� Ȱ��ȭ�� �������� üũ�Ѵ�
        /// </summary>
        /// <returns></returns>
        public bool IsActiveScene()
        {
            return CheckCurrentlyActhive(SceneName);
        }

        /// <summary>
        /// ���� ���� �ҷ��´�
        /// </summary>
        public void LoadScene()
        {
            AddressablesManager.Instance.LoadSceneAsync(SceneName, LoadSceneMode.Additive, LoadSceneObject);
        }

        /// <summary>
        /// ���� ���� Ȱ��ȭ�Ǿ� �־� �ִٸ� ��Ȱ��ȭ�Ѵ�
        /// </summary>
        public void UnLoadSceneNoneCheck()
        {
            EventQueueManager.Instance.AddAction(SceneDataManager.Instance.GetSceneData(SceneName).UnLoad);
            EventQueueManager.Instance.AddAction(LODMaker.UnLoad);

            AddressablesManager.Instance.UnLoadSceneAsync(SceneName);
        }

        private void LoadSceneObject(AsyncOperationHandle<SceneInstance> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                EventQueueManager.Instance.AddAction(SceneDataManager.Instance.GetSceneData(SceneName).Load);
                EventQueueManager.Instance.AddAction(LODMaker.Load);
            }
        }

        #region DebugCode

        /// <summary>
        /// �߾������� ������Ʈ �����Ϳ� �߰���
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