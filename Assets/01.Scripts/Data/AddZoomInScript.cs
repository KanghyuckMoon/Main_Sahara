using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class AddZoomInScript : MonoBehaviour
    {
        [SerializeField] private ZoomInDataSO zoomInDataSO;

        [SerializeField] private string name;
        [SerializeField] private ZoomData zoomData;
        
        [ContextMenu("줌인 추가")]
        public void SetZoomIn()
        {
            if (zoomInDataSO.ZoomInData.TryGetValue(name, out var _value))
            {
                zoomInDataSO.ZoomInData[name] = zoomData;
                return;
            }
            
            zoomInDataSO.ZoomInData.Add(name, zoomData);
            
            
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(zoomInDataSO);
#endif
        }
    }
}