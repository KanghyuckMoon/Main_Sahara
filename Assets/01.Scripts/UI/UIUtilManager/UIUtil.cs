using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System; 

namespace UI.UtilManager
{
    public class UIUtil : MonoBehaviour
    {
        public static void SendEvent(RadioButton _btn)
        {
            using (var e = new NavigationSubmitEvent() { target = _btn })
                _btn.SendEvent(e);
        }
        public static void SendEvent(Button _btn)
        {
            using (var e = new NavigationSubmitEvent() { target = _btn })
                _btn.SendEvent(e);
        }
        
        public static string GetEnumStr(Type _eType, int _idx)
        {
            string[] _arr = Enum.GetNames(_eType);
            return _arr[_idx];
        }
    }

}
