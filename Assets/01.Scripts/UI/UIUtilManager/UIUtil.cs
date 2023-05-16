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

        /// <summary>
        /// uitoolkit  ui의 중심 좌표를 가져온다 
        /// </summary>
        /// <param name="_v"></param>
        /// <returns></returns>
        public static Vector2 GetUICenterPos(VisualElement _v)
        {
            var _rect = _v.worldBound;

            return new Vector2(_rect.x + _rect.width / 2, _rect.y + _rect.height / 2);
        }
    }

}
