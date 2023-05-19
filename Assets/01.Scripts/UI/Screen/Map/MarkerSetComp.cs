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
using UI.Canvas;
using UI.Manager;
using UI.ParticleManger;
using UI.UtilManager;
using Utill.Coroutine;


namespace UI.Map
{
    public class MarkerSetComp
    {
        private VisualElement parent;
        private List<MarkerSlotPr> markerSlotPrList = new List<MarkerSlotPr>();
        private List<VisualElement> markerList = new List<VisualElement>(); // 맵에 찍혀있는 마커 리스트 

        private Dictionary<VisualElement, ItemData>
            activeMarkerDataDic = new Dictionary<VisualElement, ItemData>(); // 맵에 찍혀있는 마커 데이터 리스트 

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
            AddressablesManager.Instance.GetResource<Sprite>(curMarkerSlotPr.MarkerData.spriteKey);

        private Vector2 MousePos
        {
            get
            {
                if (curMarkerSlotPr is null) return Vector2.zero;
                return new Vector2(Input.mousePosition.x - curMarkerSlotPr.Parent.resolvedStyle.width / 2,
                    1080 - Input.mousePosition.y - curMarkerSlotPr.Parent.resolvedStyle.height / 2);
            }
        }

        public MarkerSetComp(VisualElement _v, MapView _mapView, MarkersComponent _markersComponent)
        {
            //deleteTarget.panel.Pick();  
            this.parent = _v;
            this.mapView = _mapView;
            this.markersComponent = _markersComponent;

            mapView.GhostIcon.AddManipulator(new Dragger(mapView.GhostIcon));

            mapView.Map.RegisterCallback<PointerDownEvent>(ClickMarker);

            mapView.MarkerSetPanel.RegisterCallback<MouseOverEvent>((x) => { mapView.ActiveGhostIcon(false); });
            mapView.MarkerSetPanel.RegisterCallback<PointerOutEvent>((x) => { mapView.ActiveGhostIcon(true); });
        }

        private IEnumerator CreateParticleCo(VisualElement _target)
        {
            yield return new WaitForSeconds(0.05f);
            Vector2 _pScale = mapView.Map.parent.transform.scale;
            Vector2 _particlePos = new Vector2(
                _target.worldBound.position.x + _target.ElementAt(0).resolvedStyle.width / 2 * _pScale.x,
                -_target.worldBound.position.y - _target.ElementAt(0).resolvedStyle.height / 2 * _pScale.y);


            //UIParticleManager.Instance.Play(ParticleType.Burst, _particlePos,
            //    OverlayCanvasManager.Instance.Canvas);           
            UIParticleManager.Instance.Play(ParticleType.SandBurst, _particlePos,
                OverlayCanvasManager.Instance.Canvas);
        }

        private void ClickMarker(PointerDownEvent _evt)
        {
            // 좌클릭인지 체크, Ctrl 입력 하지 않았는지 (삭제) 체크, 현재 선택한 마커 있는지 체크 
            if (_evt.button is not 0 || isInputCtrl is true || curMarkerSlotPr is null) return;

            Vector2 _markerPos = (Vector2)_evt.localPosition -
                                 new Vector2(mapView.Map.resolvedStyle.width / 2,
                                     mapView.Map.resolvedStyle.height / 2);
            // 마커 생성 
            var _marker = CreateMarker(_markerPos);
            //Vector2 _particlePos = new Vector2(_marker.worldBound.position.x, -_marker.worldBound.position.y);
            StaticCoroutineManager.Instance.InstanceDoCoroutine(CreateParticleCo(_marker));


            // ==이벤트 등록 == //  
            _marker.RegisterCallback<MouseOverEvent>((x) =>
            {
                ActiveMarker(_marker);
            });
            _marker.RegisterCallback<MouseLeaveEvent>((x) =>
            {
                if (isInputCtrl == true)
                {
                    deleteTarget = null;
                    _marker.ElementAt(0).RemoveFromClassList(deleteMarkerStr);
                    ;
                    UIManager.Instance.SetCursorImage(CursorImageType.defaultCursor);
                }
            });


            // 현재 맵에 찍혀있음 리스트에 추가 
            markerList.Add(_marker);
            // 마커 , 마커데이터 dictionary  추가 
            activeMarkerDataDic.Add(_marker, curMarkerSlotPr.MarkerData);


            // 마커 개수 0 인가 
            //bool _isZeroCount = MarkerDataManager.Instance.RemoveHaveMarker(curMarkerSlotPr.MarkerData.key);
            bool _isZeroCount = InventoryManager.Instance.ItemReduce(curMarkerSlotPr.MarkerData.key);
            if (_isZeroCount is true)
            {
                // 따라오는 이미지 끄기 
                ActiveGhostIcon(false);
                curMarkerSlotPr = null;
            }

            UpdateMarker(); //마커 ui 업데이트 
        }

        private void ActiveMarker(VisualElement _marker)
        {
            if (isInputCtrl == true)
            {
                _marker.ElementAt(0).AddToClassList(deleteMarkerStr);
                deleteTarget = _marker;
                UIManager.Instance.SetCursorImage(CursorImageType.deleteMapMarker);
            }
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

            if (Input.GetMouseButtonDown(0) == true)
            {
                if (deleteTarget != null)
                {
                    deleteTarget.RemoveFromHierarchy();
                    InventoryManager.Instance.AddItem(GetMarkerData(deleteTarget).key);
                    UpdateMarker();
                    deleteTarget = null;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isInputCtrl = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                isInputCtrl = false;
                deleteTarget = null; 
            }

            CheckMousePos(); 
            /*
            if (Input.GetKey(KeyCode.LeftControl))
            {
                isInputCtrl = true;
                DeleteMarker();
                if (Input.GetMouseButtonDown(0))
                {       
                    DeleteMarker(); 
                    // 삭제할 마커가 있으면 
                    if (deleteTarget != null)
                    {
                        deleteTarget.RemoveFromHierarchy();
                        //MarkerDataManager.Instance.AddHaveMarker(GetMarkerData(deleteTarget).key);
                        InventoryManager.Instance.AddItem(GetMarkerData(deleteTarget).key);
                        UpdateMarker();
                        deleteTarget = null;
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                isInputCtrl = false; 
                if (deleteTarget == null) return; 
                deleteTarget.RemoveFromClassList(deleteMarkerStr);
            }*/
        }

        private void CheckMousePos()
        {
            Vector2 _mousePos = UIUtil.GetUIToolkitPos(Input.mousePosition);

            foreach (var _marker in markerList)
            {
                if (UIUtil.IsVectorInTarget(_mousePos, _marker.ElementAt(0)) == true)
                {
                    ActiveMarker(_marker);
                    return; 
                } 
            }

        }
        /// <summary>
        /// 마커 삭제 
        /// </summary>
        private void DeleteMarker()
        {
            // 마우스 커서 위치에 마커 있는지 체크 
            IEnumerable<VisualElement> _slots = markerList.Where((x) =>
                x.worldBound.Overlaps(new Rect(UIUtil.GetUIToolkitPos(Input.mousePosition), new Vector2(200, 200))));

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

                mapView.GhostIcon.transform.position = new Vector2(Input.mousePosition.x - _width / 2 * _mapScale.x
                    , 1080 - Input.mousePosition.y - _height / 2 * _mapScale.y);
                mapView.GhostIcon.transform.scale = _mapScale;
            }
        }

        /// <summary>
        /// 마커 슬롯 생성 
        /// </summary>
        public void UpdateMarker()
        {
            ActiveGhostIcon(false);
            ClearMarkers();
            var _dataList = InventoryManager.Instance.GetMarkerList();
            //var _dataList = MarkerDataManager.Instance.GetAllHaveMakrerList();

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

        public void ActiveGhostIcon(bool _isActive)
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
            mapView.GhostIcon.style.backgroundImage =
                AddressablesManager.Instance.GetResource<Texture2D>(_markerSlot.MarkerData.spriteKey);
            mapView.GhostIcon.style.width =
                (int)(CurMarkerSprite.bounds.size.x * 200);
            mapView.GhostIcon.style.height =
                (int)(CurMarkerSprite.bounds.size.y * 200);
        }

        private VisualElement CreateMarker(Vector2 _pos)
        {
            if (curMarkerSlotPr is null) return null;

            return markersComponent.CreateMarker(_pos, mapView.MarkerParent, CurMarkerSprite);
        }

        private ItemData GetMarkerData(VisualElement _element)
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