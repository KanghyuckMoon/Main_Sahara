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
            top_panel
        }
        public override void Cashing()
        {
            base.Cashing();
            BindVisualElements(typeof(Elements));
        }

        public void SetThisParent(VisualElement _v)
        {
            GetVisualElement((int)Elements.top_panel).Add(_v); 
        }
    }

}
