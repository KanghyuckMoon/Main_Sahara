using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Popup
{
    public class InteractionPresenter : IPopup
    {
        public VisualElement Parent { get; }

        public void ActiveTween()
        {
        }

        public void InActiveTween()
        {
            throw new System.NotImplementedException();
        }

        public void Undo()
        {
        }

        public void SetData(object _data)
        {

        }

        public IEnumerator TimerCo(float _time)
        {
            throw new System.NotImplementedException();
        }
    }   
}
