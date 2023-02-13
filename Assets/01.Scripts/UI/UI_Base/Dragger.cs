using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Base
{
    public class Dragger : MouseManipulator
    {
        private Vector2 _startPos;
        private bool _isDragging = false;
        private Vector2 _originalPos;

        //        private Action<Vector2, Vector2> DropCallback;
        private Action DownCallback; 

        private VisualElement newTarget;
        public Dragger(VisualElement _target, Action _downCallback = null)
        {
            _isDragging = false;
            activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
            this.newTarget = _target; 
            this.DownCallback = _downCallback;
        }

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<MouseDownEvent>(OnMouseDown);

        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<MouseDownEvent>(OnMouseDown);

        }

        // 여기서 다른 거 하나랑 연동 
        protected void OnMouseDown(MouseDownEvent e)
        {
            Debug.Log("Down2");
            if (CanStartManipulation(e))
            {
                DownCallback?.Invoke(); 

                _startPos = e.mousePosition; //현재 좌표시스템에서 마우스의 좌표를 반환한다.

                _originalPos = new Vector2(target.style.left.value.value, target.style.top.value.value);

                _isDragging = true;

                Debug.Log("Down");
                newTarget.CaptureMouse(); //해당 타겟이 마우스를 잡는거

                float _h = newTarget.resolvedStyle.height / 2;
                float _w = newTarget.resolvedStyle.width / 2;

                newTarget.style.top = new Length(e.mousePosition.y - _h, LengthUnit.Pixel);
                newTarget.style.left = new Length(e.mousePosition.x - _w, LengthUnit.Pixel);


                e.StopPropagation(); //이벤트 전파중지
            }
        }


    }

}
