using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Upgrade
{
    /// <summary>
    ///  이동, 확대 축소 
    /// </summary>
    public class ElementCtrlComponent 
    {
        private VisualElement target;
        private bool isCtrl;

        private float xMoveValue, yMoveValue, moveSpeed = 2000f, zoomValue, zoomSpeed = 1f;
        private float minZoomValue = 0.5f, maxZoomValue = 2f;
        // 프로퍼티 
        public bool IsCtrl
        {
            get => isCtrl;
            set => isCtrl = value; 
        }
        public ElementCtrlComponent(VisualElement _v)
        {
            this.target = _v; 
        }

        public void Update()
        {
            InputKey();
            Move();
            Zoom(); 
        }
        public void InputKey()
        {
            if(Input.GetMouseButton(1))
            {
                float _x = Input.GetAxis("Mouse X");
                float _y = Input.GetAxis("Mouse Y");

                if(Mathf.Abs(_x) > 0.2f)
                {
                    xMoveValue = Input.GetAxis("Mouse X") > 0 ? -1 : 1; // 마우스의 좌우 이동방향
                }
                else
                {
                    xMoveValue = 0f; 
                }
                if(Mathf.Abs(_y) > 0.2f)
                {
                    yMoveValue = Input.GetAxis("Mouse Y") > 0 ? -1 : 1; // 마우스의 상하 이동방향
                }
                else
                {
                    yMoveValue = 0f; 
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                xMoveValue = yMoveValue = 0; 
            }
                zoomValue = Input.GetAxis("Mouse ScrollWheel");

        }
        public void Move()
        {
            // 이동 
            Vector3 mapPos = target.transform.position;
            //Vector2 mapPos = new Vector2(mapView.Map.style.left.value.value, mapView.Map.style.top.value.value);

            float mapX, mapY;
            mapX = Mathf.Clamp(mapPos.x + -xMoveValue * (moveSpeed - target.transform.scale.x) * Time.deltaTime,
                                                // -mapView.Map Rect.width - width,width * 0.5f);
                                                -(target.contentRect.width /** mapScale.x*/) * 0.5f, (target.contentRect.width /** mapScale.x*/) * 0.5f);

            mapY = Mathf.Clamp(mapPos.y + yMoveValue * (moveSpeed - target.transform.scale.y) * Time.deltaTime,
                                                //  -mapView.MapRect.height - height,height * 0.5f);
                                                -(target.contentRect.height /** mapScale.y*/) * 0.5f, (target.contentRect.height /** mapScale.y*/) * 0.5f);

            this.target.transform.position = new Vector3(mapX, mapY, 0);
        }

        public void Zoom()
        {
            Vector3 mapScale = target.transform.scale;

            /// 확대 축소 

            // 맵 확대 축소 
            float scaleX, scaleY;
            scaleX = Mathf.Clamp(mapScale.x + (zoomValue * mapScale.x) * zoomSpeed, minZoomValue, maxZoomValue);
            scaleY = Mathf.Clamp(mapScale.y + (zoomValue * mapScale.y) * zoomSpeed, minZoomValue, maxZoomValue);
            target.transform.scale = new Vector3(scaleX, scaleY, mapScale.z);

            // 피벗 설정
            target.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(
                new Length((target.contentRect.width * 0.5f - target.transform.position.x), LengthUnit.Pixel),
                new Length((target.contentRect.height * 0.5f - target.transform.position.y), LengthUnit.Pixel)));

        }

    }

}
