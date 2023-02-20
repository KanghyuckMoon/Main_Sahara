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
		private Dictionary<Vector3, SubSceneObj> chunkDictionary = new Dictionary<Vector3, SubSceneObj>(); //�� ������ ��ųʸ�
		private List<Vector3> chunksCurrentVisibleList = new List<Vector3>(); //���� Ȱ��ȭ�Ǿ�� �� ���
		private List<Vector3> chunksPreviousVisibleList = new List<Vector3>(); //������ Ȱ��ȭ�� �� ���

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
		/// ����� �����͵� �߰�
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
		/// �� ûũ�� �̵��ߴ��� üũ�Ѵ�
		/// </summary>
		private void CheckOutChunk()
		{
			int _currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
			int _currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);
			int _currentChunkCoordZ = Mathf.RoundToInt(viewerPosition.z / chunkSize);

			if (originChunkCoordX != _currentChunkCoordX || originChunkCoordY != _currentChunkCoordY || originChunkCoordZ != _currentChunkCoordZ) //ûũ�� ������� �̵� üũ ����
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

			//Ȥ�� �� ��ε�
			foreach(var _chunk in chunkDictionary)
			{
				if (_chunk.Value.IsActiveScene() && !chunksCurrentVisibleList.Contains(_chunk.Key))
				{
					UnLoadSubScene(_chunk.Key);
				}
			}
		}

		/// <summary>
		/// ����� Ȱ��ȭ
		/// </summary>
		/// <param name="_viewedChunkCoord"></param>
		private void LoadSubScene(Vector3 _viewedChunkCoord)
		{
			//�� �����Ͱ� �־����
			if(!chunkDictionary.TryGetValue(_viewedChunkCoord, out var a))
			{
				return;
			}

			//���� ��Ȱ��ȭ�� ���¿�����
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
		/// ����� ��Ȱ��ȭ
		/// </summary>
		/// <param name="_viewedChunkCoord"></param>
		private void UnLoadSubScene(Vector3 _viewedChunkCoord)
		{
			//�� �����Ͱ� �־����
			if (!chunkDictionary.TryGetValue(_viewedChunkCoord, out var a))
			{
				return;
			}

			//���� Ȱ��ȭ�� ���¿�����
			if (chunksCurrentVisibleList.Contains(_viewedChunkCoord))
			{
				chunksCurrentVisibleList.Remove(_viewedChunkCoord);
			}
			chunkDictionary[_viewedChunkCoord].UnLoadScene();
		}

	}

}
