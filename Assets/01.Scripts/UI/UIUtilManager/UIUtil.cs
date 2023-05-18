using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;

namespace UI.UtilManager
{
    public class UIUtil : MonoBehaviour
    {
        private static float width => Screen.width;
        private static float height => Screen.height; 
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
        /// uitoolkit  ui�� �߽� ��ǥ�� �����´� 
        /// </summary>
        /// <param name="_v"></param>
        /// <returns></returns>
        public static Vector2 GetUICenterPos(VisualElement _v)
        {
            var _rect = _v.worldBound;

            return new Vector2(_rect.x + _rect.width / 2, _rect.y + _rect.height / 2);
        }

        /// <summary>
        /// UGUIPos to uitoolkit Pos
        /// </summary>
        /// <param name="_uguiPos"></param>
        /// <returns></returns>
        public static Vector2 GetUIToolkitPos(Vector2 _uguiPos)
        {
            return new Vector2(_uguiPos.x, height - _uguiPos.y);
        }
    }

}
