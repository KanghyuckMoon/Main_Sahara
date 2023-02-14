using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Streaming.StreamingUtill;
using Utill.Pattern;
using Utill.Addressable;
using GameManager;

namespace Streaming
{
	public class StreamingManager : MonoSingleton<StreamingManager>, Observer
	{
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
		//[SerializeField]
		private Transform viewer = null;

		[SerializeField]
		private bool useDebugViewer = false;

		[SerializeField]
		private Transform debugViewer = null;

		private SubSceneReference subSceneReference;
		private Dictionary<Vector3, SubSceneObj> chunkDictionary = new Dictionary<Vector3, SubSceneObj>(); //씬 데이터 딕셔너리
		private List<Vector3> chunksCurrentVisibleList = new List<Vector3>(); //현재 활성화되어야 씬 목록
		private List<Vector3> chunksPreviousVisibleList = new List<Vector3>(); //이전에 활성화된 씬 목록

		private Vector3 viewerPosition;
		private int originChunkCoordX;
		private int originChunkCoordY;
		private int originChunkCoordZ;

		private const int chunkSize = 100;
		private const int chunksVisibleInViewDst = 2;
		private const int LODDst = 1;
		private const int interval = 3;
		private Vector3 defaultPosition = new Vector3(0,4050,0);
		public void Receive()
		{
			if (GamePlayerManager.Instance.IsPlaying)
			{
				originChunkCoordX = 0;
				originChunkCoordY = 0;
				originChunkCoordZ = 0;
				viewerPosition = defaultPosition;
				if (!useDebugViewer)
				{
					viewer = GameObject.FindGameObjectWithTag("Player")?.transform;
				}
				InitSubScene();
				InitChunk();
				UnLoadVisibleChunk();
			}
			else
			{
				originChunkCoordX = 0;
				originChunkCoordY = 0;
				originChunkCoordZ = 0;
				viewerPosition = defaultPosition;
				subSceneReference = null;
				chunkDictionary.Clear();
				chunksCurrentVisibleList.Clear();
				chunksPreviousVisibleList.Clear();

			}
		}

		public override void Awake()
		{
			base.Awake();
			GamePlayerManager.Instance.AddObserver(this);
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
			chunksPreviousVisibleList.Clear();
			SubSceneReference.Init();
			foreach (SubSceneObj obj in SubSceneReference.SubSceneArray)
			{
				chunkDictionary.Add(StringToVector3(obj.SceneName), obj);
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

				UpdateChunk();
			}
		}

		private void InitChunk()
		{
			chunksCurrentVisibleList.Clear();
			for (int _zOffset = -chunksVisibleInViewDst; _zOffset <= chunksVisibleInViewDst; _zOffset++)
			{
				for (int _yOffset = -chunksVisibleInViewDst; _yOffset <= chunksVisibleInViewDst; _yOffset++)
				{
					for (int _xOffset = -chunksVisibleInViewDst; _xOffset <= chunksVisibleInViewDst; _xOffset++)
					{
						Vector3 viewedChunkCoord = new Vector3(originChunkCoordX + _xOffset, originChunkCoordY + _yOffset, originChunkCoordZ + _zOffset);

						if (chunkDictionary.ContainsKey(viewedChunkCoord))
						{
							LoadSubScene(viewedChunkCoord);
						}
					}
				}
			}
			chunksPreviousVisibleList = chunksCurrentVisibleList.ToList();
		}

		private void UpdateChunk()
		{
			chunksCurrentVisibleList.Clear();
			for (int _zOffset = -chunksVisibleInViewDst; _zOffset <= chunksVisibleInViewDst; _zOffset++)
			{
				for(int _yOffset = -chunksVisibleInViewDst; _yOffset <= chunksVisibleInViewDst; _yOffset++)
				{
					for (int _xOffset = -chunksVisibleInViewDst; _xOffset <= chunksVisibleInViewDst; _xOffset++)
					{
						Vector3 _viewedChunkCoord = new Vector3(originChunkCoordX + _xOffset, originChunkCoordY + _yOffset, originChunkCoordZ + _zOffset);

						if (chunkDictionary.ContainsKey(_viewedChunkCoord))
						{
							LoadSubScene(_viewedChunkCoord);
						}
					}
				}
			}
			UnLoadVisibleChunk();
			chunksPreviousVisibleList = chunksCurrentVisibleList.ToList();
		}
		private void UnLoadVisibleChunk()
		{
			List<Vector3> _unloadSceneList = chunksPreviousVisibleList.Except(chunksCurrentVisibleList).ToList();

			while(_unloadSceneList.Count > 0)
			{
				UnLoadSubScene(_unloadSceneList[0]);
				_unloadSceneList.RemoveAt(0);
			}

			//혹시 모를 언로드
			foreach(var _chunk in chunkDictionary)
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
			if (!chunkDictionary[_viewedChunkCoord].IsActiveScene())
			{
				chunkDictionary[_viewedChunkCoord].LoadScene();
			}
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

	}

}
