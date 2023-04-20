using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UI.UGUIBase;

namespace UI.UGUIBase
{
 
    public static class UGUI_Extension
    {
        public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component 
        {
            return UGUUtil.GetOrAddComponent<T>(go);
        }
        public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, UGUIDefine.UIEvent type = UGUIDefine.UIEvent.Click)
        {
            UGUI_Base.AddUIEvent(go, action, type);
        }
    }
    
}
