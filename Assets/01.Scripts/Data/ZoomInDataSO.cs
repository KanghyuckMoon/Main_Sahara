using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.SeralizableDictionary;

namespace Data
{
    [System.Serializable]
    public class StringListZoomInData : SerializableDictionary<string, ZoomData> { };
    
    [CreateAssetMenu(menuName = "SO/ZoomInDataSO")]
    public class ZoomInDataSO : ScriptableObject
    {
        public StringListZoomInData ZoomInData;
    }

    [System.Serializable]
    public struct ZoomData
    {
        public float duration_Zoom;
        public float duration_Back;
        public float value;
    }
}