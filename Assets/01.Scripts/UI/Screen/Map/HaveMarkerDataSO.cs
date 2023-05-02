using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Map
{
    [CreateAssetMenu(menuName = "SO/Marker/HaveMarkerDataSO")]
    public class HaveMarkerDataSO : ScriptableObject
    {
        public List<MarkerData> markerDataList = new List<MarkerData>();

        public void AddHaveMarker(string _key)
        {
            MarkerData _data = GetMarkerData(_key);

            if (_data is not null)
            {
                _data.count++; 
            }
        }

        /// <summary>
        /// true : 개수만 감소 // false : 데이터 삭제 
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public bool RemoveHaveMarker(string _key)
        {
            MarkerData _data = GetMarkerData(_key);
            if (_data is not null)
            {
                _data.count--;
                if (_data.count <= 0)
                {
                    markerDataList.Remove(_data);
                    return false; 
                }
            }

            return true; 
        }

        private MarkerData GetMarkerData(string _key)
        {
            foreach (var _data in markerDataList)
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