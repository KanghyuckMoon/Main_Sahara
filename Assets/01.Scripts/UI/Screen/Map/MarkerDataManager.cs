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
        
        // 프로퍼티 
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

        public List<MarkerData> GetAllHaveMakrerList()
        {
            return haveMarkerSO.markerDataList;;
        }

        public void RemoveHaveMarker(string _key, int _count = 1)
        {
            haveMarkerSO.markerDataList.Remove(allMarkerDataSO.GetMarkerData(_key));
        }

        public void AddHaveMarker(string _key, int _count = 1)
        {
            haveMarkerSO.markerDataList.Add(allMarkerDataSO.GetMarkerData(_key));
        }
    }    
}

