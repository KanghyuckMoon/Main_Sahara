using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Map
{
    [System.Serializable]
    public class MarkerData
    {
        public string key; 
        public string spriteAddress;
        public int price; 
        public int count;

        public MarkerData(MarkerDataSO _markerDataSO)
        {
            this.key = _markerDataSO.markerData.key; 
            this.spriteAddress = _markerDataSO.markerData.spriteAddress; 
            this.price = _markerDataSO.markerData.price; 
            this.count = _markerDataSO.markerData.count; 
        }
    }
}