using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System; 

namespace UI.Base
{
    public class DoubleClicker : MouseManipulator
    {
        private Action OnClickCallback; 
        public DoubleClicker(Action _callback)
        {
            activators.Add(new ManipulatorActivationFilter { clickCount = 2, button = MouseButton.LeftMouse });
            this.OnClickCallback = _callback; 
        }

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<MouseDownEvent>(DoubleClick);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.RegisterCallback<MouseDownEvent>(DoubleClick);
        }

        private void DoubleClick(MouseDownEvent _mouseDownEvent)
        {
            if (_mouseDownEvent.clickCount == 2)
            {
                OnClickCallback?.Invoke();
            }
        }
    }

    public class AltClicker : MouseManipulator
    {
        private Action OnClickCallback;

        public AltClicker(Action _callback)
        {
            activators.Add(new ManipulatorActivationFilter
                { clickCount = 1, button = MouseButton.LeftMouse, modifiers = EventModifiers.Alt });
            this.OnClickCallback = _callback;
        }

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<MouseDownEvent>(AltClick);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.RegisterCallback<MouseDownEvent>(AltClick);
        }
        
        private void AltClick(MouseDownEvent _mouseDownEvent)
        {
            if ((_mouseDownEvent.modifiers & EventModifiers.Alt) != 0)
            {
                OnClickCallback?.Invoke();
            }
        }
    }
}
