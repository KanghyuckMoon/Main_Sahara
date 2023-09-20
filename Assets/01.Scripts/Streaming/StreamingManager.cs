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
		public bool IsCurrentSceneSetting
		{
			get
			{
				return isCurrentSceneSetting;
			}
			set
			{
				isCurrentSceneSetting = value;
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

				viewer ??= PlayerObj.Player?.transform;

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

		[SerializeField] private GameObject loadingCanvas;
		
		private Transform viewer = null;

		[SerializeField]
		private bool useDebugViewer = false;

		[SerializeField]
		private Transform debugViewer = null;

		private SubSceneReference subSceneReference;
		public Dictionary<Vector3, SubSceneObj> chunkDictionary = new Dictionary<Vector3, SubSceneObj>(); //씬 데이터 딕셔너리
		public Dictionary<string, LODMaker> lodDictionary = new Dictionary<string, LODMaker>(); //LOD 데이터 딕셔너리
		
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
		private bool isCurrentSceneSetting = false;


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
				originChunkCoordY = 40;
				originChunkCoordZ = 0;
				viewerPosition = defaultPosition;
				subSceneReference = null;
				chunkDictionary.Clear();
				lodDictionary.Clear();

			}
		}

		public void LoadReadyScene()
		{
			isSceneSetting = false;
			viewer = PlayerObj.Player.transform;
			viewerPosition = viewer.position;
			originChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
			originChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);
			originChunkCoordY = (int)Mathf.Clamp(originChunkCoordY, 39, 40);
			originChunkCoordZ = Mathf.RoundToInt(viewerPosition.z / chunkSize);
			//viewerPosition = defaultPosition;
			InitSubScene();
			//InitChunkLegacy();
			StartCoroutine(InitChunk());
			//StartCoroutine(InitChunk());
			//InitChunk();
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
			if (!isSceneSetting)
			{
				return;
			}
			
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

				originChunkCoordY = (int)Mathf.Clamp(originChunkCoordY, 39, 40);

				StartCoroutine(UpdateChunk());
			}
		}

		
		private IEnumerator InitChunk()
		{
			Vector3 _pos = new Vector3(originChunkCoordX, originChunkCoordY, originChunkCoordZ);
			var currentScene = chunkDictionary[_pos];
			LoadSubScene(_pos);
			while (true)
			{
            yield return new WaitForSeconds(1f);
				if(currentScene.IsActiveScene())
				{
					break;
				}
				else
				{
					yield return null;
				}
			}

			isCurrentSceneSetting = true;
			
			loadingCanvas.SetActive(true);
			
			foreach(var _obj in chunkDictionary)
			{
            yield return new WaitForSeconds(0.1f);
				LoadSubScene(_obj.Key);
				Debug.Log("Scene Load : " + _obj.Key);
				
				while(true)
				{
				    if(_obj.Value.IsActiveScene())
				    {
						if (Vector3.Distance(_pos, _obj.Key) > StreamingManager.chunksVisibleInViewDst)
						{
							_obj.Value.UnLoadSceneNoneCheck();
							Debug.Log("Scene UnLoad : " + _obj.Key);
						}
				    	break;
				    }
				    else
				    {
				    	yield return null;
				    }
				}
			}
			
			isSceneSetting = true;
			GameManager.GamePlayerManager.Instance.IsPlaying = true;
			
			loadingCanvas.SetActive(false);
			
			StartCoroutine(UpdateChunk());
		}

		private IEnumerator UpdateChunk()
		{
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
	}

}
