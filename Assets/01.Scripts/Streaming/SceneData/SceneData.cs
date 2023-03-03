using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using UnityEngine.ResourceManagement.AsyncOperations;
using Pool;
using Data;

namespace Streaming
{
	public partial class SceneData
	{
		public string SceneName
		{
			get
			{
				return sceneName;
			}
			set
			{
				sceneName = value;
			}
		}
		
		public ObjectDataList ObjectDataList
		{
			get
			{
				return objectDataList;
			}
			set
			{
				objectDataList = value;
			}
		}
		public List<ObjectSceneChecker> ObjectCheckerList
		{
			get
			{
				return objectCheckerList;
			}
			set
			{
				objectCheckerList = value;
			}
		}
		public bool IsLoad
		{
			get
			{
				return isLoad;
			}
		}

		private string sceneName = "";
		private bool isLoad = false;

		public ObjectDataList objectDataList;
		private List<ObjectSceneChecker> objectCheckerList;

		private SubSceneObj loadSceneObj = null;
		private LODMaker lodMaker = null;


		public SceneData(string _sceneName)
		{
			this.sceneName = _sceneName;
			objectDataList = new ObjectDataList();
			objectCheckerList = new List<ObjectSceneChecker>();

			InitScene(_sceneName);
		}

		//public void SetLoad()
		//{
		//
		//}

		/// <summary>
		/// ���� ���� �ʱ� �����͸� �ٸ��� ó��
		/// </summary>
		/// <param name="_sceneName"></param>
		partial void InitScene(string _sceneName);

		/// <summary>
		/// �ڽ��� ����ϴ� SubSceneObj�� LODMaker�� ����Ѵ�
		/// </summary>
		/// <param name="_loadSceneObj"></param>
		/// <param name="_lodMaker"></param>
		public void SetLoadSceneObjNLodMaker(SubSceneObj _loadSceneObj, LODMaker _lodMaker)
		{
			this.loadSceneObj = _loadSceneObj;
			this.lodMaker = _lodMaker;
			InitLodObjects();
		}

		/// <summary>
		/// ObjectData�� �߰��Ѵ�
		/// </summary>
		/// <param name="_objectData"></param>
		public void AddObjectData(ObjectData _objectData)
		{
			objectDataList.objectDataList.Add(_objectData);

			if (_objectData.lodType == LODType.On)
			{
				lodMaker?.AddLOD(_objectData);
			}

			if (IsLoad && !_objectData.isUse)
			{
				LoadObjectData(_objectData);
			}
		}

		public void AddOnlyLODObjectData(ObjectData _objectData)
		{
			if (_objectData.lodType == LODType.On)
			{
				lodMaker?.AddLOD(_objectData);
			}
		}

		/// <summary>
		/// ObjectData�� �����Ѵ�
		/// </summary>
		/// <param name="_objectData"></param>
		public void RemoveObjectData(ObjectData _objectData)
		{
			objectDataList.objectDataList.Remove(_objectData);

			if (_objectData.lodType == LODType.On)
			{
				lodMaker?.RemoveLOD(_objectData.key);
			}
		}

		/// <summary>
		/// ������Ʈ üĿ�� �߰��Ѵ�
		/// </summary>
		/// <param name="_obj"></param>
		public void AddObjectChecker(ObjectSceneChecker _obj)
		{
			objectCheckerList.Add(_obj);
		}

		/// <summary>
		/// ������Ʈ üĿ�� �����Ѵ�
		/// </summary>
		/// <param name="_obj"></param>
		public void RemoveObjectChecker(ObjectSceneChecker _obj)
		{
			objectCheckerList.Remove(_obj);
		}

		/// <summary>
		/// �� ������Ʈ���� �ҷ��´�
		/// </summary>
		public void Load()
		{
			isLoad = true;
			foreach (ObjectData _objectData in objectDataList.objectDataList)
			{
				if (_objectData.isUse)
				{
					continue;
				}
				LoadObjectData(_objectData);
				//���� Ǯ������ ��ü
			}
		}

		/// <summary>
		/// �� ������Ʈ���� �����Ѵ�
		/// </summary>
		public void UnLoad()
		{
			isLoad = false;
			while (objectCheckerList.Count > 0)
			{
				ObjectSceneChecker _obj = objectCheckerList[0];
				if(_obj is not null)
				{
					if (_obj.ObjectData.isUse)
					{
						_obj.UnUse();
						ObjectClassCycle _objectClassCycle = _obj?.ObjectClassCycle;
						if(_objectClassCycle is null)
						{
							objectCheckerList.RemoveAt(0);
							continue;
						}
						else if (_objectClassCycle is not null && _objectClassCycle?.gameObject is not null)
						{
							_obj.ObjectClassCycle.TargetObject.SetActive(false);
							_obj.ObjectClassCycle.RemoveObjectClass(_obj);
							ObjectPoolManager.Instance.RegisterObject(_obj.ObjectData.address, _obj.ObjectClassCycle.TargetObject);
							ClassPoolManager.Instance.RegisterObject("ObjectSceneChecker", _obj );
						}
					}
				}

				objectCheckerList.RemoveAt(0);
			}
		}

		public void SaveUnLoad()
		{
			while (objectCheckerList.Count > 0)
			{
				ObjectSceneChecker _obj = objectCheckerList[0];
				if (_obj is not null)
				{
					if (_obj.ObjectData.isUse)
					{
						_obj.UnUse();
						ObjectClassCycle _objectClassCycle = _obj?.ObjectClassCycle;
						if (_objectClassCycle is null)
						{
							objectCheckerList.RemoveAt(0);
							continue;
						}
						else if (_objectClassCycle is not null && _objectClassCycle?.gameObject is not null)
						{
							_obj.ObjectClassCycle.TargetObject.SetActive(false);
							_obj.ObjectClassCycle.RemoveObjectClass(_obj);
							ObjectPoolManager.Instance.RegisterObject(_obj.ObjectData.address, _obj.ObjectClassCycle.TargetObject);
							ClassPoolManager.Instance.RegisterObject("ObjectSceneChecker", _obj);
						}
					}
				}

				objectCheckerList.RemoveAt(0);
			}
		}
		public void SaveLoad()
		{
			if(IsLoad)
			{
				foreach (ObjectData _objectData in objectDataList.objectDataList)
				{
					//if (_objectData.isUse)
					//{
					//	continue;
					//}
					LoadObjectData(_objectData);
					//���� Ǯ������ ��ü
				}
			}
		}


		private void LoadObjectData(ObjectData _objectData)
		{
			if (_objectData.address is not null)
			{
				ObjectPoolManager.Instance.GetObjectAsyncParameter<ObjectData>(_objectData.address, LoadObject, _objectData);
			}
		}

		private void LoadObject(GameObject gameObject, ObjectData _objectData)
		{
			gameObject.transform.SetPositionAndRotation(_objectData.position, _objectData.rotation);
			gameObject.transform.localScale = _objectData.scale;
			gameObject.name = $"{_objectData.address} {SceneName}";

			ObjectClassCycle _objectClassCycle = gameObject.GetComponentInChildren<ObjectClassCycle>();
			ObjectSceneChecker _objectSceneChecker = ClassPoolManager.Instance.GetClass<ObjectSceneChecker>("ObjectSceneChecker");
			if (_objectSceneChecker is null)
			{
				_objectSceneChecker = new ObjectSceneChecker();
			}
			_objectSceneChecker.TargetSceneData = this;
			_objectSceneChecker.ObjectData = _objectData;
			_objectSceneChecker.ObjectClassCycle = _objectClassCycle;
			_objectClassCycle.AddObjectClass(_objectSceneChecker);
			_objectSceneChecker.Use();

			if (_objectData.isMonster)
			{
				StatData _statData = gameObject.GetComponentInParent<StatData>();
				_statData.LoadSaveData(_objectData.statSaveData);
				_objectData.SetObserble(_statData);
			}

			gameObject.SetActive(true);

			objectCheckerList.Add(_objectSceneChecker);
			//���� Ǯ������ ��ü
		}


		private void InitLodObjects()
		{
			foreach (ObjectData _objectData in objectDataList.objectDataList)
			{
				if (_objectData.lodType == LODType.On)
				{
					lodMaker?.InitAddLOD(_objectData);
				}
				//���� Ǯ������ ��ü
			}
		}

	}

	[System.Serializable]
	public class ObjectDataList
	{
		public List<ObjectData> objectDataList = new List<ObjectData>();
	}
}
