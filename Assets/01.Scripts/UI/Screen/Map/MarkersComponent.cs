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

        private List<VisualElement> markerList = new List<VisualElement>();

        /// <summary>
        /// 마커 생성 
        /// </summary>
        public void CreateMarker(Vector2 _pos, VisualElement _parent) // 위치, 부모 요소 
        {
            TemplateContainer marker = markerUxml.Instantiate();
            _parent.Add(marker);
            //marker.contentContainer.transform.position = new Vector3(-mapView.Map.transform.position.x /*+ mapView.Map.style.width.value.value * 0.5f*/ - 35,// (-marker.contentContainer.style.width.value.value *0.5f),
            //                                                                  -mapView.Map.transform.position.y/* + mapView.Map.style.width.value.value * 0.5f*/ - 35,//(-marker.contentContainer.transform.position.y * 0.5f),
            //                                                                  0);
            float _w = marker.ElementAt(0).style.width.value.value;
            float _h = marker.ElementAt(0).style.height.value.value;
            Debug.Log("RECT" + _w + " ," + _h);
            _pos += new Vector2(_w / 2, _h / 2);
            marker.contentContainer.transform.position = _pos;
            markerList.Add(marker);
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
