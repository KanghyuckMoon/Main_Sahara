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

        // ���⼭ �ٸ� �� �ϳ��� ���� 
        protected void OnMouseDown(MouseDownEvent e)
        {
            Debug.Log("Down2");
            if (CanStartManipulation(e))
            {
                DownCallback?.Invoke(); 

                _startPos = e.mousePosition; //���� ��ǥ�ý��ۿ��� ���콺�� ��ǥ�� ��ȯ�Ѵ�.

                _originalPos = new Vector2(target.style.left.value.value, target.style.top.value.value);

                _isDragging = true;

                Debug.Log("Down");
                newTarget.CaptureMouse(); //�ش� Ÿ���� ���콺�� ��°�

                float _h = newTarget.resolvedStyle.height / 2;
                float _w = newTarget.resolvedStyle.width / 2;

                newTarget.style.top = new Length(e.mousePosition.y - _h, LengthUnit.Pixel);
                newTarget.style.left = new Length(e.mousePosition.x - _w, LengthUnit.Pixel);


                e.StopPropagation(); //�̺�Ʈ ��������
            }
        }


    }

}
