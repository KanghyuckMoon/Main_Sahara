using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
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

        private int width, height;

        // ������Ƽ 
        private Vector2 MoveDir => new Vector2(xMoveValue, yMoveValue).normalized;
        public MarkersComponent MarkersComponent => markersComponent; 

        public void Init(MapView _mapView)
        {
            this.mapView = _mapView;
            width = Screen.width;
            height = Screen.height; 
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
            // ��Ŀ ����
            if(Input.GetKeyDown(KeyCode.G))
            {
                markersComponent.CreateMarker(new Vector2(-mapView.Map.transform.position.x - 35, -mapView.Map.transform.position.y -35),mapView.MarkerParent); 
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
            mapX = Mathf.Clamp(mapPos.x + -xMoveValue * (moveSpeed - mapView.MapTrm.scale.x) * Time.deltaTime,
                                               // -mapView.MapRect.width - width,width * 0.5f);
                                                -(mapView.MapRect.width /** mapScale.x*/) * 0.5f, (mapView.MapRect.width /** mapScale.x*/) * 0.5f);

            mapY = Mathf.Clamp(mapPos.y + yMoveValue * (moveSpeed - mapView.MapTrm.scale.y) * Time.deltaTime,
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
            mapView.MarkerParent.transform.scale = new Vector2(1 / scaleX, 1 / scaleY); 

            // �ǹ� ����
            mapView.Map.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(
                new Length((mapView.MapRect.width * 0.5f - mapView.MapTrm.position.x), LengthUnit.Pixel),
                new Length((mapView.MapRect.height * 0.5f - mapView.MapTrm.position.y), LengthUnit.Pixel)));

            // �߽��� ���󰡵��� 
            //float diffX = mapView.CenterMark.worldBound.x - mapView.Map.worldBound.x;
            //float diffY = mapView.CenterMark.worldBound.y - mapView.Map.worldBound.y;
            //mapView
        }



    }

}

