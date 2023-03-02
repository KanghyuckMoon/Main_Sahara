using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Streaming.StreamingUtill;
using UnityEngine.SceneManagement;
using Pool;
using Data;

namespace Streaming
{
	public class ObjectSceneChecker : IObjectClass
	{
		public ObjectData ObjectData
		{
			get
			{
				return objectData;
			}
			set
			{
				objectData = value;
			}
		}
		public SceneData TargetSceneData
		{
			get
			{
				return targetSceneData;
			}
			set
			{
				targetSceneData = value;
			}
		}
		public ObjectClassCycle ObjectClassCycle
		{
			get
			{
				return objectClassCycle;
			}
			set
			{
				objectClassCycle = value;
			}
		}

		public ObjectDataSO ObjectDataSO
		{
			get
			{
				return objectDataSO;
			}
			set
			{
				objectDataSO = value;
			}
		}

		[SerializeField]
		private ObjectDataSO objectDataSO = null;

		private ObjectData objectData = null;

		private SceneData targetSceneData = null;

		private ObjectClassCycle objectClassCycle = null;

		private int originChunkCoordX;
		private int originChunkCoordY;
		private int originChunkCoordZ;

		private const int chunkSize = 100;
		
		/// <summary>
		/// 자신의 현재 위치에 따라 목표하는 씬 데이터를 변경함
		/// </summary>
		public void UpdateSceneData()
		{
			SceneData _sceneData = SceneDataManager.Instance.GetSceneData(PositionToSceneName());

			if (_sceneData == targetSceneData || _sceneData is null)
			{
				return;
			}

			targetSceneData.RemoveObjectData(ObjectData);
			targetSceneData.RemoveObjectChecker(this);
			targetSceneData = _sceneData;
			targetSceneData.AddObjectData(objectData);

			objectClassCycle.TargetObject.name = $"{objectData.address} {targetSceneData.SceneName}";

			if (!targetSceneData.IsLoad)
			{
				UnUse();
				ObjectPoolManager.Instance.RegisterObject(objectData.address, objectClassCycle.gameObject);
				ClassPoolManager.Instance.RegisterObject<ObjectSceneChecker>("ObjectSceneChecker", this);
			}
			else
			{
				SceneManager.MoveGameObjectToScene(ObjectClassCycle.TargetObject, SceneManager.GetSceneByName(targetSceneData.SceneName));
				targetSceneData.AddObjectChecker(this);
			}
		}

		/// <summary>
		/// 현재 오브젝트데이터를 저장함
		/// </summary>
		public void SaveObjectData()
		{
			if (objectData is null)
			{
				return;
			}

			objectData.position = objectClassCycle.transform.position;
			objectData.rotation = objectClassCycle.transform.rotation;
			objectData.scale = objectClassCycle.transform.localScale;
		}

		/// <summary>
		/// 현재 오브젝트 데이터의 상태를 사용중으로 돌림
		/// </summary>
		public void Use()
		{
			objectData.isUse = true;
		}

		/// <summary>
		/// 현재 오브젝트 데이터의 상태를 사용하지 않음으로 돌림
		/// </summary>
		public void UnUse()
		{
			objectData.isUse = false;
		}

		/// <summary>
		/// 위치에 따라 씬 이름으로 바꿈
		/// </summary>
		/// <returns></returns>
		private string PositionToSceneName()
		{
			return NameFromPosition(originChunkCoordX, originChunkCoordY, originChunkCoordZ);
		}

		public void Start()
		{
			if (objectDataSO != null)
			{
				objectData = new ObjectData();
				objectData.CopyObjectDataSO(objectDataSO);
				objectData.key = objectClassCycle.TargetObject.GetInstanceID();
				if (objectData.isMonster)
				{
					var _statData = objectClassCycle.TargetObject.GetComponentInParent<StatData>();
					objectData.SetObserble(_statData);
				}
				objectData.isUse = true;
			}
		}

		public void Update()
		{
			if (objectData is null)
			{
				return;
			}


			int _currentChunkCoordX = Mathf.RoundToInt(objectClassCycle.transform.position.x / chunkSize);
			int _currentChunkCoordY = Mathf.RoundToInt(objectClassCycle.transform.position.y / chunkSize);
			int _currentChunkCoordZ = Mathf.RoundToInt(objectClassCycle.transform.position.z / chunkSize);

			if (originChunkCoordX != _currentChunkCoordX || originChunkCoordY != _currentChunkCoordY || originChunkCoordZ != _currentChunkCoordZ) //청크를 벗어났는지 이동 체크 조건
			{
				originChunkCoordX = _currentChunkCoordX;
				originChunkCoordY = _currentChunkCoordY;
				originChunkCoordZ = _currentChunkCoordZ;

				if (targetSceneData is null)
				{
					SceneData sceneData = SceneDataManager.Instance.GetSceneData(PositionToSceneName());
					if (sceneData is null)
					{
						return;
					}
					targetSceneData = sceneData;
					targetSceneData.AddObjectData(objectData);
					targetSceneData.AddObjectChecker(this);
				}

				UpdateSceneData();
			}

			SaveObjectData();
		}

	}
}
