using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Antlr3.Runtime.Misc;
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
        target.RegisterCallback<MouseOverEvent>(OnMouseStay);
        target.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);
        target.RegisterCallback<MouseOverEvent>(OnMouseStay);
        target.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected void OnMouseDown(MouseDownEvent e)
    {
        // 좌클릭으로 헀는지 조건 체크
        if (CanStartManipulation(e))
        {
            StartCallback?.Invoke();
            e.StopPropagation(); //이벤트 전파중지
        }
    }
    
    protected void OnMouseStay(MouseOverEvent e)
    {
        // 좌클릭으로 헀는지 조건 체크
        if (CanStartManipulation(e))
        {
            DragCallback?.Invoke();
            e.StopPropagation(); //이벤트 전파중지
        }
    }
    
    protected void OnMouseUp(MouseUpEvent e)
    {
        // 좌클릭으로 헀는지 조건 체크
        if (CanStartManipulation(e))
        {
            EndCallback?.Invoke();
            e.StopPropagation(); //이벤트 전파중지
        }
    }
    
}
