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
            target.RegisterCallback<MouseDownEvent>((e) =>OnClickCallback?.Invoke());
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.RegisterCallback<MouseDownEvent>((e) =>OnClickCallback?.Invoke());
        }
    }

}
