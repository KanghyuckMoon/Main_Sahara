using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Streaming.StreamingUtill;
using Utill.Pattern;
using Utill.Addressable;
using GameManager;
using Unity.Jobs;
using UnityEngine.SceneManagement;

namespace Streaming
{
	public delegate void StreamingEventTransmit(string _sender, string _recipient, object _obj);
	public class StreamingManager : MonoSingleton<StreamingManager>, Observer
	{
		public class BoolStruct
        {
			public bool value;
        }

		public struct SceneStreamingJob : IJob
		{
			public int originChunkCoordX;
			public int originChunkCoordY;
			public int originChunkCoordZ;

			public void Execute()
			{
				foreach (var _sceneObj in StreamingManager.Instance.chunkDictionary)
				{
					Vector3 _currentPos = new Vector3(originChunkCoordX, originChunkCoordY, originChunkCoordZ);
					if (Vector3.Distance(_currentPos, _sceneObj.Key) < StreamingManager.chunksVisibleInViewDst)
					{
						StreamingManager.Instance.LoadSubScene(_sceneObj.Key);	
					}
					else
					{
						StreamingManager.Instance.UnLoadSubSceneNoneCheck(_sceneObj.Key);	
					}
				}
			}
		}

		public bool IsSetting
		{
			get
			{
				return isSceneSetting;
			}
			set
			{
				isSceneSetting = value;
			}
		}
		private Transform Viewer
		{
			get
			{
				if (useDebugViewer)
				{
					return debugViewer;
				}

				viewer ??= GameObject.FindGameObjectWithTag("Player")?.transform;

				return viewer;
			}
		}

		private SubSceneReference SubSceneReference
		{
			get
			{
				subSceneReference ??= FindObjectOfType<SubSceneReference>();
				return subSceneReference;
			}
		}
		public StreamingEventTransmit StreamingEventTransmit
		{
			get
			{
				return streamingEventTransmit;
			}
			set
			{
				streamingEventTransmit = value;
			}
		}

		private StreamingEventTransmit streamingEventTransmit = default;

		//[SerializeField]
		private Transform viewer = null;

		[SerializeField]
		private bool useDebugViewer = false;

		[SerializeField]
		private Transform debugViewer = null;

		private SubSceneReference subSceneReference;
		public Dictionary<Vector3, SubSceneObj> chunkDictionary = new Dictionary<Vector3, SubSceneObj>(); //씬 데이터 딕셔너리
		public Dictionary<string, LODMaker> lodDictionary = new Dictionary<string, LODMaker>(); //LOD 데이터 딕셔너리
		public Dictionary<string, BoolStruct> sceneActiveCheckDic = new Dictionary<string, BoolStruct>();

		private Vector3 viewerPosition;
		private int originChunkCoordX;
		private int originChunkCoordY;
		private int originChunkCoordZ;

		public const int chunkSize = 100;
		public const int chunksVisibleInViewDst = 3;
		private const int LODDst = 1;
		private const int interval = 3;
		private Vector3 defaultPosition = new Vector3(0,4050,0);
		private bool isSceneSetting = false;


		public void ReceiveEvent(string _sender, object _obj)
		{

		}

		public void Receive()
		{
			if (GamePlayerManager.Instance.IsPlaying)
			{
				if (!useDebugViewer)
				{
					viewer = GameObject.FindGameObjectWithTag("Player")?.transform;
				}
			}
			else
			{
				isSceneSetting = false;
				originChunkCoordX = 0;
				originChunkCoordY = 0;
				originChunkCoordZ = 0;
				viewerPosition = defaultPosition;
				subSceneReference = null;
				chunkDictionary.Clear();
				lodDictionary.Clear();

			}
		}

		public IEnumerator LoadReadyScene()
		{
			while(true)
			{
				if (SubSceneReference is not null)
				{
					break;
				}
				yield return null;
			}
			originChunkCoordX = 0;
			originChunkCoordY = 0;
			originChunkCoordZ = 0;
			viewerPosition = defaultPosition;
			InitSubScene();
			InitChunk();
			while (AddressablesManager.Instance.loadMessageQueue.Count > 0)
            {
				yield return null;
            }
			isSceneSetting = true;
		}

		public void Start()
		{
			//base.Awake();
			GamePlayerManager.Instance.AddObserver(this);
			StartCoroutine(AddressablesManager.Instance.LoadSceneQueue());
			StartCoroutine(AddressablesManager.Instance.UnLoadSceneAsyncQueue());
		}

		private void Update()
		{
			if (!GamePlayerManager.Instance.IsPlaying)
			{
				return;
			}

			if (Time.frameCount % interval == 0)
			{
				if (Viewer is null)
				{
					viewerPosition = defaultPosition;
				}
				else
				{
					var _position = viewer.position;
					viewerPosition = new Vector3(_position.x, _position.y, _position.z);
				}
				CheckOutChunk();
			}
		}

		/// <summary>
		/// 서브씬 데이터들 추가
		/// </summary>
		private void InitSubScene()
		{
			chunkDictionary.Clear();
			lodDictionary.Clear();
			SubSceneReference.Init();
			foreach (SubSceneObj obj in SubSceneReference.SubSceneArray)
			{
				Vector3 _vector = StringToVector3(obj.SceneName);
				chunkDictionary.Add(_vector, obj);
				lodDictionary.Add(obj.SceneName, obj.GetComponent<LODMaker>());
			}
		}

		/// <summary>
		/// 뷰어가 청크를 이동했는지 체크한다
		/// </summary>
		private void CheckOutChunk()
		{
			int _currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
			int _currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);
			int _currentChunkCoordZ = Mathf.RoundToInt(viewerPosition.z / chunkSize);

			if (originChunkCoordX != _currentChunkCoordX || originChunkCoordY != _currentChunkCoordY || originChunkCoordZ != _currentChunkCoordZ) //청크를 벗어났는지 이동 체크 조건
			{
				originChunkCoordX = _currentChunkCoordX;
				originChunkCoordY = _currentChunkCoordY;
				originChunkCoordZ = _currentChunkCoordZ;

				Physics.RebuildBroadphaseRegions(new Bounds(new Vector3(_currentChunkCoordX, _currentChunkCoordY, _currentChunkCoordZ) * 100,Vector3.one * 200), 5);
				StartCoroutine(UpdateChunk());
				streamingEventTransmit.Invoke("StreamingManager", "SaveManager", null);
			}
		}

		private void InitChunk()
		{
			foreach(var _obj in chunkDictionary)
			{
				LoadSubScene(_obj.Key);
			}
		}

		private IEnumerator UpdateChunk()
		{
			SetSceneDic();

			SceneStreamingJob _sceneStreamingJob = new SceneStreamingJob()
			{
				originChunkCoordX = this.originChunkCoordX,
				originChunkCoordY = this.originChunkCoordY,
				originChunkCoordZ = this.originChunkCoordZ
			};
			JobHandle _jobHandle = _sceneStreamingJob.Schedule();
			
			yield return null;

		}

		/// <summary>
		/// 서브씬 활성화
		/// </summary>
		/// <param name="_viewedChunkCoord"></param>
		private void LoadSubScene(Vector3 _viewedChunkCoord)
		{
			if (!CheckCurrentlyActhive(chunkDictionary[_viewedChunkCoord].SceneName))
			{
				chunkDictionary[_viewedChunkCoord].LoadScene();
			}
		}
		
		
		/// <summary>
		/// 서브씬 활성화
		/// </summary>
		/// <param name="_viewedChunkCoord"></param>
		private void UnLoadSubSceneNoneCheck(Vector3 _viewedChunkCoord)
		{
			if (CheckCurrentlyActhive(chunkDictionary[_viewedChunkCoord].SceneName))
			{
				chunkDictionary[_viewedChunkCoord].UnLoadSceneNoneCheck();
			}
		}

		/// <summary>
		/// Check Currently Acthive Scene from Build Index
		/// </summary>
		/// <param name="buildIndex"></param>
		/// <returns></returns>
		public bool CheckCurrentlyActhive(string _sceneName)
		{
			if (sceneActiveCheckDic.TryGetValue(_sceneName, out var _bool))
            {
				return _bool.value;
			}
			return	false;
		}

		public void SetSceneDic()
		{
			foreach(var _obj in sceneActiveCheckDic)
			{
				_obj.Value.value = false;
			}
            for (int i = 0; i < SceneManager.sceneCount; ++i)
			{
				string _name = SceneManager.GetSceneAt(i).name;
				if (sceneActiveCheckDic.TryGetValue(_name, out var _bool))
                {
					sceneActiveCheckDic[_name].value = true;
				}
				else
                {
					BoolStruct _boolStruct = new BoolStruct();
					_boolStruct.value = true;
					sceneActiveCheckDic.Add(SceneManager.GetSceneAt(i).name, _boolStruct);
                }
			}
		}

		public LODMaker GetLODMaker(string _sceneName)
        {
			if (lodDictionary.TryGetValue(_sceneName, out var _lodmaker))
            {
				return _lodmaker;
            }
			return null;
        }
	}

}
