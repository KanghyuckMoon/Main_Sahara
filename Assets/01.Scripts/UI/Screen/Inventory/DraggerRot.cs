using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DraggerRot : MouseManipulator
{
    private bool _isDragging = false;

    private Action StartCallback = null;
    private Action DragCallback = null;
    private Action EndCallback = null;

    public DraggerRot(Action _startCallback = null, Action _dragCallback = null, Action _endCallback =null)
    {
        _isDragging = false;
        activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });

        this.StartCallback = _startCallback; 
        this.DragCallback = _dragCallback; 
        this.EndCallback = _endCallback; 
        
    }
    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);
        target.RegisterCallback<MouseMoveEvent>(OnMouseStay);
        target.RegisterCallback<MouseLeaveEvent>(OnMouseUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);
        target.RegisterCallback<MouseMoveEvent>(OnMouseStay);
        target.RegisterCallback<MouseLeaveEvent>(OnMouseUp);
    }

    protected void OnMouseDown(MouseDownEvent e)
    {
        // 좌클릭으로 헀는지 조건 체크
        if (CanStartManipulation(e))
        {
            _isDragging = true; 
            StartCallback?.Invoke();
            e.StopPropagation(); //이벤트 전파중지
        }
    }
    
    protected void OnMouseStay(MouseMoveEvent e)
    {
        // 좌클릭으로 헀는지 조건 체크
        if (CanStartManipulation(e) && _isDragging)
        {
            DragCallback?.Invoke();
            e.StopPropagation(); //이벤트 전파중지
            // 키 업시 
            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false; 
            }
        }
    }
    
    protected void OnMouseUp(MouseLeaveEvent e)
    {
        // 좌클릭으로 헀는지 조건 체크
        if (CanStartManipulation(e))
        {
            //_isDragging = false; 
            EndCallback?.Invoke();
            e.StopPropagation(); //이벤트 전파중지
        }
    }
    
}
