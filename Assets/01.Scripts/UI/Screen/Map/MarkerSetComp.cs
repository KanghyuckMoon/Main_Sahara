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
        private bool isInputCtrl = false; // ctrl 누르고 있는 중인가 
        
        private const string selectStr = "active_select"; 
        private const string deleteMarkerStr = "delete_select"; 
        
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
            
            mapView.Map.RegisterCallback<PointerDownEvent>((x) =>
            {
                if (x.button is not 0 || isInputCtrl is true || curMarkerSlotPr is null) return; 
                markerList.Add(CreateMarker((Vector2)x.localPosition -
                                       new Vector2(mapView.Map.resolvedStyle.width / 2, mapView.Map.resolvedStyle.height / 2)));
                // 마커 개수 0 인가 
                bool _isZeroCount = MarkerDataManager.Instance.RemoveHaveMarker(curMarkerSlotPr.MarkerData.key);
                if (_isZeroCount is true)
                {
                    ActiveGhostIcon(false);
                    curMarkerSlotPr = null; 
                }
                UpdateMarker(); //마커 ui 업데이트 
            });
            
            mapView.MarkerSetPanel.RegisterCallback<MouseOverEvent>((x) => { mapView.ActiveGhostIcon(false);}); 
            mapView.MarkerSetPanel.RegisterCallback<PointerOutEvent>((x) => { mapView.ActiveGhostIcon(true);}); 
            
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
                isInputCtrl = true; 
                if(Input.GetMouseButtonDown(0))
                {
                    DeleteMarker();
                    if (deleteTarget != null)
                    {
                        deleteTarget.RemoveFromHierarchy();
                        MarkerDataManager.Instance.AddHaveMarker(GetMarkerData(deleteTarget).key);
                        deleteTarget = null; 
                        
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                isInputCtrl = false; 
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
                // 데이터 가져오기 
            }
        }
        
        private void FollowCursor(Vector2 _pos)
        {
            if (curMarkerSlotPr is not null)
            {
                // 마우스 따라가기 
                Debug.Log("@@@@@Mouse Pos" + _pos);
                Debug.Log("@@@@@GhostIcon Pos" + mapView.GhostIcon.transform.position);
                                                                                            
//                mapView.GhostIcon.transform.position = _pos;
                float _width = mapView.GhostIcon.resolvedStyle.width; 
                float _height = mapView.GhostIcon.resolvedStyle.height;
                Vector2 _scale = mapView.GhostIcon.transform.scale;
                Vector2 _mapScale = mapView.Map.parent.transform.scale;
                
                float _addWidth = 0f; 
                float _addHeight = 0f; 
                // float _addWidth = Mathf.Clamp((mapView.GhostIcon.resolvedStyle.width / mapView.GhostIcon.transform.scale.x)- _width,0,float.MaxValue); 
                //float _addHeight = Mathf.Clamp((mapView.GhostIcon.resolvedStyle.height / mapView.GhostIcon.transform.scale.y)- _height, 0, float.MaxValue); 
                
                mapView.GhostIcon.transform.position = new Vector2(Input.mousePosition.x - _width/2 *  _mapScale.x
                    , 1080 - Input.mousePosition.y - _height/2 * _mapScale.y);
                mapView.GhostIcon.transform.scale = _mapScale; 
                //, 뷰   
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
            mapView.GhostIcon.style.width =
                (int)(CurMarkerSprite.bounds.size.x * 400);
            mapView.GhostIcon.style.height =
                (int)(CurMarkerSprite.bounds.size.y * 400);
        }

        private VisualElement CreateMarker(Vector2 _pos)
        {
            if (curMarkerSlotPr is null) return null; 
         
            return markersComponent.CreateMarker(_pos, mapView.MarkerParent,CurMarkerSprite);
            
            //MarkerDataManager.Instance.RemoveHaveMarker(curMarkerSlotPr.MarkerData.key);
          //  if ()
            {
                
            }
        }
        
        private  MarkerData GetMarkerData(VisualElement _element)
        {
            foreach (var _markerSlot in markerSlotPrList)
            {
                if (_markerSlot.Parent.Equals(_element))
                    return _markerSlot.MarkerData; 
            }

            return null; 
        }

    }    
}

