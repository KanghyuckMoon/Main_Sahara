using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UI.Map;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using Utill.Addressable;
using System.Linq; 

namespace  UI.Map
{
    public class MarkerSetComp
    {
        private VisualElement parent;
        private List<MarkerSlotPr> markerSlotPrList = new List<MarkerSlotPr>();
        private List<VisualElement> markerList = new List<VisualElement>();
        private MapView mapView;

        // 현재 활성화 중인 슬롯 
        private MarkerSlotPr curMarkerSlotPr = null;
        private VisualElement deleteTarget = null;
        private MarkersComponent markersComponent;

        private bool isMapOver = false; // 맵 위에 커서가 있는가 
        
        private const string selectStr = "active_select"; 
        private const string deleteMarkerStr = "delete_select"; 
        
        // 프로퍼티 
        private Vector2 MousePos => new Vector2(Input.mousePosition.x - curMarkerSlotPr.Parent.resolvedStyle.width / 2,
            1080 - Input.mousePosition.y - curMarkerSlotPr.Parent.resolvedStyle.height / 2);
        public MarkerSetComp(VisualElement _v,MapView _mapView,MarkersComponent _markersComponent)
        {
            //deleteTarget.panel.Pick();  
            this.parent = _v; 
            this.mapView = _mapView; 
            this.markersComponent = _markersComponent; 
            
            mapView.GhostIcon.AddManipulator(new Dragger(mapView.GhostIcon));
            mapView.Map.RegisterCallback<PointerDownEvent>((x) =>
            {
                if (x.button is not 0) return; 
                markerList.Add(CreateMarker((Vector2)x.localPosition -
                                       new Vector2(mapView.Map.resolvedStyle.width / 2, mapView.Map.resolvedStyle.height / 2)));
                MarkerDataManager.Instance.RemoveHaveMarker(curMarkerSlotPr.MarkerData.key);
            });
            
            mapView.MarkerSetPanel.RegisterCallback<MouseEnterEvent>((x) => { mapView.ActiveGhostIcon(false);}); 
            mapView.MarkerSetPanel.RegisterCallback<PointerOutEvent>((x) => { mapView.ActiveGhostIcon(true);}); 
        }

        /// <summary>
        /// 마커 
        /// </summary>
        public void Update()
        {
            FollowCursor(MousePos);

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                for (int i = 0; i < markerList.Count; i++)
                {
                    markerList[i].RemoveFromHierarchy();
                }
                markerList.Clear();
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                DeleteMarker();
                if (deleteTarget != null)
                {
                    deleteTarget.RemoveFromHierarchy();
                        deleteTarget = null; 
                }
            }
            

        }

        private void DeleteMarker()
        {
            IEnumerable<VisualElement> _slots =markerList.
                Where((x) => x.worldBound.Overlaps(mapView.GhostIcon.worldBound));

            // 슬롯에 드랍 했다면
            if (_slots.Count() != 0)
            {
                // 가장 가깝게 드랍한 슬롯 
                VisualElement _closedSlot = _slots.OrderBy(x =>
                    Vector2.Distance(x.worldBound.position, mapView.GhostIcon.worldBound.position)).First();
                _closedSlot.ElementAt(0).AddToClassList(deleteMarkerStr);
                deleteTarget = _closedSlot; 
            }
        }
        private void FollowCursor(Vector2 _pos)
        {
            if (curMarkerSlotPr is not null)
            {
                // 마우스 따라가기 
                mapView.GhostIcon.transform.position = _pos; 
                Debug.Log("Folllow@@@");
            }
        }
        /// <summary>
        /// 마커 슬롯 생성 
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

        private VisualElement CreateMarker(Vector2 _pos)
        {
            if (curMarkerSlotPr is null) return null; 
         
            return markersComponent.CreateMarker(_pos, mapView.MarkerParent, 
                AddressablesManager.Instance.GetResource<Sprite>(curMarkerSlotPr.MarkerData.spriteAddress));
            
            //MarkerDataManager.Instance.RemoveHaveMarker(curMarkerSlotPr.MarkerData.key);
          //  if ()
            {
                
            }
        }

    }    
}

