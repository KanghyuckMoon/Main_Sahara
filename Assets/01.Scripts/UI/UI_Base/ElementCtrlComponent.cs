using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening; 

namespace UI.Base
{
    /// <summary>
    ///  이동, 확대 축소 
    /// </summary>
    public class ElementCtrlComponent
    {
        private VisualElement target;
        private bool isCtrl;
        private float xMoveValue, yMoveValue, moveSpeed = 600f, zoomValue, zoomSpeed = 20f;
        private float minZoomValue = 1f, maxZoomValue = 2f;

        private bool isInput = true;
        private bool isLimit = true; 
        
        private Vector2 realMoveTargetV, realScaleTargetV;

        // 프로퍼티 

        public bool IsLimit
        {
            get => isLimit;
            set => isLimit = value; 
        }
        public bool IsCtrl
        {
            get => isCtrl;
            set => isCtrl = value;
        }

        public bool IsInput
        {
            get => isInput;
            set => isInput = value;
        }

        private Vector2 TargetV
        {
            get => target.transform.position;
            set => target.transform.position = value; 
        }

        public ElementCtrlComponent(VisualElement _v)
        {
            this.target = _v;
        }

        public void Update()
        {
            //if (IsInput == false) return; 
            InputKey();
            //Move();
            RealMove();
            Zoom();

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                this.target.transform.position = realMoveTargetV; 
            }
        }

        private Vector2 startPos, nextPos, movePos;

        public void InputKey()
        {
            #region 키보드 움직임

            /*
                          //if(Input.GetMouseButton(1))
                          //{
                          //    float _x = Input.GetAxis("Mouse X");
                          //    float _y = Input.GetAxis("Mouse Y");
              
                          //    if(Mathf.Abs(_x) > 0.01f)
                          //    {
                          //        xMoveValue = Input.GetAxis("Mouse X") > 0 ? -1 : 1; // 마우스의 좌우 이동방향
                          //    }
                          //    else
                          //    {
                          //        xMoveValue = 0f; 
                          //    }
                          //    if(Mathf.Abs(_y) > 0.01f)
                          //    {
                          //        yMoveValue = Input.GetAxis("Mouse Y") > 0 ? -1 : 1; // 마우스의 상하 이동방향
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

            #endregion

            if (Input.GetMouseButtonDown(1))
            {
                startPos = Input.mousePosition;
            }

            if (Input.GetMouseButton(1))
            {
                movePos = (Vector2)Input.mousePosition - startPos;
                startPos = Input.mousePosition;
                // 이동 
                Move(new Vector2(movePos.x, -movePos.y));
                movePos = Vector2.zero;
            }

            if (Input.GetMouseButtonUp(1))
            {
                CheckLimit();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResetPosAndZoom();
            }

            zoomValue = Input.GetAxis("Mouse ScrollWheel");
        }

        
        public void SetZoomValue(float _minV, float _maxV)
        {
            this.minZoomValue = _minV;
            this.maxZoomValue = _maxV; 
        }
        
        /// <summary>
        /// 포지션, 줌 값 초기화 
        /// </summary>
        public void ResetPosAndZoom()
        {
            //this.target.transform.position = Vector2.zero;
            realMoveTargetV = Vector2.zero; 
            target.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(
                new Length(50f, LengthUnit.Percent),
                new Length(50f, LengthUnit.Percent)));
            target.parent.transform.scale = Vector2.one;
        }

        public void StopTween()
        {
            DOTween.KillAll();
        }
        /// <summary>
        /// 닷트윈을 통한 움직임 (현재 포지션 + _pos) 
        /// </summary>
        public void TweenMove(Vector2 _pos)
        {
            Vector2 _targetVec = (Vector2)target.transform.position + _pos; 
            DOTween.To(() => TargetV.x, (x) => TargetV =new Vector2(x,TargetV.y) , _targetVec.x, 0.5f);
            DOTween.To(() => TargetV.y, (y) => TargetV =new Vector2(TargetV.x,y) , _targetVec.y, 0.5f);
        }

        public void SetPos(Vector2 _pos)
        {
            target.transform.position = _pos; 
        }

        private void Move(Vector2 _value)
        {
            // 이동 
            Vector3 mapPos = target.transform.position;

            float distX, distY; // 움직일 거리 
            distX = _value.x  / target.parent.transform.scale.x;
            distY = _value.y  /  target.parent.transform.scale.y;

            float _targetX, _targetY; // 현재 포지션 + 움직이 거리]
            //  float _limitX = Mathf.Clamp(target.contentRect.width * target.transform.scale.x   - Screen.width,0,float.MaxValue); // 화면 크기 보다 대장장이 창이 크면 조작 가능  
            // float _limitY = Mathf.Clamp(target.contentRect.height * target.transform.scale.y - Screen.height,0,float.MaxValue); 
            float _limitX = target.contentRect.width * target.parent.transform.scale.x; // 화면 크기 보다 대장장이 창이 크면 조작 가능  
            float _limitY = target.contentRect.height * target.parent.transform.scale.y;
            _targetX = Mathf.Clamp(distX + mapPos.x, -_limitX * 0.5f, _limitX * 0.5f);
            _targetY = Mathf.Clamp(distY + mapPos.y, -_limitY * 0.5f, _limitY * 0.5f);

            //   this.target.transform.position = new Vector3(_targetX, _targetY, 0);
            realMoveTargetV = new Vector2(distX + realMoveTargetV.x, distY + realMoveTargetV.y);
            // this.target.transform.position = new Vector3(distX + mapPos.x, distY + mapPos.y, 0);
        }


        /// <summary>
        /// 실질적 움직임 
        /// </summary>
        private void RealMove()
        {
            this.target.transform.position = Vector2.Lerp(target.transform.position, realMoveTargetV, 
                Time.deltaTime * 20f * target.transform.scale.x);
            /*this.target.style.left = realTargetV.x; 
            this.target.style.bottom = realTargetV.y;*/ 
        }

        private void CheckLimit()
        {
            if (IsLimit == false) return; 
            float _limitX = target.contentRect.width * target.parent.transform.scale.x * 2; // 화면 크기 보다 대장장이 창이 크면 조작 가능  
            float _limitY = target.contentRect.height * target.parent.transform.scale.y * 2;

            float _targetX = Mathf.Clamp(TargetV.x, -_limitX, _limitX);
            float _targetY = Mathf.Clamp(TargetV.y, -_limitY, _limitY* 0.5f);

            realMoveTargetV = new Vector2(_targetX, _targetY);
            //   this.target.transform.position = new Vector3(_targetX, _targetY, 0);
        }


        public void Zoom()
        {
            Vector3 curScale = target.parent.transform.scale;

            /// 확대 축소 

            // 맵 확대 축소 
            float scaleX, scaleY;
            scaleX = Mathf.Clamp(curScale.x + (zoomValue * curScale.x) * zoomSpeed * Time.deltaTime, minZoomValue, maxZoomValue);
            scaleY = Mathf.Clamp(curScale.y + (zoomValue * curScale.y) * zoomSpeed* Time.deltaTime, minZoomValue, maxZoomValue);
            target.parent.transform.scale = new Vector3(scaleX, scaleY, curScale.z);

            realScaleTargetV = new Vector2(scaleX, scaleY);
            
            this. target.parent.transform.scale = Vector2.Lerp( target.parent.transform.scale, realScaleTargetV, 
                Time.deltaTime * 20f);
            // 피벗 설정
            /*target.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(
                new Length((target.conten   tRect.width * 0.5f - target.transform.position.x), LengthUnit.Pixel),
                new Length((target.contentRect.height * 0.5f - target.transform.position.y), LengthUnit.Pixel)));*/
        }
        
        public void Move()
        {
            // 이동 
            Vector3 mapPos = target.transform.position;
            //Vector2 mapPos = new Vector2(mapView.Map.style.left.value.value, mapView.Map.style.top.value.value);

            float mapX, mapY;
            mapX = Mathf.Clamp(mapPos.x + -xMoveValue * (moveSpeed / target.transform.scale.x) * Time.deltaTime,
                // -mapView.Map Rect.width - width,width * 0.5f);
                -(target.contentRect.width /** mapScale.x*/) * 0.5f,
                (target.contentRect.width /** mapScale.x*/) * 0.5f);

            mapY = Mathf.Clamp(mapPos.y + yMoveValue * (moveSpeed / 2 / target.transform.scale.y) * Time.deltaTime,
                //  -mapView.MapRect.height - height,height * 0.5f);
                -(target.contentRect.height /** mapScale.y*/) * 0.5f,
                (target.contentRect.height /** mapScale.y*/) * 0.5f);

            this.target.transform.position = new Vector3(mapX, mapY, 0);
        }

    }
}