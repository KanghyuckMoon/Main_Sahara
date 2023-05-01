using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;

namespace UI.Map
{
    public class MarkerDataManager : MonoSingleton<MarkerDataManager>
    {
        private AllMarkerDataSO allMarkerDataSO;
        private HaveMarkerDataSO haveMarkerSO; 
        
        // ������Ƽ 
        private AllMarkerDataSO AllMarkerDataSO
        {
            get
            {
                if (allMarkerDataSO is null)
                {
                     allMarkerDataSO = AddressablesManager.Instance.GetResource<AllMarkerDataSO>("AllMarkerDataSO");
                }
                return allMarkerDataSO; 
            }
        }
        public override void Awake()
        {
            base.Awake();
            allMarkerDataSO = AddressablesManager.Instance.GetResource<AllMarkerDataSO>("AllMarkerDataSO");
            haveMarkerSO = AddressablesManager.Instance.GetResource<HaveMarkerDataSO>("HaveMarkerDataSO");
        }

        /// <summary>
        /// ���� ������ �ʱ�ȭ (��� ����������) 
        /// </summary>
        public void InitHaveItems()
        {
            haveMarkerSO.markerDataList.Clear();
            AllMarkerDataSO.markeDataSOList.ForEach((x) => 
            {
                haveMarkerSO.markerDataList.Add(new MarkerData(x));
            }); 
        }
        public List<MarkerData> GetAllHaveMakrerList()
        {
            return haveMarkerSO.markerDataList;;
        }

        public bool RemoveHaveMarker(string _key, int _count = 1)
        {
            for (int i = 0; i < _count; i++)
            {
                // ������ 0 ���� ū�� 
                bool _isNotCountZero = haveMarkerSO.RemoveHaveMarker(_key);
                if (_isNotCountZero is false)
                {
                    return true; 
                }
            }
            return false; 
        }

        public void AddHaveMarker(string _key, int _count = 1)
        {
            MarkerData _data = GetMarkerData(_key);
            if (_data is null)
            {
                haveMarkerSO.markerDataList.Add(allMarkerDataSO.GetMarkerData(_key));
            }
            for (int i = 0; i < _count; i++)
            {
                // ����Ʈ�� ������ ���� �߰��ϰ�  ������ ���� �߰� 
                haveMarkerSO.AddHaveMarker(_key);
            }
        }           
        
        private MarkerData GetMarkerData(string _key)
        {
            foreach (var _data in haveMarkerSO.markerDataList)
            {
                if (_data.key == _key)
                {
                    return _data; 
                }
            }

            return null; 
        }
    }    
}

