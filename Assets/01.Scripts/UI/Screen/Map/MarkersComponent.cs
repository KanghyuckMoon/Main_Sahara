using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace UI
{
    [Serializable]
    /// <summary>
    /// 마커들 관리 
    /// </summary>
    public class MarkersComponent
    {
        [SerializeField]
        private VisualTreeAsset markerUxml;
        [SerializeField]
        private int width, height; 
        private List<VisualElement> markerList = new List<VisualElement>();

        private const string deleteMarkerStr = "delete_marker";

        /// <summary>
        /// 마커 생성 
        /// </summary>
        public VisualElement CreateMarker(Vector2 _pos, VisualElement _parent, Sprite _marker) // 위치, 부모 요소 
        {
            TemplateContainer marker = markerUxml.Instantiate();
            marker.style.position = Position.Absolute;
            _parent.Add(marker);

            width = (int)(_marker.bounds.size.x * 400);
            height = (int)(_marker.bounds.size.y * 400);
            
            marker.ElementAt(0).style.width = new StyleLength(width);
            marker.ElementAt(0).style.height = new StyleLength(height);
            marker.ElementAt(0).style.backgroundImage = new StyleBackground(_marker);

            _pos += new Vector2(-width / 2, -height / 2);
            marker.contentContainer.transform.position = _pos;
            markerList.Add(marker);

            return marker;
        }

        /// <summary>
        /// 전체 마커 활성화 비활성화 
        /// </summary>
        /// <param name="_isActive"></param>
        public void ActiveMarkers(bool _isActive)
        {
            DisplayStyle _display = _isActive == true ? DisplayStyle.Flex : DisplayStyle.None; 

            for (int i =0; i< markerList.Count; i++)
            {
                markerList[i].style.display = _display;
            }
        }
    }

}
