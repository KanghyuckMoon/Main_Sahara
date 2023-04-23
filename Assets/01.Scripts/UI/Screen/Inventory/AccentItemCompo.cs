using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using UnityEngine.UIElements;
using Utill.Addressable;

namespace UI.Inventory
{
    public class AccentItemCompo
    {
        private Transform inventoryCam; 
        private AllItemDataSO allItemDataSO;

        private Dictionary<string,GameObject> modelDic = new Dictionary<string,GameObject>();
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
                    string _modelKey = _itemData.modelkey.Replace("\r", ""); 
                    if (_modelKey == String.Empty)
                    {
                        continue;
                    }
                    try
                    {
                        _prefab = AddressablesManager.Instance.GetResource<GameObject>(_modelKey);
                    }
                    catch (Exception e)
                    {
                            Debug.LogError(e.Message+ "@@@@@@ " + _modelKey);
                        continue; 
                    }
                    
                    
                    GameObject _instance = GameObject.Instantiate(_prefab, inventoryCam);
                    _instance.name = _itemData.modelkey;
                    _instance.transform.localPosition = new Vector3(0, -0.218f, 2.75f);
                    modelDic.Add(_itemData.modelkey,_instance);
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
            curActiveModel.transform.eulerAngles += _rotV;
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
            curActiveModel.transform.eulerAngles += _rotV;
            // curActiveModel.transform.rotation = Quaternion.AngleAxis(_rotV.x, Vector3.right);
        }

        public void ActiveModel(string _key)
        {
            InactiveAllModels();
            curActiveModel = this.modelDic[_key];
            curActiveModel.SetActive(true); 
            
        }

        /// <summary>
        ///  모든 모델 비활성화
        /// </summary>
        public void InactiveAllModels()
        {
            foreach (var _model in modelDic)
            {
                _model.Value.SetActive(false);                
            }
            curActiveModel = null; 
        }
        //   모든 아이템 저장 리스트 
        // SO 받고 생성 init 
        // 프리팹 키 받고 활성화
        // 모든 아이템 비활성화 
    }
    
}
