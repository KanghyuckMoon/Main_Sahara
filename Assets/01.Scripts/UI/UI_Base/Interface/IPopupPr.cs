
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

namespace UI.Base
{
    public enum PopupType
    {
        GetItem, //  아이템 획득시 
        GetNewItem, // 새 아이템 획득시 
        //FindItem, // 아이템 주변 다가갔을 때
        EventAlarm, // 알림창
        Interaction, // 상호작용 창 ( 아이템 주변 다가갔을 때 
        Shop, // 아이템 구매, 판매시 ㄴ
    }
    public interface IPopupPr
    {
        public void SetParent(VisualElement _v);
        public PopupType PopupType { get; }
    }
    
}
