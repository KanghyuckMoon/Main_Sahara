using System.Collections.Generic;
using UnityEngine;
using UI.Base;

namespace UI.Upgrade
{
    public enum LineType
    {
        UpgradeDefault, 
        UpgradeActive, 
        
    }
    
    [System.Serializable]
    public class LineData
    {
        public LineType key; 
        public Color color;
        public Material material; 
    }
    [CreateAssetMenu(menuName = "SO/UI/LinerDataSO")]
    public class LineDataSO : ScriptableObject
    {
        [SerializeField]
        public List<LineData> lineDataList = new List<LineData>();

        public LineData GetLineData(LineType _type)
        {
            return lineDataList.Find((x) => x.key == _type);
        }
    }
}