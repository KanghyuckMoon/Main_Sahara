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
    }
}