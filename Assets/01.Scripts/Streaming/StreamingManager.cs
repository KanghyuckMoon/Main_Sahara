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
using System.Threading.Tasks;
using System;

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
				for (int _zOffset = -chunksVisibleInViewDst; _zOffset <= chunksVisibleInViewDst; _zOffset++)
				{
					for (int _yOffset = -chunksVisibleInViewDst; _yOffset <= chunksVisibleInViewDst; _yOffset++)
					{
						for (int _xOffset = -chunksVisibleInViewDst; _xOffset <= chunksVisibleInViewDst; _xOffset++)
						{
							Vector3 _viewedChunkCoord = new Vector3(originChunkCoordX + _xOffset, originChunkCoordY + _yOffset, originChunkCoordZ + _zOffset);

							if (StreamingManager.Instance.chunkDictionary.ContainsKey(_viewedChunkCoord))
							{
								//씬 데이터가 있어야함
								if (!StreamingManager.Instance.chunkDictionary.TryGetValue(_viewedChunkCoord, out var a))
								{
									continue;
								}

								//씬이 비활성화된 상태여야함
								if (!StreamingManager.Instance.chunksCurrentVisibleList.Contains(_viewedChunkCoord))
								{
									StreamingManager.Instance.chunksCurrentVisibleList.Add(_viewedChunkCoord);
								}
								StreamingManager.Instance.LoadSubScene(_viewedChunkCoord);
							}
						}
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
		public List<Vector3> chunksCurrentVisibleList = new List<Vector3>(); //현재 활성화되어야 씬 목록
		public List<Vector3> chunksPreviousVisibleList = new List<Vector3>(); //이전에 활성화된 씬 목록
		public Dictionary<string, BoolStruct> sceneActiveCheckDic = new Dictionary<string, BoolStruct>();

		private Vector3 viewerPosition;
		private int originChunkCoordX;
		private int originChunkCoordY;
		private int originChunkCoordZ;

		private const int chunkSize = 100;
		private const int chunksVisibleInViewDst = 2;
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
				//isSceneSetting = false;
				//originChunkCoordX = 0;
				//originChunkCoordY = 0;
				//originChunkCoordZ = 0;
				//viewerPosition = defaultPosition;
				if (!useDebugViewer)
				{
					viewer = GameObject.FindGameObjectWithTag("Player")?.transform;
				}
				//InitSubScene();
				//InitChunk();
				//UnLoadVisibleChunk();
				//isSceneSetting = true;
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
				chunksCurrentVisibleList.Clear();
				chunksPreviousVisibleList.Clear();

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
			UnLoadVisibleChunk();
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
					viewerPosition = new Vector3(viewer.position.x, viewer.position.y, viewer.position.z);
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
			chunksPreviousVisibleList.Clear();
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

				StartCoroutine(UpdateChunk());
				//StartCoroutine(UpdateChunk());
				//streamingEventTransmit.Invoke("StreamingManager", "SaveManager", null);
			}
		}

		private void InitChunk()
		{
			chunksCurrentVisibleList.Clear();

			foreach(var _obj in chunkDictionary)
			{
				LoadSubScene(_obj.Key);
			}

			chunksPreviousVisibleList = chunksCurrentVisibleList.ToList();
		}

		private IEnumerator UpdateChunk()
		{
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			SetSceneDic();

			chunksCurrentVisibleList.Clear();
			SceneStreamingJob _sceneStreamingJob = new SceneStreamingJob()
			{
				originChunkCoordX = this.originChunkCoordX,
				originChunkCoordY = this.originChunkCoordY,
				originChunkCoordZ = this.originChunkCoordZ
			};
			JobHandle _jobHandle = _sceneStreamingJob.Schedule();
			
			while (!_jobHandle.IsCompleted)
			{
				yield return null;
			}
			_jobHandle.Complete();

			sw.Stop();
			Debug.Log("Streaming Set: " + sw.ElapsedMilliseconds.ToString() + "ms");

			UnLoadVisibleChunk();
			chunksPreviousVisibleList = chunksCurrentVisibleList.ToList();


		}
		private void UnLoadVisibleChunk()
		{
			System.Diagnostics.Stopwatch sw2 = new System.Diagnostics.Stopwatch();
			sw2.Start();

			List<Vector3> _unloadSceneList = chunksPreviousVisibleList.Except(chunksCurrentVisibleList).ToList();

			sw2.Stop();
			Debug.Log("Streaming UnLoad1: " + sw2.ElapsedMilliseconds.ToString() + "ms");

			System.Diagnostics.Stopwatch sw3 = new System.Diagnostics.Stopwatch();
			sw3.Start();

			while (_unloadSceneList.Count > 0)
			{
				UnLoadSubScene(_unloadSceneList[0]);
				_unloadSceneList.RemoveAt(0);
			}

			sw3.Stop();
			Debug.Log("Streaming UnLoad2: " + sw3.ElapsedMilliseconds.ToString() + "ms");

			//혹시 모를 언로드
			foreach (var _chunk in chunkDictionary)
			{
				if (_chunk.Value.IsActiveScene() && !chunksCurrentVisibleList.Contains(_chunk.Key))
				{
					UnLoadSubScene(_chunk.Key);
				}
			}
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
		private void LoadSubSceneNoneCheck(Vector3 _viewedChunkCoord)
		{
			//씬 데이터가 있어야함
			if(!chunkDictionary.TryGetValue(_viewedChunkCoord, out var a))
			{
				return;
			}

			//씬이 비활성화된 상태여야함
			if (!chunksCurrentVisibleList.Contains(_viewedChunkCoord))
			{
				chunksCurrentVisibleList.Add(_viewedChunkCoord);
			}
			chunkDictionary[_viewedChunkCoord].LoadSceneNoneCheck();
		}

		/// <summary>
		/// 서브씬 비활성화
		/// </summary>
		/// <param name="_viewedChunkCoord"></param>
		private void UnLoadSubScene(Vector3 _viewedChunkCoord)
		{
			//씬 데이터가 있어야함
			if (!chunkDictionary.TryGetValue(_viewedChunkCoord, out var a))
			{
				return;
			}

			//씬이 활성화된 상태여야함
			if (chunksCurrentVisibleList.Contains(_viewedChunkCoord))
			{
				chunksCurrentVisibleList.Remove(_viewedChunkCoord);
			}
			chunkDictionary[_viewedChunkCoord].UnLoadScene();
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
