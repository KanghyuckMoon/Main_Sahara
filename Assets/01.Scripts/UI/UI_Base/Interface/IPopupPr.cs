
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

namespace UI.Base
{
    public enum PopupType
    {
        GetItem, //  아이템 획득시 
        FindItem, // 아이템 주변 다가갔을 때
        EventAlarm, // 알림창
    }
    public interface IPopupPr
    {
        public void SetParent(VisualElement _v);
        public PopupType PopupType { get; }
    }
    
}
