using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UI.Map;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using Utill.Addressable;

namespace  UI.Map
{
    public class MarkerSetComp
    {
        private VisualElement parent;
        private List<MarkerSlotPr> markerSlotPrList = new List<MarkerSlotPr>();
        private MapView mapView;

        // 현재 활성화 중인 슬롯 
        private MarkerSlotPr curMarkerSlotPr = null;
        private MarkersComponent markersComponent;
        
        private const string selectStr = "active_select"; 
        public MarkerSetComp(VisualElement _v,MapView _mapView,MarkersComponent _markersComponent)
        {
            this.parent = _v; 
            this.mapView = _mapView; 
            this.markersComponent = _markersComponent; 
            
            mapView.GhostIcon.AddManipulator(new Dragger(mapView.GhostIcon));
            mapView.Map.RegisterCallback<PointerDownEvent>((x) =>
            {
                if (x.button is not 0) return; 
                CreateMarker((Vector2)x.localPosition -
                             new Vector2(mapView.Map.resolvedStyle.width / 2, mapView.Map.resolvedStyle.height / 2));
            });
            mapView.Map.RegisterCallback<PointerOverEvent>((x) =>
            {
                FollowCursor(x.localPosition);
            });
        }

        /// <summary>
        /// 마커 
        /// </summary>
        public void Update()
        {

        }

        private void FollowCursor(Vector2 _pos)
        {
            if (curMarkerSlotPr is not null)
            {
                // 마우스 따라가기 
                mapView.GhostIcon.transform.position = _pos; 
            }
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
                parent.Add(markerSlotPr.Parent);
                
                markerSlotPr.AddClickEvent(() => ClickMarkerSlot(markerSlotPr));
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

        private  void ActiveGhostIcon(bool _isActive)
        {
            mapView.GhostIcon.style.display = _isActive ? DisplayStyle.Flex : DisplayStyle.None; 
        }
        
        private void ClickMarkerSlot(MarkerSlotPr _markerSlot)
        {
            if (curMarkerSlotPr is not null)
            {
                curMarkerSlotPr.SelectSlot(false); 
            }

            // 고스트아이콘 활성화 
            ActiveGhostIcon(true); 
            curMarkerSlotPr = _markerSlot;
            curMarkerSlotPr.SelectSlot(true); 
            mapView.GhostIcon.style.backgroundImage = AddressablesManager.Instance.GetResource<Texture2D>(_markerSlot.MarkerData.spriteAddress);
        }

        private void CreateMarker(Vector2 _pos)
        {
            if (curMarkerSlotPr is null) return; 
         
            markersComponent.CreateMarker(_pos, mapView.MarkerParent, 
                AddressablesManager.Instance.GetResource<Sprite>(curMarkerSlotPr.MarkerData.spriteAddress));
            
            //MarkerDataManager.Instance.RemoveHaveMarker(curMarkerSlotPr.MarkerData.key);
          //  if ()
            {
                
            }
        }

    }    
}

