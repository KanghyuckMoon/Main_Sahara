using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using UnityEngine.UIElements;
using Utill.Addressable;

namespace UI.Inventory
{
    public struct St_Transform
    {
        public Vector3 position;
        public Quaternion rotation;

        public St_Transform(Transform _trm)
        {
            position = _trm.position;
            rotation = _trm.rotation; 
        }
    }
    public class AccentItemCompo
    {
        private Transform inventoryCam; 
        private AllItemDataSO allItemDataSO;
        private Vector3 modelRot;

        private Dictionary<string,GameObject> modelDic = new Dictionary<string,GameObject>();
        // 초기화시 사용할 모델 위치, 회전 정보 
        private Dictionary<string, St_Transform> modelTrmDic = new Dictionary<string, St_Transform>(); 
        private GameObject curActiveModel = null; 
        
        private bool isInit = false; 
        
        public void Init(Transform _invenCam)
        {
            // 초기화
            //modelList.Clear();
            inventoryCam = _invenCam.Find("ModelParent"); 
            allItemDataSO = AddressablesManager.Instance.GetResource<AllItemDataSO>("AllItemDataSO");
            
          //  return;   
            // 생성 
            if (isInit == false)
            {                 
                isInit = true; 
                foreach (var _itemData in allItemDataSO.itemDataSOList)
                {
                    GameObject _prefab;
                    string _modelKey = _itemData.modelkey; 
                    if (_modelKey == String.Empty)
                    {
                        continue;
                    }
                    try
                    {
                        _modelKey = _modelKey.Replace("\r", "");
                        _prefab = AddressablesManager.Instance.GetResource<GameObject>(_modelKey);
                    }
                    catch (Exception e)
                    {
                            Debug.LogError(e.Message+ "@@@@@@ " + _modelKey);
                        continue; 
                    }
                    
                    
                    GameObject _instance = GameObject.Instantiate(_prefab, inventoryCam);
                    _instance.name = _itemData.modelkey;
                    _instance.layer = 11;
                    _instance.transform.localPosition = new Vector3(0, -0.218f, 2.75f);
                    if (!modelDic.ContainsKey((_itemData.modelkey)))
                    {
                        modelDic.Add(_itemData.modelkey,_instance);
                    }

                    if (modelTrmDic.ContainsKey(_itemData.modelkey) == false)
                    {
                        modelTrmDic.Add(_itemData.modelkey, new St_Transform(_instance.transform));
                    }
                }
            }

        }

        public void Update()
        {
        }
        public void RotateModelHorizon(Vector3 _rotV)
        {
            if (curActiveModel is null) return;

            Debug.Log("클릭중");
            //Quaternion xQut =  Quaternion.AngleAxis(curActiveModel.transform.eulerAngles.y + _rotV.y, Vector3.up); 
            //Quaternion yQut =  Quaternion.AngleAxis(curActiveModel.transform.eulerAngles.x + _rotV.x, Vector3.right); 
            //Quaternion _resultQut = xQut * yQut;  
            modelRot += _rotV;
           // curActiveModel.transform.rotation = Quaternion.AngleAxis(_rotV.x, Vector3.right);
        }
        
        public void RotateModelVertical(Vector3 _rotV)
        {
            if (curActiveModel is null) return;

            Debug.Log("클릭중");
            //Quaternion xQut =  Quaternion.AngleAxis(curActiveModel.transform.eulerAngles.y + _rotV.y, Vector3.up); 
            //Quaternion yQut =  Quaternion.AngleAxis(curActiveModel.transform.eulerAngles.x + _rotV.x, Vector3.right); 
            //Quaternion _resultQut = xQut * yQut;  
            //curActiveModel.transform.rotation *= yQut;
            modelRot += _rotV;
            
            // curActiveModel.transform.rotation = Quaternion.AngleAxis(_rotV.x, Vector3.right);
        }

        public void UpdateRotateModel()
        {
            curActiveModel.transform.localEulerAngles = modelRot;
        }

        public void ActiveModel(string _key)
        {
            InactiveAllModels();
            // 모델이 존재하면 
            if(modelDic.TryGetValue(_key, out GameObject _obj)== true)
            {
                curActiveModel = this.modelDic[_key];
                curActiveModel.SetActive(true); 
            }

            
        }

        /// <summary>
        ///  모든 모델 비활성화
        /// </summary>
        public void InactiveAllModels()
        {
            foreach (var _model in modelDic)
            {
                _model.Value.SetActive(false);
                // 위치 회전 초기화 
                _model.Value.transform.position = modelTrmDic[_model.Key].position;
                _model.Value.transform.rotation = modelTrmDic[_model.Key].rotation;
            }
            
            curActiveModel = null; 
        }
        //   모든 아이템 저장 리스트 
        // SO 받고 생성 init 
        // 프리팹 키 받고 활성화
        // 모든 아이템 비활성화 
    }
    
}
