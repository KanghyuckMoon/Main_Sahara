using System.Collections;
using System.Collections.Generic;
using UI.Map;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

namespace  UI.Map
{
    public class MarkerSetComp
    {
        private VisualElement parent;
        private List<MarkerSlotPr> markerSlotPrList = new List<MarkerSlotPr>(); 
    
        public MarkerSetComp(VisualElement _v)
        {
            this.parent = _v; 
        }

        /// <summary>
        /// 마커 생성 
        /// </summary>
        public void UpdateMarker()
        {
            ClearMarkers();
            var _dataList = MarkerDataManager.Instance.GetAllHaveMakrerList();

            foreach (var _data in _dataList)
            {
                MarkerSlotPr markerSlotPr = new MarkerSlotPr(); 
                markerSlotPrList.Add(markerSlotPr);
                markerSlotPr.SetData(_data);
            }
            
        }

        /// <summary>
        ///  현재 있는 마커들 삭제 
        /// </summary>
        private void ClearMarkers()
        {
            markerSlotPrList.ForEach((x) => x.Parent.RemoveFromHierarchy());
            
            markerSlotPrList.Clear();
        }
    }    
}

