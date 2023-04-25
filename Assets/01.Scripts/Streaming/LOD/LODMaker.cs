using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Pool;

namespace Streaming
{
	public class LODMaker : MonoBehaviour
	{
		#region Debug
		[SerializeField]
		private string lodAddress;
		[SerializeField]
		private Vector3 localPos;
		[SerializeField]
		private bool isSetFadeCross = true;
		#endregion
		
		private float lod0 = 1f;
		[SerializeField, Range(0f, 1f)]
		private float lod1 = 0.99f;
		[SerializeField, Range(0f, 1f)]
		private float lod2 = 0.98f;
		[SerializeField, Range(0f, 1f)]
		private float lod3 = 0.01f;

		[SerializeField, Range(0f, 1f)]
		private float lod0Width = 0f;
		[SerializeField, Range(0f, 1f)]
		private float lod1Width = 0f;
		[SerializeField, Range(0f, 1f)]
		private float lod2Width = 1f;
		[SerializeField, Range(0f, 1f)]
		private float lod3Width = 0.05f;

		private bool isLoad
		{
			get
			{
				return subSceneObj.IsActiveScene();
			}
		}
		private LODGroup lodGroup = null;
		private SubSceneObj subSceneObj = null;
		private Dictionary<long, ObjectData> renderObjectDic = new Dictionary<long, ObjectData>();
		private Dictionary<long, GameObject> objectList = new Dictionary<long, GameObject>();
		private bool isInit = false;
		private int unloadCount = 0;
		private bool isUnLoadUpdate = false;

		public void Init(SubSceneObj _subSceneObj)
		{
			if (isInit)
			{
				return;
			}
			subSceneObj = _subSceneObj;
			lodGroup = GetComponent<LODGroup>();
			lodGroup.enabled = false;
			if (isSetFadeCross)
			{
				lodGroup.fadeMode = LODFadeMode.CrossFade;
			}
			renderObjectDic = new Dictionary<long, ObjectData>();
			objectList = new Dictionary<long, GameObject>();
			for (int i = 0; i < transform.childCount; ++i)
			{
				AddObjectToDic(transform.GetChild(i).gameObject);
			}
			isInit = true;

			StartCoroutine(IEUpdateLODObjects());
		}

		public void Load()
		{
			lodGroup.enabled = false;
			//오브젝트 제거
			foreach (var obj in objectList)
			{
				obj.Value.SetActive(false);
				if (renderObjectDic.ContainsKey(obj.Key))
				{
					ObjectPoolManager.Instance.RegisterObjectAsync(renderObjectDic[obj.Key].lodAddress, obj.Value);
				}
			}
			objectList.Clear();
			ClearLOD();
		}

		public void UnLoad()
		{
			lodGroup.enabled = true;
			isUnLoadUpdate = true;
			
			
				if (isUnLoadUpdate)
				{
					isUnLoadUpdate = false;
					foreach (var obj in renderObjectDic)
					{
						if (!objectList.ContainsKey(obj.Key))
						{
							unloadCount++;
							objectList.Add(obj.Key, null);
							ObjectPoolManager.Instance.GetObjectAsyncParameter<KeyValuePair<long, ObjectData>>(obj.Value.lodAddress, UnloadObjectAsync, obj);
						}
					}
				}
				
			StartCoroutine(IEUpdateLODObjects());
		}

		private IEnumerator IEUpdateLODObjects()
		{
			while (true)
			{
				yield return null;
			}
			yield return null;
		}

		private void IEUnLoad()
		{
			foreach (var obj in renderObjectDic)
			{
				if (!objectList.ContainsKey(obj.Key))
				{
					unloadCount++;
					objectList.Add(obj.Key, null);
					//GameObject _obj = ObjectPoolManager.Instance.GetObject(obj.Value.lodAddress);//(obj.Value.lodAddress, UnloadObjectAsync, obj);
					//objectList[obj.Key] = _obj;
					//objectList.Add(_keyValuePair.Key, _lodObj);
					//ObjectSettingData(_obj, obj.Value)
					//_obj.SetActive(true);
					ObjectPoolManager.Instance.GetObjectAsyncParameter<KeyValuePair<long, ObjectData>>(obj.Value.lodAddress, UnloadObjectAsync, obj);
				}
			}

			//yield return null;
		}

		private void UnloadObjectAsync(GameObject _lodObj, KeyValuePair<long, ObjectData> _keyValuePair)
		{
			unloadCount--;
			objectList[_keyValuePair.Key] = _lodObj;
			//objectList.Add(_keyValuePair.Key, _lodObj);
			ObjectSettingData(_lodObj, _keyValuePair.Value);
			_lodObj.SetActive(true);
			
			if (unloadCount == 0)
			{
				ResetLODObject();
			}
		}

		private void ResetLODObject()
		{
			if (objectList.Count != renderObjectDic.Count)
			{
				foreach (var obj in renderObjectDic)
				{
					if (!objectList.ContainsKey(obj.Key))
					{
						GameObject lodObj = ObjectPoolManager.Instance.GetObject(obj.Value.lodAddress);
						objectList.Add(obj.Key, lodObj);
						ObjectSettingData(lodObj, obj.Value);
						lodObj.SetActive(true);
					}
				}
			}
			ResetLOD();
		}
		
		/// <summary>
		/// Debug 데이터에 기입한 사항대로 오브젝트데이터와 LOD를 추가함
		/// </summary>
		[ContextMenu("Generate")]
		public void Generate()
		{
			ObjectData _objectData = new ObjectData();
			GameObject _obj = GameObject.Instantiate(AddressablesManager.Instance.GetResource<GameObject>(lodAddress), null);
			_obj.transform.SetParent(transform);
			_obj.transform.localPosition = localPos;

			_objectData.position = _obj.transform.localPosition;
			_objectData.rotation = _obj.transform.rotation;
			_objectData.scale = _obj.transform.localScale;
			_objectData.isUse = true;
			_objectData.key = 0;
			_objectData.lodType = LODType.On;
			_objectData.address = lodAddress;
			_objectData.lodAddress = lodAddress;

			ResetLOD();
		}

		/// <summary>
		/// LOD 설정을 다시 불러옴
		/// </summary>
		[ContextMenu("SetLOD")]
		public void SetLOD()
		{
			ResetLOD();
		}
		/// <summary>
		/// ObejctData에 따라 LOD 생성
		/// </summary>
		/// <param name="_objectData"></param>
		public void InitAddLOD(ObjectData _objectData)
		{

			renderObjectDic.Add(_objectData.key, _objectData);
			if (!isLoad)
			{
				ResetLODObject();
			}
		}

		/// <summary>
		/// ObejctData에 따라 LOD 생성
		/// </summary>
		/// <param name="_objectData"></param>
		public void AddLOD(ObjectData _objectData)
		{
			if (!renderObjectDic.ContainsKey(_objectData.key))
			{

				if (!isLoad)
				{
					ObjectPoolManager.Instance.GetObjectAsyncParameter<ObjectData>(_objectData.lodAddress, CraeteObjectInUnLoad, _objectData);
				}

				renderObjectDic.Add(_objectData.key, _objectData);
			}
		}

		private void CraeteObjectInUnLoad(GameObject _obj, ObjectData _objectData)
		{
			_obj.SetActive(true);
			ObjectSettingData(_obj, _objectData);
			if (!objectList.ContainsKey(_objectData.key))
			{
				objectList.Add(_objectData.key, _obj);
			}
			//ResetLODObject();
		}

		private void ObjectSettingData(GameObject _obj, ObjectData _objectData)
		{
			_obj.transform.SetPositionAndRotation(_objectData.position, _objectData.rotation);
			_obj.transform.localScale = _objectData.scale;
			_obj.transform.SetParent(transform);
		}

		/// <summary>
		/// 특정 LOD 제거
		/// </summary>
		/// <param name="_key"></param>
		public void RemoveLOD(long _key)
		{
			if (renderObjectDic.ContainsKey(_key))
			{
				var _obj = renderObjectDic[_key];
				renderObjectDic.Remove(_key);
				if (objectList.TryGetValue(_key, out GameObject gameObj))
				{
					ObjectPoolManager.Instance.RegisterObject(_obj.lodAddress, gameObj);
					gameObj.SetActive(false);
				}
			}
		}

		private void ClearLOD()
		{
			Renderer[] _renderers0 = new Renderer[0];
			Renderer[] _renderers1 = new Renderer[0];
			Renderer[] _renderers2 = new Renderer[0];
			Renderer[] _renderers3 = new Renderer[0];

			LOD[] _lods = new LOD[4];
			_lods[0] = new LOD(lod0, _renderers0);
			_lods[1] = new LOD(lod1, _renderers1);
			_lods[2] = new LOD(lod2, _renderers2);
			_lods[3] = new LOD(lod3, _renderers3);
			_lods[0].fadeTransitionWidth = lod0Width;
			_lods[1].fadeTransitionWidth = lod1Width;
			_lods[2].fadeTransitionWidth = lod2Width;
			_lods[3].fadeTransitionWidth = lod3Width;

			lodGroup.SetLODs(_lods);
		}

		private void ResetLOD()
		{
			LOD[] _lods = lodGroup.GetLODs();

			List<Renderer> _renderers = new List<Renderer>();
			foreach (var _keyValue in objectList)
			{
				GameObject _obj = _keyValue.Value;
				Renderer[] _renderer = _obj.GetComponentsInChildren<Renderer>();
				for (int i = 0; i < _renderer.Length; ++i)
				{
					if (_renderers.Contains(_renderer[i]))
                    {
						continue;
                    }
					_renderers.Add(_renderer[i]);
				}
			}
			var _renderers3 = _renderers.ToArray();

			
			_lods[0].screenRelativeTransitionHeight = lod0;
			_lods[0].fadeTransitionWidth = lod0Width;

			_lods[1].screenRelativeTransitionHeight = lod1;
			_lods[1].fadeTransitionWidth = lod1Width;

			_lods[2].screenRelativeTransitionHeight = lod2;
			_lods[2].fadeTransitionWidth = lod2Width;

			_lods[3].renderers = _renderers3;
			_lods[3].screenRelativeTransitionHeight = lod3;
			_lods[3].fadeTransitionWidth = lod3Width;

			lodGroup.SetLODs(_lods);
		}
		private void AddObjectToDic(GameObject _obj)
		{
			ObjectData _objectData = new ObjectData();
			_obj.transform.SetParent(transform);
			_obj.transform.localPosition = localPos;

			_objectData.position = _obj.transform.position;
			_objectData.rotation = _obj.transform.rotation;
			_objectData.scale = _obj.transform.localScale;
			_objectData.isUse = true;
			_objectData.key = ObjectData.totalKey++;
			_objectData.lodType = LODType.On;
			_objectData.address = lodAddress;
			_objectData.lodAddress = lodAddress;

			renderObjectDic.Add(_objectData.key, _objectData);
			objectList.Add(_objectData.key, _obj);

			ResetLOD();
		}

	}

}