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
                Vector2 diff = e.localMousePosition - _startPos; // ��ȭ�� 

                //layout�� �θ�κ����� ������� ��ġ�� ���Ѵ�.�⺻�� �ȼ��̴ϱ� �� �־ �Ǳ��� new Length ����
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
            target.ReleaseMouse(); //��ġ��� ������ ������
            Debug.Log($"{target.name} : ���� {e.localMousePosition}, ���� : {e.mousePosition}, ������ : {e.originalMousePosition}");

            // ���� ���콺 ������ ��ġ�� ���� �ٿ���� �ȿ� �ִ��� üũ 
            // �ִٸ�, ������ �κ��丮����, ������������ üũ 
            // �κ��丮 �����̸� ���� ���� �ʱ�ȭ, ��ġ�� ���Կ� ������ �ֱ� 
            // �����̸� �� ���� �����ͼ� ���� ������ �־��ֱ� 
            // 
            // ���ٸ�, �� 
            // ���� �巡�� �Ѱ� ��Ȱ��ȭ
            DropCallback?.Invoke(e.mousePosition);
        }
    }

}
