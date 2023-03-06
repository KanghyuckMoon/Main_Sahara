using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Utill.Measurement; 
namespace UI
{
    public enum MapType
    {
        FullMap,
        MiniMap
    }
    [Serializable]
    public class MapView : AbUI_Base
    {
        [SerializeField]
        private MapType mapType = MapType.MiniMap;
        [SerializeField]
        private Vector2 mapSize = new Vector2(8000, 8000);
        private VisualElement map; // �� �̹��� element
        private VisualElement centerMarker; // ��� ��ũ 
        private VisualElement markerParent; // ��Ŀ �θ� ��� 
        private VisualElement minimapMask;// �̴ϸ� ����ũ ��� 

        int width, height; 
        
        // ������Ƽ 
        public VisualElement Map => map;
        public ITransform MapTrm => map.transform; 
        public Rect MapRect => map.contentRect;
        public VisualElement CenterMark => centerMarker;
        public MapType CurMapType => mapType;
        public VisualElement MarkerParent => markerParent;
        public VisualElement MinimapMask => minimapMask;
        public float MinimapMaskW => minimapMask.contentRect.width;
        public float MinimapMaskH => minimapMask.contentRect.height;

        enum Elements
        {
            full_map_panel, 
            map_panel,
            map,
            center_marker,
            minimap_mask,
            markers,

        }

        enum Buttons
        {

        }

        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));

            map = GetVisualElement((int)Elements.map);
            centerMarker = GetVisualElement((int)Elements.center_marker);
            markerParent = GetVisualElement((int)Elements.markers);
            minimapMask = GetVisualElement((int)Elements.minimap_mask); 
        }

        public override void Init()
        {
            base.Init();
            AddButtonEvents();
            width = Screen.width;
            height = Screen.height;
        //    SeMapUISize(); 
        }

        public void SetMapUISize(Vector2 _mapSize)
        {
//            map.style.width = width * 2;
//            map.style.height = height * 4;

            map.style.width = _mapSize.x;
            map.style.height = _mapSize.y;

            //  map.contentRect = new Rect(map.contentRect.x, map.contentRect.y, Screen.width * 2, Screen.width * 4);
        }
        private void AddButtonEvents()
        {

        }

        /// <summary>
        /// ��ü�� Ȱ��ȭ�� true, �̴ϸ� Ȱ��ȭ�� false 
        /// </summary>
        /// <returns></returns>
        public bool ShowMap()
        {
            if(mapType == MapType.MiniMap)
            {
                mapType = MapType.FullMap; 
                ShowFullMap();
                return true;
            }
            mapType = MapType.MiniMap;
            //testMap.Test(() => ShowMiniMap(), () => SetMask(true)); 
           ShowMiniMap();
            return false;
        }

        public void ShowMap(bool _isAcitve)
        {
            if(_isAcitve == true)
            {
                mapType = MapType.FullMap;
                ShowFullMap();
                return; 
            }
            mapType = MapType.MiniMap;
            ShowMiniMap();
            SetMask(true); 
        }

        private void ShowFullMap()
        {
            // ��ü�� UI Ȱ��ȭ�ϰ� 
            ShowVisualElement(GetVisualElement((int)Elements.full_map_panel),true); 
            // �Է� ����ϰ� 
            
            // �г� ����
            VisualElement _panel = GetVisualElement((int)Elements.map_panel);
            _panel.RemoveFromClassList("minimap_panel");
            _panel.AddToClassList("map_panel");

            // �� ������ ���� 
            //map.RemoveFromClassList("minimap");
            //map.AddToClassList("map");

            // �̴ϸ� ����ũ ���� 
            VisualElement _minimapMask = GetVisualElement((int)Elements.minimap_mask);
            ShowVisualElement(_minimapMask, false);

            _panel.Add(map);
        }

        private void ShowMiniMap()
        {
            // ��ü�� UI ��Ȱ��ȭ�ϰ� 

            // �Է� �����ϰ� 

            // �г� ����


            // �� ������ ���� 
            //map.RemoveFromClassList("map");  
            //map.AddToClassList("minimap");    

            // �ʱ�ȭ 
            map.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(new Length(50, LengthUnit.Percent), new Length(50, LengthUnit.Percent)));
            MapTrm.scale = Vector3.one;
            MapTrm.position = Vector3.zero;
            MarkerParent.transform.scale = Vector3.one;
            var _markers = MarkerParent.Children();
            foreach (var _marker in _markers)
            {
                _marker.transform.scale = Vector2.one; 
            }
            SetMapPanel(_isMinimap: true); 
            // ����ũ Ű��
            //SetMask(true); 
            //SetMask(true); 
         }

        public void SetMask(bool _isActive)
        {
            VisualElement _minimapMask = GetVisualElement((int)Elements.minimap_mask);
            _minimapMask.Add(map);

            ShowVisualElement(_minimapMask, _isActive);
            ShowVisualElement(GetVisualElement((int)Elements.map_panel), true); 
        }

        private void SetMapPanel(bool _isMinimap)
        {
            VisualElement _panel = GetVisualElement((int)Elements.map_panel);
            ShowVisualElement(_panel, false);
            if (_isMinimap == true)
            {
                // ��ü�� UI ��Ȱ��ȭ�ϰ� 
                ShowVisualElement(GetVisualElement((int)Elements.full_map_panel), ! _isMinimap); 
                _panel.RemoveFromClassList("map_panel");
                _panel.AddToClassList("minimap_panel");
                return; 
            }

            // ��ü�� UI Ȱ��ȭ�ϰ� 
            ShowVisualElement(GetVisualElement((int)Elements.full_map_panel), _isMinimap); 
            _panel.AddToClassList("map_panel");
            _panel.RemoveFromClassList("minimap_panel");
        }

        public void Test()
        {
            Logging.Log("Overflow Hidden");
            GetVisualElement((int)Elements.minimap_mask).style.overflow = Overflow.Hidden;
        }
        public void Test2()
        {
            Logging.Log("Overflow Visible");
            GetVisualElement((int)Elements.minimap_mask).style.overflow = Overflow.Visible;

        }
    }
}


