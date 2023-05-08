using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UI.Map;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using Utill.Addressable;
using System.Linq;
using Inventory;
using UI.Manager;

namespace  UI.Map
{
    public class MarkerSetComp
    {
        private VisualElement parent;
        private List<MarkerSlotPr> markerSlotPrList = new List<MarkerSlotPr>();
        private List<VisualElement> markerList = new List<VisualElement>(); // 맵에 찍혀있는 마커 리스트 
        private Dictionary<VisualElement,MarkerData> activeMarkerDataDic = new Dictionary<VisualElement,MarkerData>(); // 맵에 찍혀있는 마커 데이터 리스트 
        
        private MapView mapView;

        // 현재 활성화 중인 슬롯 
        private MarkerSlotPr curMarkerSlotPr = null;
        private VisualElement deleteTarget = null;
        private MarkersComponent markersComponent;

        private bool isMapOver = false; // 맵 위에 커서가 있는가 
        private bool isInputCtrl = false; // ctrl 누르고 있는 중인가 
        
        private const string selectStr = "active_select"; 
        private const string deleteMarkerStr = "delete_marker"; 
        
        // 프로퍼티 
        private Sprite CurMarkerSprite =>
            AddressablesManager.Instance.GetResource<Sprite>(curMarkerSlotPr.MarkerData.spriteAddress); 
        private Vector2 MousePos
        {
            get
            {
                if (curMarkerSlotPr is null) return Vector2.zero;
                return new Vector2(Input.mousePosition.x - curMarkerSlotPr.Parent.resolvedStyle.width / 2,
                    1080 - Input.mousePosition.y - curMarkerSlotPr.Parent.resolvedStyle.height / 2);
            }   
        }
        public MarkerSetComp(VisualElement _v,MapView _mapView,MarkersComponent _markersComponent)
        {
            //deleteTarget.panel.Pick();  
            this.parent = _v; 
            this.mapView = _mapView; 
            this.markersComponent = _markersComponent; 
            
            mapView.GhostIcon.AddManipulator(new Dragger(mapView.GhostIcon));
            
            mapView.Map.RegisterCallback<PointerDownEvent>(ClickMarker);
            
            mapView.MarkerSetPanel.RegisterCallback<MouseOverEvent>((x) => { mapView.ActiveGhostIcon(false);}); 
            mapView.MarkerSetPanel.RegisterCallback<PointerOutEvent>((x) => { mapView.ActiveGhostIcon(true);}); 
            
        }

        private void ClickMarker(PointerDownEvent _evt)
        {
            // 좌클릭인지 체크, Ctrl 입력 하지 않았는지 (삭제) 체크, 현재 선택한 마커 있는지 체크 
            if (_evt.button is not 0 || isInputCtrl is true || curMarkerSlotPr is null) return;
           // 마커 생성 
            var _marker = CreateMarker((Vector2)_evt.localPosition -
                                       new Vector2(mapView.Map.resolvedStyle.width / 2,
                                           mapView.Map.resolvedStyle.height / 2)); 
                
            /*_marker.RegisterCallback<MouseOverEvent>((x) => { UIManager.Instance.SetCursorImage(CursorImageType.deleteMapMarker); });
            _marker.RegisterCallback<MouseLeaveEvent>((x) =>
            {
                UIManager.Instance.SetCursorImage(CursorImageType.defaultCursor);
            });*/
            
            // 현재 맵에 찍혀있음 리스트에 추가 
            markerList.Add(_marker);
            // 마커 , 마커데이터 dictionary  추가 
            activeMarkerDataDic.Add(_marker,curMarkerSlotPr.MarkerData);
                
            // 마커 개수 0 인가 
            bool _isZeroCount = MarkerDataManager.Instance.RemoveHaveMarker(curMarkerSlotPr.MarkerData.key);
            if (_isZeroCount is true)
            {
                // 따라오는 이미지 끄기 
                ActiveGhostIcon(false);
                curMarkerSlotPr = null; 
            }
            UpdateMarker(); //마커 ui 업데이트 
        }
        
        /// <summary>
        /// 마커 
        /// </summary>
        public void Update()
        {
            if (curMarkerSlotPr is not null)
            {
                FollowCursor(MousePos);
            }


            if (Input.GetKey(KeyCode.LeftControl))
            {
                isInputCtrl = true;
                DeleteMarker(); 
                if(Input.GetMouseButtonDown(0))
                {
                    DeleteMarker();
                    if (deleteTarget != null)
                    {
                        deleteTarget.RemoveFromHierarchy();
                        MarkerDataManager.Instance.AddHaveMarker(GetMarkerData(deleteTarget).key);
                        UpdateMarker(); 
                        deleteTarget = null;
                    }
                }   
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                deleteTarget.RemoveFromClassList(deleteMarkerStr);
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                isInputCtrl = false; 
            }
        
        }

        /// <summary>
        /// 마커 삭제 
        /// </summary>
        private void DeleteMarker()
        {
            // 마우스 커서 위치에 마커 있는지 체크 
            IEnumerable<VisualElement> _slots =markerList.
                Where((x) => x.worldBound.Overlaps(new Rect(Input.mousePosition,new Vector2(200,200))));
            
            // 슬롯에 드랍 했다면
            if (_slots.Count() != 0)
            {
                // 가장 가깝게 드랍한 슬롯 
                VisualElement _closedSlot = _slots.OrderBy(x =>
                    Vector2.Distance(x.worldBound.position, mapView.GhostIcon.worldBound.position)).First();
                
                // 스타일 클래스 추가 
                _closedSlot.ElementAt(0).AddToClassList(deleteMarkerStr);
                deleteTarget = _closedSlot;
                // 데이터 가져오기 
                UpdateMarker(); 
            }
        }
        
        private void FollowCursor(Vector2 _pos)
        {
            if (curMarkerSlotPr is not null)
            {
                // 마우스 따라가기 
                float _width = mapView.GhostIcon.resolvedStyle.width; 
                float _height = mapView.GhostIcon.resolvedStyle.height;
                Vector2 _mapScale = mapView.Map.parent.transform.scale;
                
                mapView.GhostIcon.transform.position = new Vector2(Input.mousePosition.x - _width/2 *  _mapScale.x
                    , 1080 - Input.mousePosition.y - _height/2 * _mapScale.y);
                mapView.GhostIcon.transform.scale = _mapScale; 
            }
        }
        /// <summary>
        /// 마커 슬롯 생성 
        /// </summary>
        public void UpdateMarker()
        {
            ClearMarkers();
            //var _dataList = InventoryManager.Instance.GetMarkerList();
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
            mapView.GhostIcon.style.width =
                (int)(CurMarkerSprite.bounds.size.x * 400);
            mapView.GhostIcon.style.height =
                (int)(CurMarkerSprite.bounds.size.y * 400);
        }

        private VisualElement CreateMarker(Vector2 _pos)
        {
            if (curMarkerSlotPr is null) return null; 
         
            return markersComponent.CreateMarker(_pos, mapView.MarkerParent,CurMarkerSprite);
        }
        
        private  MarkerData GetMarkerData(VisualElement _element)
        {
            foreach (var _markerData in activeMarkerDataDic)
            {
                if (_markerData.Key.Equals(_element))
                {
                    return _markerData.Value; 
                }
            }
            /*foreach (var _markerSlot in markerSlotPrList)
            {
                if (_markerSlot.Parent.Equals(_element))
                    return _markerSlot.MarkerData; 
            }*/

            return null; 
        }

    }    
}

