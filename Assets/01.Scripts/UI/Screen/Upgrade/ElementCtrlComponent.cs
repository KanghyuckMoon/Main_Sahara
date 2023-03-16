using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Upgrade
{
    /// <summary>
    ///  �̵�, Ȯ�� ��� 
    /// </summary>
    public class ElementCtrlComponent 
    {
        private VisualElement target;
        private bool isCtrl;
        private float xMoveValue, yMoveValue, moveSpeed = 600f, zoomValue, zoomSpeed = 1f;
        private float minZoomValue = 1f, maxZoomValue = 2f;
        // ������Ƽ 
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
       //     Move();
            Zoom(); 
        }

        private Vector2 startPos,nextPos,movePos; 
        public void InputKey()
        {
            /*
            //if(Input.GetMouseButton(1))
            //{
            //    float _x = Input.GetAxis("Mouse X");
            //    float _y = Input.GetAxis("Mouse Y");

            //    if(Mathf.Abs(_x) > 0.01f)
            //    {
            //        xMoveValue = Input.GetAxis("Mouse X") > 0 ? -1 : 1; // ���콺�� �¿� �̵�����
            //    }
            //    else
            //    {
            //        xMoveValue = 0f; 
            //    }
            //    if(Mathf.Abs(_y) > 0.01f)
            //    {
            //        yMoveValue = Input.GetAxis("Mouse Y") > 0 ? -1 : 1; // ���콺�� ���� �̵�����
            //    }
            //    else
            //    {
            //        yMoveValue = 0f; 
            //    }
            //}
            if (Input.GetMouseButtonUp(1))
            {
                xMoveValue = yMoveValue = 0; 
            }
            */

            if (Input.GetMouseButtonDown(1))
            {
                startPos = Input.mousePosition; 
            }
            if (Input.GetMouseButton(1))
            {
                movePos = (Vector2)Input.mousePosition - startPos;
                startPos = Input.mousePosition;
                // �̵� 
                Move(new Vector2(movePos.x,-movePos.y)); 
                movePos = Vector2.zero;
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                ResetPosAndZoom(); 
            }
            if(Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("Pos" + target.transform.position);
                Debug.Log("Origin X " + target.style.transformOrigin.value.x);
                Debug.Log("Origin Y " + target.style.transformOrigin.value.y);
                Debug.Log("Scale Y " + target.transform.scale);
            }
            zoomValue = Input.GetAxis("Mouse ScrollWheel");

        }

        /// <summary>
        /// ������, �� �� �ʱ�ȭ 
        /// </summary>
        public void ResetPosAndZoom()
        {
            this.target.transform.position = Vector2.zero;
            target.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(
                                new Length(50f, LengthUnit.Percent),
                                new Length(50f, LengthUnit.Percent)));
            target.transform.scale = Vector2.one; 

        }

        private void Move(Vector2 _value)
        {
            // �̵� 
            Vector3 mapPos = target.transform.position;

            float distX, distY; // ������ �Ÿ� 
            distX = _value.x/* / target.transform.scale.x*/;
            distY = _value.y/* / target.transform.scale.y*/;

            float _targetX, _targetY; // ���� ������ + ������ �Ÿ�]
            float _limitX = Mathf.Clamp(target.contentRect.width * target.transform.scale.x   - Screen.width,0,float.MaxValue); // ȭ�� ũ�� ���� �������� â�� ũ�� ���� ����  
            float _limitY = Mathf.Clamp(target.contentRect.height * target.transform.scale.y - Screen.height,0,float.MaxValue); 

            //_targetX = Mathf.Clamp(distX + mapPos.x, -_limitX * 0.5f, _limitX * 0.5f);
           // _targetY = Mathf.Clamp(distY + mapPos.y, -_limitY * 0.5f, _limitY * 0.5f);

         //   this.target.transform.position = new Vector3(_targetX, _targetY, 0);
            this.target.transform.position = new Vector3(mapPos.x + distX, mapPos.y + distY, 0);
        }
        public void Move()
        {
            // �̵� 
            Vector3 mapPos = target.transform.position;
            //Vector2 mapPos = new Vector2(mapView.Map.style.left.value.value, mapView.Map.style.top.value.value);

            float mapX, mapY;
            mapX = Mathf.Clamp(mapPos.x + -xMoveValue * (moveSpeed / target.transform.scale.x) * Time.deltaTime,
                                                // -mapView.Map Rect.width - width,width * 0.5f);
                                                -(target.contentRect.width /** mapScale.x*/) * 0.5f, (target.contentRect.width /** mapScale.x*/) * 0.5f);

            mapY = Mathf.Clamp(mapPos.y + yMoveValue * (moveSpeed/2 / target.transform.scale.y) * Time.deltaTime,
                                                //  -mapView.MapRect.height - height,height * 0.5f);
                                                -(target.contentRect.height /** mapScale.y*/) * 0.5f, (target.contentRect.height /** mapScale.y*/) * 0.5f);

            this.target.transform.position = new Vector3(mapX, mapY, 0);
        }

        public void Zoom()
        {
            Vector3 mapScale = target.transform.scale;

            /// Ȯ�� ��� 

            // �� Ȯ�� ��� 
            float scaleX, scaleY;
            scaleX = Mathf.Clamp(mapScale.x + (zoomValue * mapScale.x) * zoomSpeed, minZoomValue, maxZoomValue);
            scaleY = Mathf.Clamp(mapScale.y + (zoomValue * mapScale.y) * zoomSpeed, minZoomValue, maxZoomValue);
            target.transform.scale = new Vector3(scaleX, scaleY, mapScale.z);

            // �ǹ� ����
            target.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(
                new Length((target.contentRect.width * 0.5f - target.transform.position.x), LengthUnit.Pixel),
                new Length((target.contentRect.height * 0.5f - target.transform.position.y), LengthUnit.Pixel)));

        }

    }

}
