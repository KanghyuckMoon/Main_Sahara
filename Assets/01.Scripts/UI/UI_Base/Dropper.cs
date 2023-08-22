using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System; 

namespace UI.Base
{
    public class Dropper : MouseManipulator
    {

        private Vector2 _startPos;
        private bool _isDragging = false;

        private Action<Vector2> DropCallback;

        public Dropper(Action<Vector2> DropCallback = null)
        {
            _isDragging = false;
            activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
            this.DropCallback = DropCallback;
        }

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<MouseMoveEvent>(OnMouseMove);
            target.RegisterCallback<MouseUpEvent>(OnMouseUp);
           // target.RegisterCallback<MouseCaptureEvent>(OnMouseUp);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
            target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
        }

        protected void OnMouseMove(MouseMoveEvent e)
        {
            Debug.Log("Move2");
            if (/*_isDragging &&*/ target.HasMouseCapture())
            {
                Debug.Log("Move");
                Vector2 diff = e.localMousePosition - _startPos; // 변화값 

                //layout은 부모로부터의 상대적인 위치를 말한다.기본은 픽셀이니까 걍 넣어도 되긴해 new Length 없이
                 //target.style.top = new Length(target.layout.y + diff.y, LengthUnit.Pixel);
                //target.style.left = new Length(target.layout.x + diff.x, LengthUnit.Pixel);

                target.style.top = new Length(e.mousePosition.y - target.resolvedStyle.height / 2, LengthUnit.Pixel);
                target.style.left = new Length(e.mousePosition.x - target.resolvedStyle.width / 2, LengthUnit.Pixel);
            }
        }

        protected void OnMouseUp(MouseUpEvent e)
        {
            Debug.Log("up");
            if (/*!_isDragging ||*/ !target.HasMouseCapture())
            {
                return;
            }

            

            _isDragging = false;
            target.ReleaseMouse(); //터치라면 릴리즈 포인터
            Debug.Log($"{target.name} : 로컬 {e.localMousePosition}, 월드 : {e.mousePosition}, 오리진 : {e.originalMousePosition}");

            // 현재 마우스 포인터 위치가 슬롯 바운더리 안에 있는지 체크 
            // 있다면, 슬롯이 인벤토리인지, 장착슬롯인지 체크 
            // 인벤토리 슬롯이면 현재 슬롯 초기화, 위치한 슬롯에 데이터 넣기 
            // 장착이면 그 슬롯 가져와서 현재 데이터 넣어주기 
            // 
            // 없다면, 끝 
            // 현재 드래그 한거 비활성화
            DropCallback?.Invoke(e.mousePosition);
        }
    }

}
