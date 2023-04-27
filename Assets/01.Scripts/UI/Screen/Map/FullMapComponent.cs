    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using PathMode;
using UI.EventManage;
using UI.MapLiner;
using InputSystem;
using UI.Base; 
using Utill.Pattern;
using Utill.Addressable;
using Pool;
using UI.Map;

namespace UI
{
    [Serializable]
    public class FullMapComponent
    {
        private MapView mapView;

        // 인스펙터 참조 
        [SerializeField]
        private MarkersComponent markersComponent;

        [SerializeField]
        private float moveSpeed;

        private float zoomSpeed = 1f;
        [SerializeField]
        private float maxZoomValue;
        [SerializeField]
        private float minZoomValue;

        // 프라이빗 
        private MapLiner.MapLiner mapLiner; 

        private float zoomValue;
        private float xMoveValue;
        private float yMoveValue;

        private Sprite selectMarker;

        private ElementCtrlComponent elementCtrlComponent; // 움직임 확대 축소 
        private MarkerSetComp markerSetComp; 
          
        // 프로퍼티 
        private Vector2 MoveDir => new Vector2(xMoveValue, yMoveValue).normalized;
        public MarkersComponent MarkersComponent => markersComponent;
        
        private ElementCtrlComponent ElementCtrlComponent
        {
            get
            {
                if (elementCtrlComponent == null)
                {
                    elementCtrlComponent = new ElementCtrlComponent(mapView.Map);
                }

                return elementCtrlComponent;
            }
        }

        public void Init(MapView _mapView)
        {
            this.mapView = _mapView;
            this.mapLiner = LineCreateManager.Instance.CreateLine(ScreenType.Map);

            this.elementCtrlComponent = new ElementCtrlComponent(mapView.Map); 
            elementCtrlComponent.SetZoomValue(_minV:0.1f, _maxV: 4f);

            this.markerSetComp = new MarkerSetComp(mapView.MarkerSlotParent,mapView,markersComponent); 
            markerSetComp.UpdateMarker();
        }

        public void UpdateUI()
        {
            ElementCtrlComponent.Update();
            markerSetComp.Update();
            /*
            // 전체맵일 때만 입력으로 이동 확대 
            KeyInput();
            // 이동 
            MoveMap();
            // 확대 축소 
            ZoomMap();
            */
            //mapView.MapRect.width 
            LineCreateManager.Instance.UpdateLinesPos(ScreenType.Map, (Vector2)mapView.Map.transform.position); 
            LineCreateManager.Instance.UpdateLinesScale(ScreenType.Map, (Vector2)mapView.Map.transform.scale); 
          
            //EventManager.Instance.TriggerEvent(EventsType.UpdateMapPos, (Vector2)mapView.Map.transform.position);
           // EventManager.Instance.TriggerEvent(EventsType.UpdateMapScale, (Vector2)mapView.Map.transform.scale);
            // 마커 생성

            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                selectMarker = AddressablesManager.Instance.GetResource<Sprite>("Marker1");
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                selectMarker = AddressablesManager.Instance.GetResource<Sprite>("Marker2");
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                selectMarker = AddressablesManager.Instance.GetResource<Sprite>("Marker3");
            }
            
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (selectMarker is null)
                {
                    return;
                }
                markersComponent.CreateMarker(new Vector2(-mapView.Map.transform.position.x,
                    -mapView.Map.transform.position.y), mapView.MarkerParent, selectMarker);
            }
        }
        /// <summary>
        /// 발자국 활성화 
        /// </summary>
        public void ActivePath()
        {
            var _list = PathModeManager.Instance.GetPathList();
            //EventManager.Instance.TriggerEvent(EventsType.UpdateMapLine,_list);

            this.mapLiner.UpdateMapLine(_list); 
        }

        public void ClearLines()
        {
            //EventManager.Instance.TriggerEvent(EventsType.ClearMapLine); 
            this.mapLiner.ClearMapLine(); 
        }


    }

}

 #region  키보드 입력

           /*
           private void KeyInput()
        {
            
            // 움직임 
            if (InputManager.Instance.CheckKey("MapMoveF_Down"))
            {
                yMoveValue = 1f;
            }
            if (InputManager.Instance.CheckKey("MapMoveL_Down"))
            {
                xMoveValue = -1f;
            }
            if (InputManager.Instance.CheckKey("MapMoveB_Down"))
            {
                yMoveValue = -1f;
            }
            if (InputManager.Instance.CheckKey("MapMoveR_Down"))
            {
                xMoveValue = 1f;
            }

            // 키 뗐을때 초기화 
            if (InputManager.Instance.CheckKey("MapMoveF_Up") || InputManager.Instance.CheckKey("MapMoveB_Up"))
            {
                yMoveValue = 0f;
            }
            if (InputManager.Instance.CheckKey("MapMoveL_Up") || InputManager.Instance.CheckKey("MapMoveR_Up"))
            {
                xMoveValue = 0f;
            }

            // 확대 축소 
            zoomValue = Input.GetAxis("Mouse ScrollWheel");
        }

        /// <summary>
        /// 맵 이동
        /// </summary>
        private void MoveMap()
        {
            // 이동 
            Vector3 mapPos = mapView.Map.transform.position;
            //Vector2 mapPos = new Vector2(mapView.Map.style.left.value.value, mapView.Map.style.top.value.value);

            float mapX, mapY;
            mapX = Mathf.Clamp(mapPos.x + -MoveDir.x * (moveSpeed / mapView.MapTrm.scale.x) * Time.deltaTime,
                                                // -mapView.MapRect.width - width,width * 0.5f);
                                                -(mapView.MapRect.width /** mapScale.x#1#) * 0.5f, (mapView.MapRect.width /** mapScale.x#1#) * 0.5f);

            mapY = Mathf.Clamp(mapPos.y + MoveDir.y * (moveSpeed / mapView.MapTrm.scale.y) * Time.deltaTime,
                                                //  -mapView.MapRect.height - height,height * 0.5f);
                                                -(mapView.MapRect.height /** mapScale.y#1#) * 0.5f, (mapView.MapRect.height /** mapScale.y#1#) * 0.5f);
            // 왜 스케일 안 곱해야 하는거지? 

            //mapView.Map.style.left = mapX;
            //mapView.Map.style.top = mapY; 

            mapView.Map.transform.position = new Vector3(mapX, mapY, 0);
        }

        /// <summary>
        /// 맵 확대 축소 
        /// </summary>
        private void ZoomMap()
        {
            Vector3 mapScale = mapView.MapTrm.scale;

            /// 확대 축소 

            // 맵 확대 축소 
            float scaleX, scaleY;
            scaleX = Mathf.Clamp(mapScale.x + (zoomValue * mapScale.x) * zoomSpeed, minZoomValue, maxZoomValue);
            scaleY = Mathf.Clamp(mapScale.y + (zoomValue * mapScale.y) * zoomSpeed, minZoomValue, maxZoomValue);
            mapView.MapTrm.scale = new Vector3(scaleX, scaleY, mapScale.z);

            // 마커들 확대 축소 
            // mapView.MarkerParent.transform.scale = new Vector2(1 / scaleX, 1 / scaleY);
            //var _markers = mapView.MarkerParent.Children();
            //foreach (var _marker in _markers)
            //{
            //    _marker.transform.scale = new Vector2(1 / scaleX, 1 / scaleY);
            //}
            // 마커들 피벗 설정 
            //mapView.MarkerParent.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(
            //   new Length((mapView.MapRect.width * 0.5f - mapView.MapTrm.position.x), LengthUnit.Pixel),
            //   new Length((mapView.MapRect.height * 0.5f - mapView.MapTrm.position.y), LengthUnit.Pixel)));

            // 피벗 설정
            mapView.Map.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(
                new Length((mapView.MapRect.width * 0.5f - mapView.MapTrm.position.x), LengthUnit.Pixel),
                new Length((mapView.MapRect.height * 0.5f - mapView.MapTrm.position.y), LengthUnit.Pixel)));

            // 중심점 따라가도록 
            //float diffX = mapView.CenterMark.worldBound.x - mapView.Map.worldBound.x;
            //float diffY = mapView.CenterMark.worldBound.y - mapView.Map.worldBound.y;
            //mapView
        }
        */

        #endregion