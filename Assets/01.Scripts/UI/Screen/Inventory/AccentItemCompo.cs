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
            inventoryCam = _invenCam; 
            allItemDataSO = AddressablesManager.Instance.GetResource<AllItemDataSO>("AllItemDataSO");
            
            return;   
            // 생성 
            if (isInit == false)
            {                 
                isInit = true; 
                foreach (var _itemData in allItemDataSO.itemDataSOList)
                {
                    GameObject _prefab = new GameObject();
                    if (_itemData.prefebkey == String.Empty)
                    {
                        continue;
                    }
                    try
                    {
                        Debug.LogError("@@@@@@" + _itemData.prefebkey);
                        _prefab = AddressablesManager.Instance.GetResource<GameObject>(_itemData.prefebkey);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("@@@@@@" + _itemData.prefebkey);
                    }
                    GameObject _instance = GameObject.Instantiate(_prefab, inventoryCam);
                    _instance.name = _itemData.prefebkey;
                    _instance.transform.position = new Vector3(0, -0.218f, 2.75f);
                    modelDic.Add(_itemData.prefebkey,_instance);
                }
            }

        }

        public void Update()
        {
        }
        public void RotateModel(Vector3 _rotV)
        {
            if (curActiveModel is null) return;

            curActiveModel.transform.rotation = Quaternion.AngleAxis(_rotV.y, Vector3.up);
           // curActiveModel.transform.rotation = Quaternion.AngleAxis(_rotV.x, Vector3.right);
            
        }

        public void ActiveModel(string _key)
        {
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
