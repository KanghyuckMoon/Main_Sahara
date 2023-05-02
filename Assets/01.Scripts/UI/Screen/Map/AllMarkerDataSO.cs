using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Map
{
    [CreateAssetMenu(menuName = "SO/Marker/AllMarkerDataSO")]
    public class AllMarkerDataSO : ScriptableObject
    {
        public List<MarkerDataSO> markeDataSOList = new List<MarkerDataSO>();
        public Dictionary<string, MarkerDataSO> markerDataSODic = new Dictionary<string, MarkerDataSO>();

        public void OnValidate()
        {
            markerDataSODic.Clear();
            foreach (var _markerDataSo in markeDataSOList)
            {
                markerDataSODic.Add(_markerDataSo.markerData.key, _markerDataSo);
            }
        }

        public MarkerData GetMarkerData(string _key)
        {
            if (markerDataSODic.TryGetValue(_key, out MarkerDataSO _dataSO) is true)
            {
                return _dataSO.markerData;
            }

            return null;
        }
    }
}