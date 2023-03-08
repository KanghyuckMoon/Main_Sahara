using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using PathMode;
using UI.EventManage; 

namespace UI
{
    [Serializable]
    public class FullMapComponent
    {
        private MapView mapView;

        // �ν����� ���� 
        [SerializeField]
        private MarkersComponent markersComponent;

        [SerializeField]
        private float moveSpeed;

        private float zoomSpeed = 1f;
        [SerializeField]
        private float maxZoomValue;
        [SerializeField]
        private float minZoomValue;

        // �����̺� 
        private float zoomValue;
        private float xMoveValue;
        private float yMoveValue;

        // ������Ƽ 
        private Vector2 MoveDir => new Vector2(xMoveValue, yMoveValue).normalized;
        public MarkersComponent MarkersComponent => markersComponent;

        public void Init(MapView _mapView)
        {
            this.mapView = _mapView;
        }

        public void UpdateUI()
        {
            // ��ü���� ���� �Է����� �̵� Ȯ�� 
            KeyInput();
            // �̵� 
            MoveMap();
            // Ȯ�� ��� 
            ZoomMap();
            //mapView.MapRect.width 
            EventManager.Instance.TriggerEvent(EventsType.UpdateMapPos, (Vector2)mapView.Map.transform.position);
            EventManager.Instance.TriggerEvent(EventsType.UpdateMapScale, (Vector2)mapView.Map.transform.scale);
            // ��Ŀ ����
            if (Input.GetKeyDown(KeyCode.G))
            {
                markersComponent.CreateMarker(new Vector2(-mapView.Map.transform.position.x, -mapView.Map.transform.position.y), mapView.MarkerParent);
            }
        }

        private void KeyInput()
        {
            // ������ 
            if (Input.GetKey(KeyCode.W))
            {
                yMoveValue = 1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                xMoveValue = -1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                yMoveValue = -1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                xMoveValue = 1f;
            }

            // Ű ������ �ʱ�ȭ 
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                yMoveValue = 0f;
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                xMoveValue = 0f;
            }

            // Ȯ�� ��� 
            zoomValue = Input.GetAxis("Mouse ScrollWheel");
        }

        /// <summary>
        /// �� �̵�
        /// </summary>
        private void MoveMap()
        {
            // �̵� 
            Vector3 mapPos = mapView.Map.transform.position;
            //Vector2 mapPos = new Vector2(mapView.Map.style.left.value.value, mapView.Map.style.top.value.value);

            float mapX, mapY;
            mapX = Mathf.Clamp(mapPos.x + -MoveDir.x * (moveSpeed / mapView.MapTrm.scale.x) * Time.deltaTime,
                                                // -mapView.MapRect.width - width,width * 0.5f);
                                                -(mapView.MapRect.width /** mapScale.x*/) * 0.5f, (mapView.MapRect.width /** mapScale.x*/) * 0.5f);

            mapY = Mathf.Clamp(mapPos.y + MoveDir.y * (moveSpeed / mapView.MapTrm.scale.y) * Time.deltaTime,
                                                //  -mapView.MapRect.height - height,height * 0.5f);
                                                -(mapView.MapRect.height /** mapScale.y*/) * 0.5f, (mapView.MapRect.height /** mapScale.y*/) * 0.5f);
            // �� ������ �� ���ؾ� �ϴ°���? 

            //mapView.Map.style.left = mapX;
            //mapView.Map.style.top = mapY; 

            mapView.Map.transform.position = new Vector3(mapX, mapY, 0);
        }

        /// <summary>
        /// �� Ȯ�� ��� 
        /// </summary>
        private void ZoomMap()
        {
            Vector3 mapScale = mapView.MapTrm.scale;

            /// Ȯ�� ��� 

            // �� Ȯ�� ��� 
            float scaleX, scaleY;
            scaleX = Mathf.Clamp(mapScale.x + (zoomValue * mapScale.x) * zoomSpeed, minZoomValue, maxZoomValue);
            scaleY = Mathf.Clamp(mapScale.y + (zoomValue * mapScale.y) * zoomSpeed, minZoomValue, maxZoomValue);
            mapView.MapTrm.scale = new Vector3(scaleX, scaleY, mapScale.z);

            // ��Ŀ�� Ȯ�� ��� 
            // mapView.MarkerParent.transform.scale = new Vector2(1 / scaleX, 1 / scaleY);
            var _markers = mapView.MarkerParent.Children();
            foreach (var _marker in _markers)
            {
                _marker.transform.scale = new Vector2(1 / scaleX, 1 / scaleY);
            }
            // ��Ŀ�� �ǹ� ���� 
            //mapView.MarkerParent.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(
            //   new Length((mapView.MapRect.width * 0.5f - mapView.MapTrm.position.x), LengthUnit.Pixel),
            //   new Length((mapView.MapRect.height * 0.5f - mapView.MapTrm.position.y), LengthUnit.Pixel)));

            // �ǹ� ����
            mapView.Map.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(
                new Length((mapView.MapRect.width * 0.5f - mapView.MapTrm.position.x), LengthUnit.Pixel),
                new Length((mapView.MapRect.height * 0.5f - mapView.MapTrm.position.y), LengthUnit.Pixel)));

            // �߽��� ���󰡵��� 
            //float diffX = mapView.CenterMark.worldBound.x - mapView.Map.worldBound.x;
            //float diffY = mapView.CenterMark.worldBound.y - mapView.Map.worldBound.y;
            //mapView
        }

        /// <summary>
        /// ���ڱ� Ȱ��ȭ 
        /// </summary>
        public void ActivePath()
        {
            var _list = PathModeManager.Instance.GetPathList();
            EventManager.Instance.TriggerEvent(EventsType.UpdateMapLine,_list);
        }

        public void ClearLines()
        {
            EventManager.Instance.TriggerEvent(EventsType.ClearMapLine); 
        }


    }

}

