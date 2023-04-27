using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  UI.Map
{
    public class MarkerTest : MonoBehaviour
    {
        [SerializeField]
        private AllMarkerDataSO _allMarkerDataSo;
        [SerializeField]
        private HaveMarkerDataSO haveMarkerDataSO; 

        [ContextMenu("보유 마커 모든 마커들로 초기화")]
        public void InitHaveMarker()
        {
            haveMarkerDataSO.markerDataList.Clear();
            _allMarkerDataSo.markeDataSOList.ForEach((x) => 
            {
                haveMarkerDataSO.markerDataList.Add(new MarkerData(x));
            }); 
        }
        
        [ContextMenu("AA")]
        public void TestInit()
        {
            _allMarkerDataSo.markeDataSOList.ForEach((x) =>
            {
                x.markerData.count = 10;
            });
        }
    }    
}

