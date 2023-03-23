using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.EventAlarm
{
    [System.Serializable]
    public class EventAlarmScreenView : AbUI_Base
    {
        enum Elements
        {
            top_panel,
            event_alarm_parent
        }
        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
        }

        public void SetThisParent(VisualElement _v)
        {
            GetVisualElement((int)Elements.event_alarm_parent).Add(_v); 
        }

        public void ActiveView()
        {
            ShowVisualElement(parentElement,! IsVisible()); 
        }

        public void ActiveView(bool _isActive)
        {
            ShowVisualElement(parentElement, _isActive);
        }
    }

}
