using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IPopup
{
    public VisualElement Parent { get; }
    public void Active();
    public void Undo();
    public void SetData(object _data);
    public IEnumerator TimerCo(float _time); 
}
