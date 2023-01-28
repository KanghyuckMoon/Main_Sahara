using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

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
            markers

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
            SeMapUISize(); 
        }

        public void SeMapUISize()
        {
//            map.style.width = width * 2;
//            map.style.height = height * 4;

            map.style.width = mapSize.x;
            map.style.height = mapSize.y;

            //  map.contentRect = new Rect(map.contentRect.x, map.contentRect.y, Screen.width * 2, Screen.width * 4);
        }
        private void AddButtonEvents()
        {

        }

        public void ShowMap()
        {
            if(mapType == MapType.MiniMap)
            {
                mapType = MapType.FullMap; 
                ShowFullMap();
                return; 
            }
            mapType = MapType.MiniMap; 
            ShowMiniMap();
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
            ShowVisualElement(GetVisualElement((int)Elements.full_map_panel), false);
            
            // �Է� �����ϰ� 

            // �г� ����
            VisualElement _panel = GetVisualElement((int)Elements.map_panel);
            _panel.RemoveFromClassList("map_panel");
            _panel.AddToClassList("minimap_panel");

            // �� ������ ���� 
            //map.RemoveFromClassList("map");  
            //map.AddToClassList("minimap");    

            // �ʱ�ȭ 
            map.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(new Length(50, LengthUnit.Percent),new Length(50, LengthUnit.Percent)));
            MapTrm.scale = Vector3.one;
            MapTrm.position = Vector3.zero; 

            // ����ũ Ű��
            VisualElement _minimapMask = GetVisualElement((int)Elements.minimap_mask);
            ShowVisualElement(_minimapMask, true);

            _minimapMask.Add(map);
        }
    }
}


