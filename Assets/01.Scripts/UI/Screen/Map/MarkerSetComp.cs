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

        // ���� Ȱ��ȭ ���� ���� 
        private MarkerSlotPr curMarkerSlotPr = null;
        private VisualElement deleteTarget = null;
        private MarkersComponent markersComponent;

        private bool isMapOver = false; // �� ���� Ŀ���� �ִ°� 
        private bool isInputCtrl = false; // ctrl ������ �ִ� ���ΰ� 
        
        private const string selectStr = "active_select"; 
        private const string deleteMarkerStr = "delete_select"; 
        
        // ������Ƽ 
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
                if (x.button is not 0 || isInputCtrl is true) return; 
                markerList.Add(CreateMarker((Vector2)x.localPosition -
                                       new Vector2(mapView.Map.resolvedStyle.width / 2, mapView.Map.resolvedStyle.height / 2)));
                // ��Ŀ ���� 0 �ΰ� 
                bool _isZeroCount = MarkerDataManager.Instance.RemoveHaveMarker(curMarkerSlotPr.MarkerData.key);
                if (_isZeroCount is true)
                {
                    ActiveGhostIcon(false);
                    curMarkerSlotPr = null; 
                }
                UpdateMarker(); //��Ŀ ui ������Ʈ 
            });
            
            mapView.MarkerSetPanel.RegisterCallback<MouseOverEvent>((x) => { mapView.ActiveGhostIcon(false);}); 
            mapView.MarkerSetPanel.RegisterCallback<PointerOutEvent>((x) => { mapView.ActiveGhostIcon(true);}); 
            
        }

        /// <summary>
        /// ��Ŀ 
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
            
            // ���Կ� ��� �ߴٸ�
            if (_slots.Count() != 0)
            {
                // ���� ������ ����� ���� 
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
                // ���콺 ���󰡱� 
                mapView.GhostIcon.transform.position = _pos;
                mapView.GhostIcon.transform.scale = mapView.Map.parent.transform.scale; 
                Debug.Log("Folllow@@@");
            }
        }
        /// <summary>
        /// ��Ŀ ���� ���� 
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
        ///  ���� �ִ� ��Ŀ�� ���� 
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

            // ��Ʈ������ Ȱ��ȭ 
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

    }    
}

